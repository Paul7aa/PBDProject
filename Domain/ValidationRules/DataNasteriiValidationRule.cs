using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PBDProject.Domain.ValidationRules
{
    public class DataNasteriiValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime dataNasterii = (DateTime)value;
            if (dataNasterii.Date.Year > DateTime.UtcNow.Date.Year-10) return new ValidationResult(false, "Alegeți o dată validă!");
            return ValidationResult.ValidResult;
        }
    }
}
