using Postgres.Models;

namespace AutomobileRegisty__kursovaya_;

public partial class App : Application
{
    public static User CurrentUser { get; set; }

    public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}
}
