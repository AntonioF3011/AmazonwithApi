using CRM.MAUI.ViewModels;
using CRM.Library.Services;
namespace CRM.MAUI.Views;

public partial class ProductManagementView : ContentPage
{
    public ProductManagementView()
    {
        InitializeComponent();
        BindingContext = new ProductManagementViewModel();

    }

    private void EditClicked(object sender, EventArgs e)
    {

        (BindingContext as ProductManagementViewModel).UpdateProduct();
    }


    private void HomeClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ProductManagementViewModel).RefreshProducts();
    }

   

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductManagementViewModel).DeleteProducts();
        
    }
    private void InlineDelete_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ProductManagementViewModel).RefreshProducts();
    }

    private void PaymentConfigurationClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PaymentConfig");
    }
}
