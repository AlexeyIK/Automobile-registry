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
			// Загрузка данных для выпадающих списков
			ManufacturerPicker.ItemsSource = db.Manufacturers.Select(m => m.Name).ToList();
			TypePicker.ItemsSource = db.VehicleTypes.Select(t => t.Name).ToList();
			ColorPicker.ItemsSource = db.Colors.Select(c => c.ColorName).ToList();
		}
	}

	private async void OnCreateBtnClicked(object sender, EventArgs e)
	{
		// Добавьте валидацию и сохранение данных
	}

	private async void OnCancelBtnClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}
