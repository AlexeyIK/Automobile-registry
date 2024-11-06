namespace AutomobileRegisty__kursovaya_;

public partial class LoginScreen : ContentPage
{
    private const string EMPTY_FIELD_ALERT = "Необходимо заполнить поля \"Логин\" и \"Пароль\"!";
    private const string WRONG_LOGINPASSORD_ALERT = "Неверные логин/пароль";

    private string mLogin = "admin";
    private string mPassword = "1234";

    public LoginScreen()
    {
        InitializeComponent();
    }

    private void LoginPasswordTextChanged(object sender, EventArgs e)
    {
        if (AlertLabel.IsVisible)
            AlertLabel.IsVisible = false;
    }

    private async void OnSubmitBtnClicked(object sender, EventArgs e)
    {
        if (String.IsNullOrWhiteSpace(LoginEntry.Text) || String.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            AlertLabel.IsVisible = true;
            AlertLabel.Text = EMPTY_FIELD_ALERT;
            return;
        }

        if (LoginEntry.Text != mLogin || PasswordEntry.Text != mPassword)
        {
            AlertLabel.IsVisible = true;
            AlertLabel.Text = WRONG_LOGINPASSORD_ALERT;
            return;
        }

        await Navigation.PushAsync(new MainMenu(), true);
    }

    void PasswordEntry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        OnSubmitBtnClicked(sender, e);
    }
}

