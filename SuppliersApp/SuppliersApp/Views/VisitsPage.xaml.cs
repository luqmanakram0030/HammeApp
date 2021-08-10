using SuppliersApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuppliersApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitsPage : ContentPage
    {
        public VisitsPage(Models.clsShops selectedShop)
        {
            InitializeComponent();
            BindingContext = new VisitsPageViewModel(selectedShop);
        }
    }
}