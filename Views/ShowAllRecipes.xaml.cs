using Kokboken.Globals;

namespace Kokboken.Views;

public partial class ShowAllRecipes : ContentPage
{
   
    public ShowAllRecipes()
	{
		InitializeComponent();
        var groupedlist = Global.Data.recipes.ToList().GroupBy(z => z.Title);
        GroupedRecipes.ItemsSource = groupedlist;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
       
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}