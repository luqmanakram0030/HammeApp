using SuppliersApp.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SuppliersApp.Models
{
    public class clsShops : BaseViewModel
    {
        public int ShopID { get; set; }
        [Required]
        public string ShopName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public int? DistributorID { get; set; }
        public int? AreaID { get; set; }

        public string Remarks { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Category { get; set; }
        public int? Status { get; set; }

        private string _QRCode;

        public string QRCode
        {
            get { return _QRCode; }
            set
            {
                _QRCode = value;
                OnPropertyChanged();
            }
        }

        // public string QRCode { get; set; }
        public string CNIC { get; set; }
        public string STRN { get; set; }
        public string NTN { get; set; }
        public string Address { get; set; }
        public string TaxType { get; set; }
        public string ShopClass { get; set; }
        public decimal? ShopWorth { get; set; }
        public string ImagePath { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
    }
}
