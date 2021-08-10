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
    
    public partial class ManuallyAddPage : ContentPage
    {
        ManuallyAddViewModel vm;
        public ManuallyAddPage(int id)
        {
            InitializeComponent();
            BindingContext = vm = new ManuallyAddViewModel(id);
        }


        private void manuallySearch_SearchButtonPressed(object sender, EventArgs e)
        {
            if (manuallySearch.Text.Length >= 3)
            {
                vm.SearchCommand.Execute(e);
            }
        }

    }
}