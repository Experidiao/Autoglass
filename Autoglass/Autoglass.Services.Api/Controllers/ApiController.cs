using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;

namespace Autoglass.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public abstract class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();

        protected ActionResult CustomResponse(object xresult = null)
        {
            if (IsOperationValid())
            {
                return Ok(new { error = "0", result = xresult });
            }
            return BadRequest(new { error = "1", resul = _errors.ToArray() });
        }


        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }
        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }
    }
}
