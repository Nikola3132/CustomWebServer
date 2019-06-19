using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.BindingModels
{
    public class ProblemBindingModel
    {
        private const string errorName = "The name should be between 5 and 20 symbols.";
        private const string errorTotalPoints = "The total points should be between 50 and 300.";

        [RequiredSis()]
        [StringLengthSis(5, 20, errorName)]
        public string Name { get; set; }

        [RequiredSis()]
        [RangeSis(50, 300, errorTotalPoints)]
        public int Points { get; set; }
    }
}
