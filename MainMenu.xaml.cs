namespace AutomobileRegisty__kursovaya_;

public partial class MainMenu : ContentPage
{
    public MainMenu()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ShowAutos();
    }

    private void ShowAutos()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var carsList = db.VehiclesList.ToList();
            if (carsList.Count > 0)
                EnterLabel.IsVisible = false;

            foreach (var car in carsList)
            {
                var elem = new Label() { Text = $"{car.Model} [{car.Vin}]", TextColor = Colors.DarkCyan, FontSize=12, MaximumWidthRequest=300 };
                elem.Parent = InfoStack;
            }
        }
    }
}
