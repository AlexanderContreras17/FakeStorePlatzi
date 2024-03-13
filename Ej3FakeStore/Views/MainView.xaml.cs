namespace FakeStorePlatzi.Views;

public partial class MainView : ContentPage
{
	public MainView()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var agregarView = new AgregarView();
        agregarView.BindingContext = BindingContext; // Asigna el mismo ViewModel que el de la vista principal
        await Navigation.PushAsync(agregarView);
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var productoview = new VerProductoView();
        productoview.BindingContext = BindingContext; // Asigna el mismo ViewModel que el de la vista principal
        await Navigation.PushAsync(productoview);
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var productoview = new EditarView();
        productoview.BindingContext = BindingContext; // Asigna el mismo ViewModel que el de la vista principal
        await Navigation.PushAsync(productoview);
    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        var productoview = new EliminarView();
        productoview.BindingContext = BindingContext; // Asigna el mismo ViewModel que el de la vista principal
        await Navigation.PushAsync(productoview);
    }
}