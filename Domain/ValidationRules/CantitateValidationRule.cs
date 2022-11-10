using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PBDProject.Domain.ValidationRules
{
    public class CantitateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool parseSuccessful = Byte.TryParse((string)value, out Byte byteValue);
            if (!parseSuccessful)
                return new ValidationResult(false, "Acceptă doar valori numerice naturale!");
            if (byteValue <= 0)
                return new ValidationResult(false, "Nu poate fi 0 sau mai mic!");
            if (byteValue > this.CantitateMax.CantitateMax)
                return new ValidationResult(false, "Nu poate fi mai mult de " + this.CantitateMax.CantitateMax.ToString() + " !");
            return ValidationResult.ValidResult;
        }

        public Wrapper CantitateMax { get; set; }
    }

    public class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty CantitateMaxProperty =
             DependencyProperty.Register("CantitateMax", typeof(Byte),
             typeof(Wrapper), new FrameworkPropertyMetadata(Byte.MaxValue));

        public Byte CantitateMax
        {
            get { return (Byte)GetValue(CantitateMaxProperty); }
            set { SetValue(CantitateMaxProperty, value); }
        }
    }
    public class BindingProxy : System.Windows.Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
    }
}
