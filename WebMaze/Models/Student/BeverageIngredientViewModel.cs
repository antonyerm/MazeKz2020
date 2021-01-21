using System.Collections.Generic;
using System.Globalization;
using System.Security.Policy;

namespace WebMaze.Models
{
    public class BeverageIngredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public BeverageIngredientUnit Units { get; set; }
        public string PictureUrl { get; set; }

        public string UnitsAsString
        {
            get
            {
                switch (Units)
                {
                    case BeverageIngredientUnit.g:
                        return "г";
                    case BeverageIngredientUnit.mL:
                        return "мл";
                    default:
                        return "";
                }
            }

        } 

    }

    public enum BeverageIngredientUnit
    {
        g,
        mL,
    }

}