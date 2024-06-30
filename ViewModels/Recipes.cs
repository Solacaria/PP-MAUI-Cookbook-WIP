using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kokboken.ViewModels
{
    public enum enDifficultyDish { Enkel, Medium, Komplicerad }
    public enum enSpeedDish { Turbo, Snabb, Medium, Långkok }
    public class Recipes : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private string _ingredients;
        public List<string> Tags { get; set; } = new List<string>();
        public string TagString { get; set; } = null;
       
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string Description
        { 
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        public string Ingredients
        {
            get { return _ingredients; }
            set
            {
                if (value != _ingredients)
                {
                    _ingredients = value;
                    OnPropertyChanged(nameof(Ingredients));
                }
            }
        }
    
        public enDifficultyDish? enDiff {  get; set; }   
        public enSpeedDish? enSpeed {  get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string pName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pName));
        }
        public override string ToString() => $"{Title}";
    }
    
}
