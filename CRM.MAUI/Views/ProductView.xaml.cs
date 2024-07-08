using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;

[QueryProperty(nameof(ProductId), "productId")] //this is reciving the product Id
public partial class ProductView : ContentPage
{
    public int ProductId { get; set; }
    
	public ProductView()
	{
		InitializeComponent();
        
	}

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel).Add(); 
        Shell.Current.GoToAsync("//Management");
    }

   

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProductViewModel(ProductId);
       
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Management");
    }


}