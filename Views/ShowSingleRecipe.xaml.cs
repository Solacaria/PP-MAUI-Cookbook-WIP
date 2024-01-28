namespace Kokboken.Views;

public partial class ShowSingleRecipe : ContentPage
{
	public ShowSingleRecipe()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("//MainPage");
    }
}