namespace MauiThemes;

public partial class CustomPage : ContentPage
{
	public CustomPage()
	{
		InitializeComponent();

		BindingContext = new CustomViewModel();
	}
}
