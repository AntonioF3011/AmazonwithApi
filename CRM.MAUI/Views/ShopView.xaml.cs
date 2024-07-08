using CRM.MAUI.ViewModels;
using CRM.Library.Services;
namespace CRM.MAUI.Views;
public partial class ShopView : ContentPage
{
    public ShopView()
    {
        InitializeComponent();
        BindingContext = new ShopViewModel();
    }

    private void ManageInventoryClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Management");
    }

    private void ShopClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Cart");
    }
    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void DescriptionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductViewModel; //bind it to ProductViewModel so you can use the object 'Model' 
        (BindingContext as ShopViewModel)?.ViewProduct(product);
        
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel)?.RefreshProducts();
    }

    private void InventorySearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.Search();
    }

    private async void AddToCartClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductViewModel;
        if (product != null)
        {
            await (BindingContext as ShopViewModel)?.AddToCart(product);
        }
    }


    private void AmazonClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.RefreshProducts();
    }

    private void RemoveFromCart_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductViewModel;
        if (product != null)
        {
            (BindingContext as ShopViewModel)?.RemoveFromCart(product);

        }
    }
}
