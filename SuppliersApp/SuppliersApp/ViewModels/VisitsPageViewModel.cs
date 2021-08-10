using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Models;
using SuppliersApp.Services;
using SuppliersApp.Utlities;
using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
   public class VisitsPageViewModel:BaseViewModel
    {
        private bool _Followupcheck;

        public bool Followupcheck
        {
            get { return _Followupcheck; }
            set { _Followupcheck = value;
                OnPropertyChanged();
            }
        }
        private clsVisit _Visit;

        public clsVisit Visit
        {
            get { return _Visit; }
            set
            {
                _Visit = value;
                OnPropertyChanged();
            }
        }

        public readonly FollowupService followupService;
        public clsShops clsShops { get; set; }
        public VisitsPageViewModel(clsShops selectedShop)
        {
            followupService = new FollowupService();
            this.clsShops = selectedShop;
            Visit = new clsVisit();
            Visit.FollowupDate = DateTime.Now;
        }
        public Command FollowupSaveCmd
        {

            get
            {
                return new Command(async () =>
                {
                   if( !Followupcheck)
                    {
                        Visit.FollowupDate = null;
                        Visit.FollowupType = 0;
                    }
                    else
                    {
                        Visit.FollowupType = 1;
                    }
                    Visit.ShopId = clsShops.ShopID;
                    Visit.ShopName = clsShops.ShopName;
                    string id = await Utilty.GetSecureStorageValueFor(Utilty.UserId);
                    Visit.EmployeeId = Convert.ToInt32(id);
                    Visit.EmployeeName =  await Utilty.GetSecureStorageValueFor(Utilty.UserName);

               Result result=     await FollowupService.SaveDataVisitAsync(Visit);
                    if (result.Status)
                    {
                        Application.Current.MainPage = new NavigationPage(new MenuPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", result.Message));
                    }
              

                });
            }
        }
    }
}
