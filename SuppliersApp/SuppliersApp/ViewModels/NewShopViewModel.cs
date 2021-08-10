using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using SuppliersApp.Helpers;
using SuppliersApp.Models;
using SuppliersApp.Services;
using SuppliersApp.Utlities;
using SuppliersApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SuppliersApp.ViewModels
{
    public class NewShopViewModel : BaseViewModel
    {
        #region 
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
                    navigation1.PushPopupAsync(new IndicatorActity());

                }
                else
                {
                    navigation1.PopPopupAsync();

                }

                OnPropertyChanged();
            }
        }

        private string _Attendancebtn;

        public string Attendancebtn
        {
            get { return _Attendancebtn; }
            set
            {
                _Attendancebtn = value;
                OnPropertyChanged();
            }
        }

        private bool _IsVisible = false;

        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _shopNameVisible = false;

        public bool shopNameVisible
        {
            get { return _shopNameVisible; }
            set
            {
                _shopNameVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _contactPersonVisible = false;

        public bool contactPersonVisible
        {
            get { return _contactPersonVisible; }
            set
            {
                _contactPersonVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _emailVisible = false;

        public bool emailVisible
        {
            get { return _emailVisible; }
            set
            {
                _emailVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _phoneVisible = false;

        public bool phoneVisible
        {
            get { return _phoneVisible; }
            set
            {
                _phoneVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ImagesSave> _imageObj;
        public ObservableCollection<ImagesSave> imageObj
        {
            get { return _imageObj; }
            set
            {
                _imageObj = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ImagesSaveBase64> _imageBase64;
        public ObservableCollection<ImagesSaveBase64> imageBase64
        {
            get { return _imageBase64; }
            set
            {
                _imageBase64 = value;
                OnPropertyChanged();
            }
        }
        public int ShopId { get; set; }
        public ICommand takePhoto { get; }
        public ICommand pickPhoto { get; }

        //  public readonly SendImageApi sendImageApi;



        public readonly SalesEmplyeesAttendanceApi emplyeesAttendance;

        private readonly NewShopPage newShopPage;
        public bool res { get; set; }


        public Result result;

        public clsShops clsShops { get; set; } = new clsShops();

        public readonly NewShopRegisterApi newShopApi;

        public ICommand ShopRegisterCommand { get; }
        public ICommand QrScanCommand { get; }


        private INavigation navigation1;
        #endregion


        public NewShopViewModel(NewShopPage newShopPage, INavigation navigation)
        {
            imageObj = new ObservableCollection<ImagesSave>();

            imageBase64 = new ObservableCollection<ImagesSaveBase64>();

            // IsVisible = false;

            SetbtnValue();
            this.navigation1 = navigation;
            this.newShopPage = newShopPage;

            //sendImageApi = new SendImageApi();


            newShopApi = new NewShopRegisterApi();

            result = new Result();

            emplyeesAttendance = new SalesEmplyeesAttendanceApi();

            ShopRegisterCommand = new Command(async () => await ShopRegisterAsync());

            QrScanCommand = new Command(async () => await ScanAsync());

            takePhoto = new Command(async () => await takePhotoFromCam());

            pickPhoto = new Command(async () => await PickPhotoFromStorage());
        }

        public Command<ImagesSave> delpicCmd
        {
            get
            {
                return new Command<ImagesSave>(async (ImagesSave imgsave) =>
                {
                    imageObj.Remove(imgsave);

                    if (imageObj.Count == 0)
                    {
                        IsVisible = false;
                    }

                });
            }
        }

        private async Task PickPhotoFromStorage()
        {


            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });


            if (file == null)
                return;
            if (file != null)
            {
                try
                {
                    IsVisible = true;
                    ImageSource image = ImageSource.FromStream(() =>
                    {
                        var a = file.GetStream();
                        return a;
                    });


                    //  Isbusy = true;
                    var stream = file.GetStream();

                    var bytes = new byte[stream.Length];
                    await stream.ReadAsync(bytes, 0, (int)stream.Length);

                    imageObj.Add(new ImagesSave
                    {
                        images = image,
                        //imgConvertBase64 = Convert.ToBase64String(bytes)
                    });

                    imageBase64.Add(new ImagesSaveBase64
                    {
                        imgConvertBase64 = Convert.ToBase64String(bytes)
                        //imgConvertBase64 = Convert.ToBase64String(bytes)
                    });

                    //imagePick.Source = ImageSource.FromStream(() =>
                    //{
                    //    var a = file.GetStream();
                    //    return a;
                    //});

                    //  imagePick.Source = imageS[0];

                    //var stream = file.GetStream();

                    //var bytes = new byte[stream.Length];
                    //await stream.ReadAsync(bytes, 0, (int)stream.Length);

                    // await Application.Current.MainPage.DisplayAlert("FilePath", file.Path, "Ok");


                    // image = Convert.ToBase64String(bytes);


                    //clsShops.ImagePath.Add(new clsShops
                    //{
                    //    //ImagePath = a
                    //});

                    //res = await sendImageApi.SendImagesAsync(ImageString, ShopId);
                    // Isbusy = false;
                    //if (res)
                    //{

                    //    Application.Current.MainPage = new NavigationPage(new BarcodePage(ShopId));
                    //}

                }
                catch (Exception ex)
                {
                    // Isbusy = false;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));
                }

            }


        }

        public async Task takePhotoFromCam()
        {
            IsVisible = true;

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,

                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;
            if (file != null)
            {
                try
                {
                    IsVisible = true;
                    ImageSource image = ImageSource.FromStream(() =>
                    {
                        var a = file.GetStream();
                        return a;
                    });

                    //  Isbusy = true;
                    var stream = file.GetStream();
                    var bytes = new byte[stream.Length];
                    await stream.ReadAsync(bytes, 0, (int)stream.Length);

                    imageObj.Add(new ImagesSave
                    {
                        images = image,
                        //  imgConvertBase64= Convert.ToBase64String(bytes)
                    });

                    imageBase64.Add(new ImagesSaveBase64
                    {
                        imgConvertBase64 = Convert.ToBase64String(bytes)
                        //imgConvertBase64 = Convert.ToBase64String(bytes)
                    });

                    //imagePick.Source = imageS[0];

                    //0 var stream = file.GetStream();
                    // var bytes = new byte[stream.Length];
                    //await stream.ReadAsync(bytes, 0, (int)stream.Length);
                    // string ImageString = Convert.ToBase64String(bytes);


                    //  await Application.Current.MainPage.DisplayAlert("FilePath",file.Path,"Ok");

                    //clsShops.ImagePath1 = Convert.ToBase64String(bytes);


                    //   res = await sendImageApi.SendImagesAsync(ImageString, ShopId);

                    // if (res)
                    // {
                    //    Isbusy = false;
                    // Application.Current.MainPage.DisplayAlert("Messge","Successfully Uploaded","Ok");
                    //Application.Current.MainPage = new NavigationPage(new BarcodePage(ShopId));
                    //}

                    //if (res)
                    //{
                    //    //    Isbusy = false;
                    //    Application.Current.MainPage = new NavigationPage(new BarcodePage(ShopId));
                    //}

                }
                catch (Exception ex)
                {
                    //  Isbusy = false;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));
                }
                //if (... )
            }
        }


        private async Task ScanAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new QRScanPage(clsShops));
        }

        private async void SetbtnValue()
        {
            Attendancebtn = await Utilty.GetSecureStorageValueFor(Utilty.Attendancebtn);
            if (Attendancebtn == null)
            {
                Attendancebtn = "Start Day";
                await Utilty.SetSecureStorageValue(Utilty.Attendancebtn, "Start Day");
            }
            //else
            //{
            //    Attendancebtn = await Utilty.GetSecureStorageValueFor(Utilty.Attendancebtn);
            //}

            //Attendancebtn = await Utilty.GetSecureStorageValueFor(Utilty.Attendancebtn);
        }
        public async Task ShopRegisterAsync()
        {
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    if (clsShops.Email != null)
                    {
                        clsShops.Email = clsShops.Email.Trim();
                    }

                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);
                    clsShops.Latitude = location.Latitude.ToString();
                    clsShops.Longitude = location.Longitude.ToString();

                    if (!ValidationHelper.IsFormValid(clsShops, newShopPage)) { return; }

                    else
                    {
                        Isbusy = true;



                        result = await newShopApi.RegisterShopAsync(clsShops, imageBase64);

                        if (result.Status)
                        {
                            Isbusy = false;
                            await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("S", result.Message));
                            //  await sendImageApi.SendImagesAsync(imageObj, ShopId);
                            //  await navigation1.PushAsync(new StoreShopImages(result.ShopId));
                        }
                        else
                        {
                            Isbusy = false;
                            await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", result.Message));
                        }
                    }
                }

                else
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Please connect your device with internet."));

                }
            }
            catch (Exception ex)
            {
                Isbusy = false;
                await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", ex.Message));

            }


        }
        public Command AttendanceCommand
        {
            get
            {
                return new Command(async () =>
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
    }
    public class ImagesSave
    {
        public ImageSource images { get; set; }
        //public string imgConvertBase64 { get; set; }
    }
    public class ImagesSaveBase64
    {
        //public ImageSource images { get; set; }
        public string imgConvertBase64 { get; set; }
    }
}
