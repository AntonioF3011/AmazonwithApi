using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;

public partial class CartView : ContentPage
{
	public CartView()
	{
		InitializeComponent();
		BindingContext = new CartViewModel();
	}

    private void BackClicked(object sender, EventArgs e)
    {

		Shell.Current.GoToAsync("//MainPage");
    }
    private void ShopClicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel)?.RefreshProducts();
    }

    private void RemoveFromCart_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductViewModel;
        if (product != null)
        {
            (BindingContext as CartViewModel)?.RemoveFromCart(product);
            
        }
    }

    private void AddCart_Clicked(object sender, EventArgs e)
    {
       
            (BindingContext as CartViewModel)?.AddCart();
  
    }

    private void Checkout_Clicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel)?.Checkout();

    }
}