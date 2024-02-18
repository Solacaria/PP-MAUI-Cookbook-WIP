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
		await Shell.Current.GoToAsync("//MainPage");
    }
}