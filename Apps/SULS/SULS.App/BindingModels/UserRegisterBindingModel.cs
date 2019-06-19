using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.BindingModels
{
    public class UserRegisterBindingModel
    {
        private const string errorUsername = "The username should be between 5 and 20 symbols.";
        private const string errorPassword = "The password should be between 6 and 20 symbols.";


        [RequiredSis]
        [StringLengthSis(5, 20, errorUsername)]
        public string Username { get; set; }

        [RequiredSis]
        [EmailSis()]
        public string Email { get; set; }

        [RequiredSis]
        [StringLengthSis(6, 20, errorPassword)]
        public string Password { get; set; }

        [RequiredSis]
        public string ConfirmPassword { get; set; }
    }
}
