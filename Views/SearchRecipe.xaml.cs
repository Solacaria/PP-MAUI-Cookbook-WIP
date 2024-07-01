using Kokboken.Globals;
using Kokboken.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace Kokboken.Views;

public partial class SearchRecipe : ContentPage
{
    private List<Recipes> SearchedRecipes { get; set; } = new List<Recipes>();
  
    public SearchRecipe()
	{
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        BackgroundImageSource = "background.jpg";
        search_input.Text = string.Empty;
        SearchRecipePage.ItemsSource = null;

    }
  
    private async void Button_Clicked(object sender, EventArgs e)
    {
        SearchedRecipes.Clear();
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void SearchRecipePage_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Recipes recept = (Recipes)e.Item;

        string result = await DisplayActionSheet(recept.Title, "Ja", "Nej", null, "Vill du gå till receptet?");
        if (result == "Nej") return;
        if (result == "Ja")
        {
            //Checks to see if the rndRecipe has an object or not. Using same View as random recipe for simplicity sake
            if (Global.Data.rndRecipe.Count > 0)
            {
                //Sets the random recipe holder to the clicked recipe.
                Global.Data.rndRecipe.RemoveAt(0);
            }
            Global.Data.rndRecipe.Add(recept);
            await Shell.Current.GoToAsync("//ShowSingleRecipe");
        }
    }

    private void search_button_Clicked(object sender, EventArgs e)
    {
        string sökOrd = search_input.Text; 
        var mainList = Global.Data.recipes;

        //Clears the list of any elements. Avoiding duplicates and a growing list
        if (SearchedRecipes != null || SearchedRecipes?.Count > 0) SearchedRecipes.Clear();
       
        if (sökOrd.Length != 0)
        {
            foreach (var r in mainList)
            {
                foreach (var tag in r.Tags)
                {
                    if (r.Title.ToLower().Contains(sökOrd.ToLower()) || tag.ToUpper() == sökOrd.ToUpper())
                    {
                        if (IsRecipeDuplicate(r) == false) SearchedRecipes.Add(r);
                    }
                }
            }
            SearchRecipePage.ItemsSource = SearchedRecipes.GroupBy(x => x.Title);
        }
    }
    private bool IsRecipeDuplicate(Recipes r)
    {
        foreach (var copy in SearchedRecipes)
        {
            if (copy == r) return true;
        }
        return false;
    }
}