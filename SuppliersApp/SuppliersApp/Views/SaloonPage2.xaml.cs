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
    public partial class SaloonPage2 : ContentPage
    {
        public SaloonPage2()
        {
            InitializeComponent();
        }
        private void RadioButton_Clicked(object sender, EventArgs e)
        {



        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SaloonPage2());
        }
    }
}