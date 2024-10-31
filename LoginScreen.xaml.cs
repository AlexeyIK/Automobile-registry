namespace AutomobileRegisty__kursovaya_;

public partial class LoginScreen : ContentPage
{
	public LoginScreen()
	{
		InitializeComponent();
	}

	private async void OnSubmitBtnClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainMenu(), true);
	}
}

