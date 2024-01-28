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

        private async void rmv_recipe_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//RemoveRecipe");
        }

        private async void show_recipe_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ShowAllRecipes");
        }

        private async void add_recipe_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddRecipe");
        }

        private async void rnd_recipe_Clicked(object sender, EventArgs e)
        {
            //TODO se till att den först visar ett slumpat recept, därpå frågar användaren med en DisplayAlert om man vill gå till recept, isf kör GoToAsync
            var recept = Global.Data.recipes;
            var rnd = new Random();
            int idx = rnd.Next(0, recept.Count);
            var recipe = recept[idx];

            string dictName = "SlumpRecipe";
            IDictionary<string, Recipes> newRandom = new Dictionary<string, Recipes>();
            newRandom[dictName] = recipe;

            string result = await DisplayActionSheet($"{recipe.Title}\nVill du gå till receptet?", "Avbryt",  null, "Ja");
            if (result == "Ja") await Shell.Current.GoToAsync("//ShowSingleRecipe", newRandom);
            return;

        }
    }

}
