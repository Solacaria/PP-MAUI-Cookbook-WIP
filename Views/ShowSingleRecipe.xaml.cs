using Kokboken.Globals;
using Kokboken.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Diagnostics;

namespace Kokboken.Views;

public partial class ShowSingleRecipe : ContentPage
{
   
	public ShowSingleRecipe()
	{
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        var recept = Global.Data.rndRecipe.GroupBy(x => x.Title);
        SingleRecipePage.ItemsSource = recept;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //Goes to Main menu
		await Shell.Current.GoToAsync("//MainPage");
    }

    private void Button_Clicked_rnd(object sender, EventArgs e)
    {
        //Randomizes a new recipe and reloads the page
        Global.RandomRecipe();
        this.OnAppearing();
    }

    private async void Button_Clicked_edit(object sender, EventArgs e)
    {
        //Takes current recipe and moves to edit page.
        await Shell.Current.GoToAsync("//EditRecipe");
    }

    private async void Button_Clicked_rmv(object sender, EventArgs e)
    {
        var currentRecipe = Global.Data.rndRecipe[0];
        Recipes placeholder = null;
        int idx = -1;
        var allRecipes = Global.Data.recipes;

        for (int i = 0; i < allRecipes.Count; i++)
        {
            if (allRecipes[i] == currentRecipe)
            {
                placeholder = allRecipes[i];
                idx = i;
                Debug.WriteLine("Lika");
            }
            else
            {
                Debug.WriteLine("Inte lika");
            }
        }
        
        string result = await DisplayActionSheet($"{currentRecipe.Title}\nVill du ta bort receptet?", "Nej", null, null, "Ja");

        if (result == "Nej") return;
        if (result == "Ja" && idx != -1)
        {
            Global.Data.recipes.RemoveAt(idx);
            await DisplayAlert(null, $"{currentRecipe.Title} är nu borttagen från listan", "OK");
            await Shell.Current.GoToAsync("//ShowAllRecipes");
        }

        return;
    }
}