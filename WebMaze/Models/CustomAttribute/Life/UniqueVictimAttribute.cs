using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Repository.Life;

namespace WebMaze.Models.CustomAttribute.Life
{
    public class UniqueVictimAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var selectedVictimId = (long?)value;
            var property = validationContext.ObjectType.GetProperty("InitialCitizenId");
            if (property == null)
            {
                throw new ArgumentException("Property InitialCitizenId of the AccidentVictimViewModel is not found");
            }
            
            var initialVictimId = (long?)property.GetValue(validationContext.ObjectInstance);
            if (selectedVictimId == initialVictimId || selectedVictimId == null)
            {
                // user did not choose any other victim or chose nothing. No need to validate
                return ValidationResult.Success;
            }

            property = validationContext.ObjectType.GetProperty("AccidentId");
            if (property == null)
            {
                throw new ArgumentException("Property AccidentId of the AccidentVictimViewModel is not found");
            }
            var accidentId = (long)property.GetValue(validationContext.ObjectInstance);


            var victimRepository = validationContext.GetService(typeof(VictimRepository))
                as VictimRepository;
            var isValueOK = !victimRepository.hasCitizenAndAccident((long)selectedVictimId, accidentId);

            if (isValueOK)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage?? "Выбранный человек уже есть в списке потерпевших");
        }
    }
}
