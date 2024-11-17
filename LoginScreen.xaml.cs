using Microsoft.EntityFrameworkCore;
using Postgres.Models;

namespace AutomobileRegisty__kursovaya_;

public partial class LoginScreen : ContentPage
{
    private const string EMPTY_FIELD_ALERT = "Необходимо заполнить поля \"Логин\" и \"Пароль\"!";
    private const string WRONG_LOGINPASSORD_ALERT = "Неверные логин/пароль";
    private const string NO_USER_FOUND_ALERT = "Пользователь с таким логином не существует!";

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

        using (var db = new ApplicationContext())
        {
            var user = db.Users.Include(v => v.RoleNavigation).FirstOrDefault(o => o.Login == LoginEntry.Text);
            if (user == null)
            {
                AlertLabel.IsVisible = true;
                AlertLabel.Text = NO_USER_FOUND_ALERT;
                return;
            }

            if (user.Password != PasswordEntry.Text.Trim())
            {
                AlertLabel.IsVisible = true;
                AlertLabel.Text = WRONG_LOGINPASSORD_ALERT;
                return;
            }

            await Navigation.PushAsync(new MainMenu(user), true);
        }
    }

    void PasswordEntry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        OnSubmitBtnClicked(sender, e);
    }
}

