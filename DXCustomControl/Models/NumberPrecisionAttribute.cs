using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ISTS.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NumberPrecisionAttribute : ValidationAttribute
    {
        public int MaxDigits { get; set; }
        public int MaxDigitAfterPoint { get; set; } = 0;
        public int MinDigits { get; set; } = -1;
        public NumberPrecisionAttribute()
        {
        }
        public NumberPrecisionAttribute(int totalCharacter, int minTotalDigit, int maxNumberAfterPoint) : this(totalCharacter, maxNumberAfterPoint)
        {
            MinDigits = minTotalDigit;
        }

        public NumberPrecisionAttribute(int totalCharacter) : this(totalCharacter, 0)
        {
            MaxDigits = totalCharacter;
        }
        public NumberPrecisionAttribute(int totalCharacter, int maxNumberAfterPoint)
        {
            MaxDigitAfterPoint = maxNumberAfterPoint;
            MaxDigits = totalCharacter;
        }
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            string number = Math.Abs(Convert.ToDecimal(value)).ToString("0.####################");
            string naturalNumber = @"^\d{1," + MaxDigits + "}$";
            string numberWithAfterPointer = @"^\d+(\.{0,1}|\,{0,1})\d{0," + MaxDigitAfterPoint + "}$";
            //if (MaxDigitAfterPoint == 0)
            //{
            //    if (!Regex.IsMatch(number, naturalNumber))
            //    {
            //        return new ValidationResult(string.Format("The {0}'s value must be equal or less than {1} digit(s)", ctx.DisplayName, MaxDigits));
            //    }
            //}
            //else
            //{
            string naturalValue = number.Replace(",", "").Replace(".", "");
            if (!Regex.IsMatch(naturalValue, naturalNumber))
            {
                return new ValidationResult(string.Format("The {0}'s value must be equal or less than {1} digit(s)", ctx.DisplayName, MaxDigits));
            }
            else if (!Regex.IsMatch(number, numberWithAfterPointer))
            {
                return new ValidationResult(string.Format("The {0}'s digit after decimal point must be equal or less than {1} digit(s)", ctx.DisplayName, MaxDigitAfterPoint));
            }
            //}
            return ValidationResult.Success;
        }
    }

    //public class NumberLengthAttribute : ValidationAttribute, IClientValidatable
    //{
    //    private int _totalNumberCharacter;
    //    public NumberLengthAttribute(int totalDigits)
    //    {
    //        _totalNumberCharacter = totalDigits;
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        return true;
    //    }
    //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(System.Web.Mvc.ModelMetadata metadata, ControllerContext ctx)
    //    {
    //        string errorMessage = string.Format(LOS.Resources.Util.Messages.MaxDigits, string.IsNullOrEmpty(metadata.DisplayName) ? metadata.PropertyName : metadata.DisplayName, _totalNumberCharacter);

    //        // The value we set here are needed by the jQuery adapter
    //        ModelClientValidationRule dateGreaterThanRule = new ModelClientValidationRule();
    //        dateGreaterThanRule.ErrorMessage = errorMessage;
    //        dateGreaterThanRule.ValidationType = "validatenumberlength"; // This is the name the jQuery adapter will use
    //        //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
    //        dateGreaterThanRule.ValidationParameters.Add("totaldigit", _totalNumberCharacter);
    //        //dateGreaterThanRule.ValidationParameters.Add("maxlength", MaxLength);


    //        yield return dateGreaterThanRule;
    //    }
    //}


    [System.Diagnostics.DebuggerStepThrough]
    [AttributeUsage(AttributeTargets.Property)]
    public class NumberRangeAttribute : ValidationAttribute
    {
        public double? MinValue { get; private set; }
        public double? MaxValue { get; private set; }
        public NumberRangeAttribute(double minVal, double maxVal)
        {
            MinValue = minVal;
            MaxValue = maxVal;
        }
        public NumberRangeAttribute(double value, bool isMinVal)
        {
            MinValue = isMinVal ? value : MinValue;
            MaxValue = !isMinVal ? value : MaxValue;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            double? inputValue = Convert.ToDouble(value + "");
            if (MinValue.HasValue && MaxValue.HasValue)
            {
                return inputValue >= MinValue && inputValue <= MaxValue ? ValidationResult.Success : new ValidationResult($"{validationContext.DisplayName} must be in range {MinValue} and {MaxValue}");
            }
            if (MinValue.HasValue)
            {
                return inputValue >= MinValue ? ValidationResult.Success : new ValidationResult($"{validationContext.DisplayName} must be equal to or bigger than {MinValue}");
            }
            if (MaxValue.HasValue)
            {
                return inputValue <= MaxValue ? ValidationResult.Success : new ValidationResult($"{validationContext.DisplayName} must be equal to or less than {MaxValue}");
            }
            return ValidationResult.Success;
        }
    }
}
