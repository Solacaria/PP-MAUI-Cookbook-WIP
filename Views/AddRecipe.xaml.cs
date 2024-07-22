using Kokboken.Globals;
using Kokboken.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kokboken.Views;

public partial class AddRecipe : ContentPage
{
	public AddRecipe()
	{
		InitializeComponent();
    }

    private async void MainMenu_Button(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
    private async void SaveRecipe_Button(object sender, EventArgs e)
    {
        if (AddIngr.Text == "")
        {
            await DisplayAlert("Tomt fält", "Ingredienslistan är tom", "OK");
            return;
        }
        if (AddDesc.Text == "")
        {
            await DisplayAlert("Tomt fält", "Beskrivningen är tom", "OK");
            return;
        }
        if (AddTitle.Text == "")
        {
            await DisplayAlert("Tomt fält", "Titeln är tom", "OK");
            return;
        }
        //methods to check the radio buttons and get value.
        enSpeedDish? newSpeed = NewSpeedEnum();
        if (newSpeed == null)
        {
            await DisplayAlert("Tomt fält", "Hastighet är tom", "OK");
            return;
        }
        enDifficultyDish? newDiff = NewDiffEnum();
        if (newDiff == null)
        {
            await DisplayAlert("Tomt fält", "Svårighetsgrad är tom", "OK");
            return;
        }

        string newTitle = AddTitle.Text;
        string newDesc = AddDesc.Text;
        string newIngr = AddIngr.Text;
     
        var newTags = NewTagChecker();

        //If no tags were added
        if (newTags.Count == 0) 
        {
            Recipes newRecipe = new Recipes { Title = newTitle, Description = newDesc, Ingredients = newIngr, enDiff = newDiff, enSpeed = newSpeed };
            Global.Data.recipes.Add(newRecipe);
        }
        //If tags were added
        if (newTags.Count != 0) 
        {
            List<string> upperTag = new List<string>();
            for (int i = 0;  i < newTags.Count; i++)
            {
                upperTag.Add(newTags[i].ToUpper());
            }
            Recipes newRecipe = new Recipes { Title = newTitle, Description = newDesc, Ingredients = newIngr, enDiff = newDiff, enSpeed = newSpeed, Tags = upperTag };
            Global.Data.recipes.Add(newRecipe);
        }
       
        ClearFields();
        
        await DisplayAlert("Nytt Recept", $"{newTitle} sparad", "OK");
        Global.SerilizeJson();

        await Shell.Current.GoToAsync("//MainPage");

    }
    /// <summary>
    /// Returns the value from clicked radiobutton
    /// </summary>
    /// <returns></returns>
    private enSpeedDish? NewSpeedEnum()
    {
        enSpeedDish speed;

        if (AddSpeedTurbo.IsChecked)
        {
            speed = enSpeedDish.Turbo;
            return speed;
        }
        if (AddSpeedSnabb.IsChecked)
        {
            speed = enSpeedDish.Snabb;
            return speed;
        }
        if (AddSpeedMedium.IsChecked)
        {
            speed = enSpeedDish.Medium;
            return speed;
        }
        if (AddSpeedLångkok.IsChecked)
        {
            speed = enSpeedDish.Långkok;
            return speed;
        }

        return null;
    }
    /// <summary>
    /// Returns the value from clicked radiobutton
    /// </summary>
    /// <returns></returns>
    private enDifficultyDish? NewDiffEnum()
    {
        enDifficultyDish diff;
        if (AddDiffEnkel.IsChecked)
        {
            diff = enDifficultyDish.Enkel;
            return diff;
        }
        if (AddDiffMedium.IsChecked)
        {
            diff = enDifficultyDish.Medium;
            return diff;
        }
        if (AddDiffKomplicerad.IsChecked)
        {
            diff = enDifficultyDish.Komplicerad;
            return diff;
        }
        return null;
    }
    /// <summary>
    /// Splits the string into elements for the list of tags
    /// </summary>
    /// <returns></returns>
    private List<string> NewTagChecker()
    {
      //Filters out whitespace and commas from tags
        if (AddTag.Text.Contains(",") && AddTag.Text.Contains(' '))
        {
            List<string> newTags = new List<string>();

            string[] NT = AddTag.Text.Split(",");
            string[] PH;

            foreach (var t in NT)
            {
                if (t.Contains(" "))
                {
                    if (t != "")
                    {
                        PH = t.Split(' ');
                        foreach (var p in PH)
                        {
                            if (p != "")
                            {
                                p.TrimStart().TrimEnd();
                                newTags.Add(p);
                            }
                        }
                    }
                }
                else newTags.Add(t.Trim());
            }
            return newTags;
        }
        if (AddTag.Text.Contains(','))
        {
            List<string> newTags = new List<string>();
            var tags = AddTag.Text.Split(",");
            foreach (var s in tags)
            {
                if (s != "")
                {
                    newTags.Add(s.Trim());
                }
            }
            return newTags;
        }
        if (AddTag.Text.Contains(' '))
        {
            List<string> newTags = new List<string>();
            var tags = AddTag.Text.Split(" ");
            foreach (var s in tags)
            {
                if (s != "")
                {
                    newTags.Add(s.Trim());
                }
            }
            return newTags;
        }
        List<string> tagList =  new List<string> { AddTag.Text };
        return tagList;
    }
    /// <summary>
    /// Clears the fields.
    /// </summary>
    private void ClearFields()
    {
        //Återställer textfälten
        AddTitle.Text = "";
        AddDesc.Text = "";
        AddIngr.Text = "";
        AddTag.Text = "";

        //Återställer radioknapparna
        AddDiffEnkel.IsChecked = true;
        AddDiffEnkel.IsChecked = false;

        AddSpeedSnabb.IsChecked = true;
        AddSpeedSnabb.IsChecked = false;
    }
}