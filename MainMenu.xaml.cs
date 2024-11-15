using PostgreTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutomobileRegisty__kursovaya_;

public partial class MainMenu : ContentPage
{
    private User m_CurrentUser;
    private ObservableCollection<Vehicle> m_Vehicles = new();

    private bool m_IsRefreshing;

    public ICommand RowTappedCommand { get; private set; }

    public MainMenu(User currentUser)
    {
        InitializeComponent();
        m_CurrentUser = currentUser;

        // пробрасываем юзернейм и роль в шапку
        UserName.Text = $"{m_CurrentUser.FamilyName} {m_CurrentUser.FirstName[..1]}. ({m_CurrentUser.RoleNavigation.Name})";

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
                bool edit = await DisplayAlert(
                    "Действие", 
                    $"Выбран автомобиль:\n" +
                    $"Производитель: {vehicle.ManufacturerNavigation?.Name}\n" +
                    $"Модель: {vehicle.Model}\n" +
                    $"VIN: {vehicle.Vin}", 
                    "Редактировать",
                    "Отмена");

                if (edit)
                {
                    await OpenEditForm(vehicle);
                }
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
        await Navigation.PushAsync(new AddVehicle(m_CurrentUser), true);
    }

    private async Task OpenEditForm(Vehicle vehicle)
    {
        try
        {
            // Загружаем свежие данные из БД
            using (var db = new ApplicationContext())
            {
                var updateVehicle = await db.VehiclesList
                    .Include(v => v.ManufacturerNavigation)
                    .Include(v => v.ColorNavigation)
                    .Include(v => v.TypeNavigation)
                    .Include(v => v.CreatedByNavigation)
                    .FirstOrDefaultAsync(v => v.Id == vehicle.Id);

                if (updateVehicle != null)
                {
                    // Проверяем права на редактирование
                    if (m_CurrentUser.Role == 1)
                    {
                        await Navigation.PushAsync(new AddVehicle(m_CurrentUser, updateVehicle), true);
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", 
                            "У вас нет прав на редактирование этой записи", 
                            "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка", 
                        "Запись не найдена в базе данных", 
                        "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", 
                $"Не удалось открыть форму редактирования: {ex.Message}", 
                "OK");
        }
    }
}
