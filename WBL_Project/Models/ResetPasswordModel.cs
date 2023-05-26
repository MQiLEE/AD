using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WBL_Project.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required")]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        public string ResetCode { get; set; }
    }
}