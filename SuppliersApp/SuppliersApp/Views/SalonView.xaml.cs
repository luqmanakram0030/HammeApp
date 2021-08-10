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
    public partial class SalonView : ContentPage
    {
        public SalonView()
        {
            InitializeComponent();
            BindingContext = new SalonViewVM();
        }
    }
}