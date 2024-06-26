using Kokboken.Globals;
using Kokboken.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;

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
        BackgroundImageSource = "background.jpg";

        var recept = Global.Data.rndRecipe.GroupBy(x => x.Title);
        SingleRecipePage.ItemsSource = recept;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //Goes to Main menu
		await Shell.Current.GoToAsync("//MainPage");
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        //Randomizes a new recipe and reloads the page
        Global.RandomRecipe();
        this.OnAppearing();
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        //Takes current recipe and moves to edit page.
        await Shell.Current.GoToAsync("//EditRecipe");
    }
}