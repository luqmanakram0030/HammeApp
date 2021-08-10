using SuppliersApp.Models;
using SuppliersApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuppliersApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QRScanPage : ContentPage
	{
		public QRScanPage (clsShops clsShops)
		{
			InitializeComponent ();
			this.BindingContext = new QRScanViewModel(clsShops);
			
		}
	}
}