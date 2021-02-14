using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Repository.Life;

namespace WebMaze.Models.CustomAttribute.Life
{
    /// <summary>
    /// Validation attribute for checking if victim is unique in Victims table.
    /// </summary>
    public class UniqueVictimAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validation method for checking if victim is unique in Victims table.
        /// It uses InitialCitizenId to supress validation if the user did not choose anything in the dropdown.
        /// It uses <see cref="VictimRepository.hasCitizenAndAccident(long, long)"/> method to
        /// check uniqueness.
        /// </summary>
        /// <param name="value">Value of the model property being checked (CitizenId)</param>
        /// <param name="validationContext">Context (supplied by DI) which has the model object
        /// with all other model properties</param>
        /// <returns>Success if vicitm is unique or string message if not unique.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var selectedVictimId = (long?)value;
            var initialVictim = validationContext.ObjectType.GetProperty("InitialCitizenId");
            if (initialVictim == null)
            {
                throw new ArgumentException("Property InitialCitizenId of the AccidentVictimViewModel is not found");
            }
            
            var initialVictimId = (long?)initialVictim.GetValue(validationContext.ObjectInstance);
            if (selectedVictimId == initialVictimId || selectedVictimId == null)
            {
                // user did not choose any other victim or chose nothing. No need to validate
                return ValidationResult.Success;
            }

            var accident = validationContext.ObjectType.GetProperty("AccidentId");
            if (accident == null)
            {
                throw new ArgumentException("Property AccidentId of the AccidentVictimViewModel is not found");
            }
            var accidentId = (long)accident.GetValue(validationContext.ObjectInstance);


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
