using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;



using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SuppliersApp.ViewModels;

namespace SuppliersApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
             BindingContext = new MainPageViewModel(Navigation);
         


        }
    }
}