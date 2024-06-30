using Kokboken.Globals;
using Kokboken.ViewModels;
using System.Security.Cryptography.X509Certificates;
namespace Kokboken
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (File.Exists(Global.fname("Receptlista.json"))) Global.Data.recipes = Global.DeserializeJson();
        }
        private void exit_Clicked(object sender, EventArgs e)
        {
            Global.SerilizeJson();
            Environment.Exit(0 );
        }
        private async void show_recipe_Clicked(object sender, EventArgs e)
        {
            var list = Global.Data.recipes;
            if (list == null || list.Count == 0)
            {
                await DisplayAlert("Receptlistan är tom", "Finns inga recept att visa", "OK");
            }
            else
            {
                await Shell.Current.GoToAsync("//ShowAllRecipes");
            }
        }
        private async void add_recipe_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddRecipe");
        }
        private async void rnd_recipe_Clicked(object sender, EventArgs e)
        {
            var list = Global.Data.recipes;
            if (list == null || list.Count == 0)
            {
                await DisplayAlert("Receptlistan är tom", "Finns inget att slumpa fram", "OK");
            }
            else
            {
                SlumpasIgen:
                Global.RandomRecipe();
                string result = await ShowRandomizedRecipe();

                if (result == "Ja") await Shell.Current.GoToAsync("//ShowSingleRecipe");
                if (result == "Slumpa igen") goto SlumpasIgen;
                return;
            }
        }
        private async Task<string> ShowRandomizedRecipe()
        {
            string result = await DisplayActionSheet($"{Global.Data.rndRecipe[0].Title}\nVill du gå till receptet?", "Avbryt",null,  "Slumpa igen", "Ja");
            return result;
        }
    }
}
