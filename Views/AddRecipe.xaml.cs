using Kokboken.Globals;
using Kokboken.ViewModels;

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

        string newTitle = AddTitle.Text;
        string newDesc = AddDesc.Text;
        string newIngr = AddIngr.Text;

        enDifficultyDish? newDiff = NewDiffEnum();
        enSpeedDish? newSpeed = NewSpeedEnum();

        Recipes newRecipe = new Recipes { Title = newTitle, Description = newDesc, Ingredients = newIngr, enDiff = newDiff, enSpeed = newSpeed };
        Global.Data.recipes.Add(newRecipe);

        AddTitle.Text = "";
        AddDesc.Text = "";
        AddIngr.Text = "";
        
        await DisplayAlert("Nytt Recept", $"{newRecipe.Title} sparad", "OK");
        Global.SerilizeJson();
        await Shell.Current.GoToAsync("//MainPage");

    }
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
}