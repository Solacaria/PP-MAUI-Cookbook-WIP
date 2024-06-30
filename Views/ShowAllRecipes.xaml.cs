using Kokboken.Globals;
using Kokboken.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kokboken.Views;

public partial class ShowAllRecipes : ContentPage
{
   
    public ShowAllRecipes()
	{
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        var groupedlist = Global.Data.recipes.ToList().GroupBy(z => z.Title);
        GroupedRecipes.ItemsSource = groupedlist;
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
    /// <summary>
    /// Asks user if he wants to go to the clicked recipe.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void GroupedRecipes_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Recipes recept = (Recipes)e.Item;
        List<Recipes> recipes = new List<Recipes>();

        if (recipes.Count > 0) recipes.Clear(); //Making sure the list doesnt get infinately big during 1 session. 
        recipes.Add(recept);

        string result = await DisplayActionSheet(recept.Title, "Ja", "Nej", null, "Vill du gå till receptet?");
        if (result == "Nej") return;
        if (result == "Ja")
        {
            //Checks to see if the rndRecipe has an object or not. Using same View as random recipe for simplicity sake
            if (Global.Data.rndRecipe.Count > 0)
            {
                //Sets the random recipe holder to the clicked recipe.
                Global.Data.rndRecipe.Remove(Global.Data.rndRecipe[0]);
            }
            Global.Data.rndRecipe.Add(recipes[0]);
            await Shell.Current.GoToAsync("//ShowSingleRecipe");
        }
    }
    //TODO Lägg till sökfunktion i listan. Kanske även specifik sortering?
}