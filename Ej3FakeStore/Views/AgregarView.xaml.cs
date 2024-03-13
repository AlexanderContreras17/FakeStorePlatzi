using FakeStorePlatzi.ViewModels;

namespace FakeStorePlatzi.Views;

public partial class AgregarView : ContentPage
{
	public AgregarView()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		var datacontext = this.BindingContext as FakeStoreViewModel;
    }
}