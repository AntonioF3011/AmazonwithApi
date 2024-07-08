namespace CRM.MAUI.Views;
using CRM.MAUI.ViewModels;
[QueryProperty(nameof(ProductId), "productId")] //this is reciving the product Id
public partial class DescriptionView : ContentPage
{
    public int ProductId { get; set; }
    public DescriptionView()
	{
		InitializeComponent();
	}
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProductViewModel(ProductId);
    }

    private void BackHomeClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void DescriptionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductViewModel; //bind it to ProductViewModel so you can use the object 'Model' 
        (BindingContext as ProductViewModel)?.ViewProduct(product);

    }
   
    private async void AddToCartClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductViewModel;
        if (product != null)
        {
            await (BindingContext as ProductViewModel)?.AddToCart(product);
        }
    }
    private void InventorySearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel)?.Search();
    }
    private void ShopClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Cart");
    }
}