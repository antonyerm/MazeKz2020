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
    public class AddressConverter : IValueConverter<Adress, string> 
    {
        public string Convert(Adress addressObject, ResolutionContext resCtx)
        {
            return $"{addressObject.HouseNumber}, {addressObject.Street}, {addressObject.City}";
        }

    }
}
