using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokboken.ViewModels
{
    public enum enDifficultyDish { Enkel, Medium, Komplicerad }
    public enum enSpeedDish { Turbo, Snabb, Medium, Långkok }
    public class Recipes
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public enDifficultyDish? enDiff {  get; set; }
        public enSpeedDish? enSpeed { get; set; }
        public override string ToString() => $"{Title}";

        //public Recipes(string t, string d, string i)//, enDifficultyDish = null, enSpeedDish = null)
        //{
        //    Title = t; 
        //    Description = d; 
        //    Ingredients = i;
        //}
    }
    
}
