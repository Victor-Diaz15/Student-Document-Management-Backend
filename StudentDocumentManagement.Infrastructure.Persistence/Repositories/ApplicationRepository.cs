﻿using Microsoft.EntityFrameworkCore;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Core.Domain.Enums;
using StudentDocumentManagement.Infrastructure.Persistence.Contexts;

namespace StudentDocumentManagement.Infrastructure.Persistence.Repositories;

public class ApplicationRepository : GenericBaseRepository<Application>, IApplicationRepository
{
    private readonly MainDbContext _dbContext;
    public ApplicationRepository(MainDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> CompleteApplication(List<Guid> applicationIds)
    {
        string applicationNumberId = GenerateApplicationNumberId();

        await _dbContext.Applications
            .Where(a => applicationIds.Contains(a.Id))
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.ApplicationNumberId, applicationNumberId)
                .SetProperty(a => a.Status, ApplicationStatus.Completada));

        return applicationNumberId;
    }

    public async Task<List<Application>> GetAllWithIncludeAndThenInclude()
    {
        var list = await _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.Service!)
            .Include(x => x.Files!)
            .ThenInclude(x => x.StudentFile)
            .ToListAsync();

        return list;
    }

    public async Task<List<Application>> GetApplicationsByApplicationsIds(List<Guid> applicationIds)
    {
        var list = await _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.Service!)
            .Where(x => applicationIds.Contains(x.Id))
            .ToListAsync();

        return list;
    }

    public async Task<List<Application>> GetApplicationsByFilters(string? studentId, string? applicationNumberId, string? serviceId, ApplicationStatus? status)
    {
        var resultList = new List<Application>();
        var query = base.GetQueryable();

        if (!string.IsNullOrEmpty(studentId))
        {
            query = query.Where(x => x.StudentId.ToString() == studentId);
        }

        if (!string.IsNullOrEmpty(applicationNumberId))
        {
            query = query.Where(x => x.ApplicationNumberId == applicationNumberId);
        }

        if (!string.IsNullOrEmpty(serviceId))
        {
            query = query.Where(x => x.ServiceId.ToString() == serviceId);
        }

        if(status != null)
        {
            query = query.Where(x => x.Status == status);
        }

        query = query.Include(x => x.Service!)
            .Include(x => x.Files!)
            .ThenInclude(x => x.StudentFile)
            .AsNoTracking();

        resultList = await query.ToListAsync();

        return resultList;
    }

    public async Task<Application?> GetApplicationToUpdate(Guid applicationId)
    {
        var application = await _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.Service!)
            .Include(x => x.Files!.Where(x => x.StudentFile!.Status != FileStatus.Validado))
            .ThenInclude(x => x.StudentFile)
            .FirstOrDefaultAsync(x => x.Id == applicationId);

        return application;
    }

    public async Task<Application?> GetByIdWithIncludeAndThenInclude(Guid applicationId)
    {
        var application = await _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.Service!)
            .Include(x => x.Files!)
            .ThenInclude(x => x.StudentFile)
            .FirstOrDefaultAsync(x => x.Id == applicationId);

        return application;
    }

    private string GenerateApplicationNumberId()
    {
        // Obtén el último número de matrícula generado
        var lastApplicationNumberId = _dbContext.Applications
            .OrderByDescending(s => s.ApplicationNumberId)
            .Select(s => s.ApplicationNumberId)
            .FirstOrDefault();

        // Si no hay números anteriores, empieza desde PG-0001
        if (string.IsNullOrEmpty(lastApplicationNumberId) || !lastApplicationNumberId.StartsWith('P'))
        {
            return "PG-0001";
        }

        // Extrae la parte numérica del último número
        var lastNumber = int.Parse(lastApplicationNumberId.Substring(4));
        var newNumber = lastNumber + 1;

        // Genera el nuevo número de matrícula
        return $"PG-{newNumber:D4}";
    }
}
