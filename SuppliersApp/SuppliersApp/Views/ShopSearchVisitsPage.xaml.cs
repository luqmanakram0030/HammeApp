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
    public partial class ShopSearchVisitsPage : ContentPage
    {
        ShopSearchVisitsVM vm;
        public ShopSearchVisitsPage()
        {
            InitializeComponent();
            this.BindingContext = vm = new ShopSearchVisitsVM(Navigation);
        }
        private void searchbarShop_SearchButtonPressed(object sender, EventArgs e)
        {
            try
            {
                if (searchbarShop.Text.Length >= 3)
                {
                    vm.SearchCommand.Execute(e);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}