using Country.ViewModel;

namespace Country;

public partial class MainPage : ContentPage
{


	public MainPage()
	{
		InitializeComponent();
		BindingContext = new CountryViewModel();
	}
}

