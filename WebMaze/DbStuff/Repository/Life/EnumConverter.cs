using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    public class EnumConverter : IValueConverter<Enum, string>
    {
        public string Convert(Enum enumValue, ResolutionContext resCtx)
        {
            return Dictionaries.GetText(enumValue);
        }

    }
}
