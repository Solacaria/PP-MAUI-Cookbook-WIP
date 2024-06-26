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
        BackgroundImageSource = "background.jpg";

        var recept = Global.Data.rndRecipe[0];
        EditRecipePage.BindingContext = recept;
    }
    private async void MainMenu_Button(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void SaveRecipe_Button(object sender, EventArgs e)
    {
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

        //new values
        string newTitle = EditTtile.Text;
        string newDesc = EditDesc.Text;
        string newIngr = EditIngr.Text;
        //methods to check the radio buttons and get value.
        enSpeedDish? newSpeed = NewSpeedEnum(); 
        enDifficultyDish? newDiff = NewDiffEnum();

        //sets the new value.
        oldRecipe.Title = newTitle;
        oldRecipe.Description = newDesc;
        oldRecipe.Ingredients = newIngr;
        oldRecipe.enDiff = newDiff;
        oldRecipe.enSpeed = newSpeed;

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
}
