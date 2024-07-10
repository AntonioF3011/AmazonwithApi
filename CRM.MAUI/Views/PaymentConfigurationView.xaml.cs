namespace CRM.MAUI.Views;
using CRM.MAUI.ViewModels;
public partial class PaymentConfigurationView : ContentPage
{
	public PaymentConfigurationView()
	{
		InitializeComponent();
        BindingContext = new PaymentConfigurationViewModel();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Management");
    }
}