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
	}

	private async void OnCancelBtnClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		if (picker.SelectedIndex != -1)
		{
			var parent = (Border)picker.Parent.Parent;
			parent.Stroke = Colors.DarkCyan;
		}
	}
}
