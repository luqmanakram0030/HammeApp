using SuppliersApp.Models;
using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
   public class SalonViewVM
    {
        public clsSalon clsShops { get; set; }

        public SalonViewVM()
        {
            clsShops = new clsSalon();
        }
        public Command QuestionnaireCommand
        {
            get
            {
                return new Command(async() =>
                {
                   
                   await Application.Current.MainPage.Navigation.PushModalAsync(new QuestionnaireView(clsShops));
                });
            }
        }
    }
}
