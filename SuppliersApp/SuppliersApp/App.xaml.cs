using SuppliersApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuppliersApp
{
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
           // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDE3ODA1QDMxMzgyZTM0MmUzMEd3TmpWZXFPR056Znl0SkM2Um9PMGhjbjk5dmFFU3B3RXBheC9uZXJtbTg9");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDIyNDM0QDMxMzkyZTMxMmUzMEtUU3Y3TGRlUU52T0xNL2hwbjdVTWVrREZSTldaNzR1cktoU0dLSk1lWE09");
            InitializeComponent();

           MainPage = new NavigationPage(new SplashPage());
           // MainPage = new NavigationPage(new ManuallyAddPage());
           //MainPage = new NavigationPage(new DashboardPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
