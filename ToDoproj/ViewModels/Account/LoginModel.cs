using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoproj.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email field is invalid")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password field is invalid")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
