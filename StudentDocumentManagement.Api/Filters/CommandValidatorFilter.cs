using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentDocumentManagement.Api.Controllers;
using StudentDocumentManagement.Core.Application.Accounts.Commands.Login;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using System.Net;

namespace StudentDocumentManagement.Api.Filters;

public class CommandValidatorFilter<TCommand, Controller, Validator> : ActionFilterAttribute
    where TCommand : class
    where Controller : ApiController
    where Validator : AbstractValidator<TCommand>
{
    private readonly IValidator<TCommand> _validator;
    private readonly ILogger<TCommand> _logger;

    public CommandValidatorFilter(IValidator<TCommand> validator, ILogger<TCommand> logger)
    {
        _validator = validator;
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string message = "Invalid model object";

        var model = context.ActionArguments.Values.OfType<TCommand>().FirstOrDefault();
        if (model == null)
        {
            _logger.LogError("{@DateTimeNow} Ubicacion: {@Location}, Errores: {@mensaje}, " +
                        "Http Status Code: {@StatusCode}",
                            DateTime.Now.ToString(),
                            typeof(Controller),
                            message,
                            HttpStatusCode.BadRequest);

            context.Result = new BadRequestObjectResult(message);
            return;
        }

        var validationResult = _validator.Validate(model);
        if (!validationResult.IsValid)
        {
            _logger.LogError("{@DateTimeNow} Ubicacion: {@Location}, Validator: {@validator} Errores: {@mensaje}, " +
            "Http Status Code: {@StatusCode}",
                DateTime.Now.ToString(),
                typeof(Controller),
                typeof(Validator).Name,
                validationResult.Errors,
                HttpStatusCode.BadRequest);

            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(validationResult.ToDictionary()));
        }
    }
}
