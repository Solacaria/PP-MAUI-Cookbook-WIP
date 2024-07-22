using Kokboken.Globals;
using Kokboken.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Kokboken.Views;

public partial class EditRecipe : ContentPage
{
	public EditRecipe()
	{
		InitializeComponent();
      
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        var recept = Global.Data.rndRecipe[0];

        recept.TagString = TagsToString(recept);
        CheckRadioButtons(recept);
       
        EditRecipePage.BindingContext = recept;
    }
    private async void MainMenu_Button(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void SaveRecipe_Button(object sender, EventArgs e)
    {
        if (EditIngr.Text == "")
        {
            await DisplayAlert("Tomt fält", "Ingredienslistan är tom", "OK");
            return;
        }
        if (EditDesc.Text == "")
        {
            await DisplayAlert("Tomt fält", "Beskrivningen är tom", "OK");
            return;
        }
        if (EditTitle.Text == "")
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

        //new values
        string newTitle = EditTitle.Text;
        string newDesc = EditDesc.Text;
        string newIngr = EditIngr.Text;
        var newTag = NewTagChecker(); 

        int idx = 0;
        int nrRecipes = Global.Data.recipes.Count;
        var currentRecipe = Global.Data.rndRecipe[0];

        //Matching the title from the new and old recipes
        for (int i = 0; i < nrRecipes; i++)
        {
            if (Global.Data.recipes[i].Title == Global.Data.rndRecipe[0].Title)
            {
                idx = i;
            }
        }
        //recipe to be changed.
        var oldRecipe = Global.Data.recipes[idx];

        //sets the new values.
        oldRecipe.Title = newTitle;
        oldRecipe.Description = newDesc;
        oldRecipe.Ingredients = newIngr;
        oldRecipe.enDiff = newDiff;
        oldRecipe.enSpeed = newSpeed;
        oldRecipe.Tags = newTag;

        await DisplayAlert($"{newTitle} ändrad", null, "OK");
        Global.SerilizeJson();

        await Shell.Current.GoToAsync("//MainPage");

    }
    private enSpeedDish? NewSpeedEnum()
    {
        enSpeedDish speed;

        if (EditSpeedTurbo.IsChecked)
        {
            speed = enSpeedDish.Turbo;
            return speed;
        }
        if (EditSpeedSnabb.IsChecked)
        {
            speed = enSpeedDish.Snabb;
            return speed;
        }
        if (EditSpeedMedium.IsChecked)
        {
            speed = enSpeedDish.Medium;
            return speed;
        }
        if (EditSpeedLångkok.IsChecked)
        {
            speed = enSpeedDish.Långkok;
            return speed;
        }

        return null;
    }
    private enDifficultyDish? NewDiffEnum()
    {
        enDifficultyDish diff;
        if (EditDiffEnkel.IsChecked)
        {
            diff = enDifficultyDish.Enkel;
            return diff;
        }
        if (EditDiffMedium.IsChecked)
        {
            diff = enDifficultyDish.Medium;
            return diff;
        }
        if (EditDiffKomplicerad.IsChecked)
        {
            diff = enDifficultyDish.Komplicerad;
            return diff;
        }
        return null;
    }
    private void CheckRadioButtons(Recipes recept)
    {
        if (recept.enDiff == enDifficultyDish.Enkel) EditDiffEnkel.IsChecked = true;
        if (recept.enDiff == enDifficultyDish.Medium) EditDiffMedium.IsChecked = true;
        if (recept.enDiff == enDifficultyDish.Komplicerad) EditDiffKomplicerad.IsChecked = true;

        if (recept.enSpeed == enSpeedDish.Turbo) EditSpeedTurbo.IsChecked = true;
        if (recept.enSpeed == enSpeedDish.Snabb) EditSpeedSnabb.IsChecked = true;
        if (recept.enSpeed == enSpeedDish.Medium) EditSpeedMedium.IsChecked = true;
        if (recept.enSpeed == enSpeedDish.Långkok) EditSpeedLångkok.IsChecked = true;
    }
    private string TagsToString(Recipes recept)
    {
        string tagg = "";
        foreach (var t in recept.Tags)
        {
            tagg += $"{t}, ";
        }
        return tagg.TrimEnd();
    }
    /// <summary>
    /// Splits the string into elements for the list of tags
    /// </summary>
    /// <returns></returns>
    private List<string> NewTagChecker()
    {
        //Filters out whitespace and commas from tags.
        if (EditTag.Text.Contains(",") && EditTag.Text.Contains(' '))
        {
            List<string> newTags = new List<string>();

            string[] NT = EditTag.Text.Split(",");
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
        if (EditTag.Text.Contains(','))
        {
            List<string> newTags = new List<string>();
            var tags = EditTag.Text.Split(",");
            foreach (var s in tags)
            {
                if (s != "")
                {
                    newTags.Add(s.Trim());
                }
            }
            return newTags;
        }
        if (EditTag.Text.Contains(' '))
        {
            List<string> newTags = new List<string>();
            var tags = EditTag.Text.Split(" ");
            foreach (var s in tags)
            {
                if (s != "")
                {
                    newTags.Add(s.Trim());
                }
            }
            return newTags;
        }
        return null;
    }
}
