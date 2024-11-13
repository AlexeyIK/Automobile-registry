namespace AutomobileRegisty__kursovaya_;

public partial class AddVehicle : ContentPage
{
	public AddVehicle()
	{
		InitializeComponent();
		LoadPickerData();
	}

	private void LoadPickerData()
	{
		using (var db = new ApplicationContext())
		{
			ManufacturerPicker.ItemsSource = db.Manufacturers.Select(m => m.Name).ToList();
			TypePicker.ItemsSource = db.VehicleTypes.Select(t => t.Name).ToList();
			ColorPicker.ItemsSource = db.Colors.Select(c => c.ColorName).ToList();
		}
	}

	private async void OnCreateBtnClicked(object sender, EventArgs e)
	{
		// ToDo: добавлять запись в БД
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

			var vehicle = new PostgreTest.Models.Vehicle
			{
				Manufacturer = manufacturer.Id,
				Model = ModelEntry.Text,
				Type = type.Id,
				Color = color.Id,
				Year = year,
				Vin = VinEntry.Text,
				EnginePower = power,
				EngineVolume = volume,
				Mass = mass
			};

			db.VehiclesList.Add(vehicle);
			await db.SaveChangesAsync();
			await Navigation.PopAsync();
		}
	}

	private async void OnCancelBtnClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		// ToDo: mark somehow
	}
}
