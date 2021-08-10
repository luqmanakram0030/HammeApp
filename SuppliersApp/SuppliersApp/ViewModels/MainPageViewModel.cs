using Microcharts;
using Rg.Plugins.Popup.Extensions;
using SkiaSharp;
using SuppliersApp.Models;
using SuppliersApp.Services;
using SuppliersApp.Utlities;
using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
    public class MainPageViewModel:BaseViewModel
    {
        private clsSummery _clsSummery;
        public clsSummery clsSummery 
        {
            get { return _clsSummery; }
            set { _clsSummery = value;
                OnPropertyChanged();
            }
        } 
        public GetSummeryValueService getSummeryValue;
        private Chart _PieChart;

        public Chart PieChartQty
        {
            get { return _PieChart; }
            set { _PieChart = value;
                OnPropertyChanged();
            }
        } 
        private Chart _PieChartValue;

        public Chart PieChartValue
        {
            get { return _PieChartValue; }
            set {
                _PieChartValue = value;
                OnPropertyChanged();
            }
        }

        private bool _Isbusy;

        public bool Isbusy
        {
            get
            {
                return _Isbusy;
            }
            set
            {
                _Isbusy = value;
                if (_Isbusy)
                {
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new IndicatorActity());

                }
                else
                {
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PopPopupAsync();

                }

                OnPropertyChanged();
            }
        }
        private string _Attendancebtn;

        public string Attendancebtn
        {
            get { return _Attendancebtn; }
            set { _Attendancebtn = value;
                OnPropertyChanged();
            }
        }

        private INavigation navigation;
        public readonly SalesEmplyeesAttendanceApi emplyeesAttendance;
       // public ICommand AttendanceCommand;
        public MainPageViewModel(INavigation navigation)
        {
            
           
            getSummeryValue = new GetSummeryValueService();
            SetbtnValue();
            getSummeryValues();
          
            emplyeesAttendance = new SalesEmplyeesAttendanceApi();
            clsSummery = new clsSummery();
            this.navigation = navigation;
          //  AttendanceCommand = new Command(async () => await AttendanceApiAsync());
        }
        List<ChartEntry> entries = new List<ChartEntry>
        {
            new ChartEntry(200)
            {
                Color=SKColor.Parse("#1F77B4"),
                Label ="Acheive Quantity",
                ValueLabel = "200"
            },
            new ChartEntry(400)
            {
                Color = SKColor.Parse("#FF7F0E"),
                Label = "",
                ValueLabel = ""
            },

            };
        private async Task getSummeryValues()
        {
            clsSummery = await getSummeryValue.GetValueAsync();
            List<ChartEntry> ChartQty = new List<ChartEntry>
        {
            new ChartEntry(clsSummery.Summary.AcheiveQtyPers)
            {
                Color=SKColor.Parse("#1F77B4"),
                Label ="Acheive Quantity",
                ValueLabel = clsSummery.Summary.AcheiveQtyPers.ToString(),
            },
           new ChartEntry(100)
            {
                Color = SKColor.Parse("#D62728"),
                Label = "",
                ValueLabel = "10"
            },


            };
            List<ChartEntry> ChartValue = new List<ChartEntry>
        {
            new ChartEntry(clsSummery.Summary.AcheiveValuePers)
            {
                Color=SKColor.Parse("#FF1943"),
                Label ="Acheive Amount",
                ValueLabel = clsSummery.Summary.AcheiveValuePers.ToString(),
            },
            new ChartEntry(100)
            {
                Color = SKColor.Parse("#FF7F0E"),
                Label = "",
                ValueLabel = ""
            },

            };

            PieChartQty = new PieChart() { Entries = ChartQty };
            PieChartValue = new PieChart() { Entries = ChartValue };

        }

        private async void SetbtnValue()
        {
            Attendancebtn = await Utilty.GetSecureStorageValueFor(Utilty.Attendancebtn);
            if (Attendancebtn == null)
            {
                Attendancebtn = "Start Day";
                await Utilty.SetSecureStorageValue(Utilty.Attendancebtn, "Start Day");
            }
           
        }

        public Command AttendanceCommand
        {
            get
            {
                return new Command(async() =>
                {
                var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        Models.Result result = new Models.Result();
                        Isbusy = true;
                        result = await emplyeesAttendance.AttendanceApi(Attendancebtn);
                        if (result.Status)
                        {
                            Isbusy = false;
                            await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", result.Message));
                        }
                        else
                        {
                            Attendancebtn = await Utilty.GetSecureStorageValueFor(Utilty.Attendancebtn);
                            Isbusy = false;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Please connect your device with internet."));

                    }

                });
            }
        }
       
        public Command NavigateNewShopPage
        {
            get
            {
                return new Command(() =>
                {
                // navigation.PushAsync(new NewShopPage());

                });
            }
        }
        public Command NavigateSearchShopPage
        {
            get
            {
                return new Command(() =>
                {
                   // navigation.PushAsync(new ShopSearchPage());

                });
            }
        }
        public Command NavigateToMenuPage
        {
            get
            {
                return new Command(() =>
                {
                    navigation.PushAsync(new MenuPage());

                });
            }
        }
    }
}
