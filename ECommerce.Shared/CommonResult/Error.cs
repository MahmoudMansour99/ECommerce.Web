using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CommonResult
{
    public class Error
    {
        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorType ErrorType { get; set; }

        // Static factory method for creating a Failure error
        public static Error Failure(string code = "General.Failure", string Description = "General Faliure has occured")
        {
            return new Error(code, Description, ErrorType.Failure);
        }
        public static Error Validation(string code = "General.Validation", string Description = "General Validation has occured")
        {
            return new Error(code, Description, ErrorType.Validation);
        }
        public static Error NotFound(string code = "General.NotFound", string Description = "The Request Response is not found")
        {
            return new Error(code, Description, ErrorType.NotFound);
        }
        public static Error UnAuthorized(string code = "General.UnAuthorized", string Description = "You're not Authorized")
        {
            return new Error(code, Description, ErrorType.UnAuthorized);
        }
        public static Error Forbidden(string code = "General.Forbidden", string Description = "You're Forbidden")
        {
            return new Error(code, Description, ErrorType.Forbidden);
        }
        public static Error InvalidCardentials(string code = "General.InvalidCardentials", string Description = "The Provided Cardintials not Valid")
        {
            return new Error(code, Description, ErrorType.InvalidCardentials);
        }
    }
}
