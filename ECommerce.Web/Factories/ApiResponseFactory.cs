using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                              .ToDictionary(X => X.Key, X => X.Value.Errors
                                              .Select(x => x.ErrorMessage)).ToArray();
            var Problem = new ProblemDetails()
            {
                Title = "Validation errors.",
                Detail = "Oe or More Validation Error occured",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                        {
                            { "errors", errors }
                        }
            };
            return new BadRequestObjectResult(Problem);
        }
    }
}
