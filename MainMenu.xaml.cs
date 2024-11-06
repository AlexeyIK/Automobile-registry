using PostgreTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileRegisty__kursovaya_;

public partial class MainMenu : ContentPage
{
    public MainMenu()
    {
        InitializeComponent();
        LoadVehicles();
    }

    private void LoadVehicles()
    {
        using (var db = new ApplicationContext())
        {
            var carsList = db.VehiclesList
                .Include(v => v.ManufacturerNavigation)
                .Include(v => v.ColorNavigation)
                .Include(v => v.TypeNavigation)
                .Include(v => v.CreatedByNavigation)
                .ToList();

            if (carsList.Count > 0)
                VehiclesCollectionView.ItemsSource = carsList;
        }
    }

    private void VehiclesCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Vehicle selectedVehicle)
        {
            DisplayAlert("Выбран", $"{selectedVehicle.Manufacturer} {selectedVehicle.Model}", "OK");
        }
    }
}
