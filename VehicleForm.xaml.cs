﻿using Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileRegisty__kursovaya_;

public partial class VehicleForm : ContentPage
{
    private readonly User m_CurrentUser;
    private readonly Vehicle m_VehicleToEdit;
    private readonly bool m_IsEditMode;

    public VehicleForm(User currentUser, Vehicle vehicleToEdit = null)
    {
        InitializeComponent();
        m_CurrentUser = currentUser;
        m_VehicleToEdit = vehicleToEdit;
        m_IsEditMode = vehicleToEdit != null;

        Title = m_IsEditMode ? "Редактирование записи" : "Добавление записи";
        CreateBtn.Text = m_IsEditMode ? "Сохранить" : "Создать";
        // пробрасываем юзернейм и роль в шапку
        UserName.Text = $"{m_CurrentUser.FamilyName} {m_CurrentUser.FirstName[..1]}. ({m_CurrentUser.RoleNavigation.Name})";

        LoadPickersData();

        CreateBtn.IsVisible = !m_IsEditMode;
        UpdateBtn.IsVisible = m_IsEditMode;
        DeleteBtn.IsVisible = m_IsEditMode;

        if (m_IsEditMode)
            LoadVehicleData();
    }

    private void LoadPickersData()
    {
        using (var db = new ApplicationContext())
        {
            ManufacturerPicker.ItemsSource = db.Manufacturers.Select(m => m.Name).ToList();
            TypePicker.ItemsSource = db.VehicleTypes.Select(t => t.Name).ToList();
            ColorPicker.ItemsSource = db.Colors.Select(c => c.ColorName).ToList();
        }
    }

    private void LoadVehicleData()
    {
        ManufacturerPicker.SelectedItem = m_VehicleToEdit.ManufacturerNavigation?.Name;
        ModelEntry.Text = m_VehicleToEdit.Model;
        TypePicker.SelectedItem = m_VehicleToEdit.TypeNavigation?.Name;
        ColorPicker.SelectedItem = m_VehicleToEdit.ColorNavigation?.ColorName;
        YearEntry.Text = m_VehicleToEdit.Year.ToString();
        VinEntry.Text = m_VehicleToEdit.Vin;
        PowerEntry.Text = m_VehicleToEdit.EnginePower.ToString();
        VolumeEntry.Text = m_VehicleToEdit.EngineVolume.ToString();
        MassEntry.Text = m_VehicleToEdit.Mass.ToString();
        NumberEntry.Text = m_VehicleToEdit.Number?.ToUpper();
    }

    private async void OnSaveBtnClicked(object sender, EventArgs e)
    {
        using (var db = new ApplicationContext())
        {
            var manufacturer = db.Manufacturers.FirstOrDefault(m => m.Name == ManufacturerPicker.SelectedItem.ToString());
            var type = db.VehicleTypes.FirstOrDefault(t => t.Name == TypePicker.SelectedItem.ToString());
            var color = db.Colors.FirstOrDefault(c => c.ColorName == ColorPicker.SelectedItem.ToString());

            if (manufacturer == null || type == null || color == null ||
                string.IsNullOrEmpty(ModelEntry.Text) || string.IsNullOrEmpty(YearEntry.Text) ||
                string.IsNullOrEmpty(VinEntry.Text) || string.IsNullOrEmpty(PowerEntry.Text) ||
                string.IsNullOrEmpty(VolumeEntry.Text) || string.IsNullOrEmpty(MassEntry.Text))
            {
                AlertLabel.Text = "Пожалуйста, заполните все поля";
                AlertLabel.IsVisible = true;
                return;
            }

            if (!short.TryParse(YearEntry.Text, out short year) ||
                !short.TryParse(PowerEntry.Text, out short power) ||
                !short.TryParse(VolumeEntry.Text, out short volume) ||
                !short.TryParse(MassEntry.Text, out short mass))
            {
                AlertLabel.Text = "Пожалуйста, введите корректные числовые значения";
                AlertLabel.IsVisible = true;
                return;
            }

            var vehicle = new Vehicle
            {
                Id = m_IsEditMode ? m_VehicleToEdit.Id : 0,
                Manufacturer = manufacturer.Id,
                Model = ModelEntry.Text,
                Type = type.Id,
                Color = color.Id,
                Year = year,
                Vin = VinEntry.Text,
                EnginePower = power,
                EngineVolume = volume,
                Mass = mass,
                Number = NumberEntry.Text
            };

            if (m_IsEditMode)
            {
                var vehicleInDB = db.VehiclesList.AsNoTracking().FirstOrDefault(v => v.Id == vehicle.Id);
                if (vehicleInDB != null)
                {
                    vehicle.Creator = vehicleInDB.Creator;
                    vehicle.CreatedAt = vehicleInDB.CreatedAt;
                    vehicle.OwnedBy = vehicleInDB.OwnedBy;

                    vehicle.Editor = m_CurrentUser.Id;
                    vehicle.EditedAt = DateTime.Now;

                    db.Entry(vehicleInDB).State = EntityState.Detached;
                    db.VehiclesList.Update(vehicle);
                }
            }
            else
            {
                vehicle.CreatorNavigation = m_CurrentUser;
                vehicle.CreatedAt = DateTime.Now;

                db.VehiclesList.Add(vehicle);
            }

            await db.SaveChangesAsync();
            await Navigation.PopAsync();
        }
    }

    private async void OnDeleteBtnClicked(object sender, EventArgs e)
    {
        using (var db = new ApplicationContext())
        {
            if (m_VehicleToEdit != null)
            {
                var deleteConfirm = await DisplayAlert("Удаление", "Вы уверены, что хотите безвозвратно удалить запись?", "Да", "Отмена");
                if (deleteConfirm)
                {
                    db.VehiclesList.Remove(m_VehicleToEdit);
                    await db.SaveChangesAsync();
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Запись не найдена", "OK");
            }
        }
    }

    private async void OnCancelBtnClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
