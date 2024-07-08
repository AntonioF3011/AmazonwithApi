namespace CRM.MAUI.Views;
using CRM.MAUI.ViewModels;
//this view  is to allow the navigation between descriptions


[QueryProperty(nameof(ProductId), "productId")] //this is reciving the product Id
public partial class DescriptionForFunctionality : ContentPage
{
    public DescriptionForFunctionality()
    {
        InitializeComponent();
    }

    public int ProductId { get; set; }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProductViewForFunctionalityModel(ProductId);
    }

    private void BackHomeClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void DescriptionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductViewForFunctionalityModel;
        (BindingContext as ProductViewForFunctionalityModel)?.ViewProduct(product);
    }
    private async void AddToCartClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductViewForFunctionalityModel;
        if (product != null)
        {
            await (BindingContext as ProductViewForFunctionalityModel)?.AddToCart(product);
        }
    }

    private void ShopClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Cart");
    }
}