using PostgreTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace AutomobileRegisty__kursovaya_;

public partial class MainMenu : ContentPage
{
    private ObservableCollection<Vehicle> m_Vehicles;

    public MainMenu()
    {
        InitializeComponent();
        m_Vehicles = new ObservableCollection<Vehicle>();
        VehiclesCollectionView.ItemsSource = m_Vehicles;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        LoadVehicles();
        base.OnNavigatedTo(args);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
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

            m_Vehicles.Clear();
            foreach (var car in carsList)
            {
                m_Vehicles.Add(car);
            }
        }
    }

    private void VehiclesCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Vehicle selectedVehicle)
        {
            DisplayAlert("Выбран", $"{selectedVehicle.Manufacturer} {selectedVehicle.Model}", "OK");
        }
    }

    private async void OnAddBtnClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddVehicle(), true);
    }
}
