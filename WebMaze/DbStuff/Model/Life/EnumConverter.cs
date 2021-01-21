using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.DbStuff.Model.Life
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    public class EnumConverter : IValueConverter<Enum, string> 
    {
        public string Convert(Enum enumValue, ResolutionContext resCtx)
        {
            if (enumValue is AccidentCategoryEnum accidentCategoryEnum)
            {
                return Dictionaries.GetText().AccidentCategory[accidentCategoryEnum];
            }

            if (enumValue is BodilyHarmEnum bodylyHarmEnum)
            {
                return Dictionaries.GetText().BodilyHarm[bodylyHarmEnum];
            }

            if (enumValue is CriminalCodeEnum criminalCodeEnum)
            {
                return Dictionaries.GetText().CriminalCode[criminalCodeEnum];
            }

            if (enumValue is FireCauseEnum fireCauseEnum)
            {
                return Dictionaries.GetText().FireCause[fireCauseEnum];
            }

            if (enumValue is FireClassEnum fireClassEnum)
            {
                return Dictionaries.GetText().FireClass[fireClassEnum];
            }

            return "";
        }

    }
}
