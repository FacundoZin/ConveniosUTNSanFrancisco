using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Helpers.Validators
{
    public class ValidacionFechas : ValidationAttribute
    {
        private readonly string _ComparisonPropertyName;
        public ValidacionFechas(string comparisonPropertyName)
        {
            _ComparisonPropertyName = comparisonPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as DateOnly?;
            var property = validationContext.ObjectType.GetProperty(_ComparisonPropertyName);


            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as DateOnly?;

            if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? $"La fecha debe ser mayor que {_ComparisonPropertyName}.");
            }

            return ValidationResult.Success;
        }
    }
}
