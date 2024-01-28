namespace Kokboken.Views;

public partial class RemoveRecipe : ContentPage
{
	public RemoveRecipe()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}