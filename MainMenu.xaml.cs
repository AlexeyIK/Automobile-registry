using PostgreTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutomobileRegisty__kursovaya_;

public partial class MainMenu : ContentPage
{
    private User CurrentUser;
    private ObservableCollection<Vehicle> m_Vehicles = new();

    private bool m_IsRefreshing;

    public ICommand RowTappedCommand { get; private set; }

    public MainMenu(User currentUser)
    {
        InitializeComponent();
        CurrentUser = currentUser;

        // пробрасываем юзернейм и роль в шапку
        UserName.Text = $"{CurrentUser.FamilyName} {CurrentUser.FirstName[..1]}. ({CurrentUser.RoleNavigation.Name})";

        // Биндим коллекцию в CollectionView
        VehiclesCollectionView.ItemsSource = m_Vehicles;

        // Команда обновления списка
        RefreshCollectionView.Command = new Command(() =>
        {
            LoadVehicles();
            RefreshCollectionView.IsRefreshing = m_IsRefreshing;
        });

        // Команда тапа на элемент списка
        RowTappedCommand = new Command<Vehicle>(async (vehicle) =>
        {
            if (vehicle != null)
            {
                await DisplayAlert("Выбран автомобиль", 
                    $"Производитель: {vehicle.ManufacturerNavigation?.Name}\n" +
                    $"Модель: {vehicle.Model}\n" +
                    $"VIN: {vehicle.Vin}", 
                    "OK");
            }
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadVehicles();
    }

    private void LoadVehicles()
    {
        m_IsRefreshing = true;

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

            m_IsRefreshing = false;
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
