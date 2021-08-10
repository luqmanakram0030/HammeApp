using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliersApp.Models
{
   
    public class clsSalon
    {

        public int SalonId { get; set; }
        public string SalonName { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
        public Nullable<int> DistributorId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public string Remarks { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Category { get; set; }
        public Nullable<int> Status { get; set; }
        public string QRCode { get; set; }
        public string Type { get; set; }
        public string SalonClass { get; set; }
        public Nullable<int> CustomerHanleAtTime { get; set; }
        public Nullable<int> Staff { get; set; }
        public Nullable<int> CustomerHanleInDay { get; set; }
        public Nullable<int> SkinFacialInDay { get; set; }
        public Nullable<int> TotalFacialBeds { get; set; }
        public Nullable<int> HairCutsADay { get; set; }
        public Nullable<int> TotalChairs { get; set; }
        public Nullable<int> OfferDeals { get; set; }
        public Nullable<int> MassageInDay { get; set; }
        public string WhichBrands { get; set; }
        public string RetailProduct { get; set; }
        public string BenefitRetailProduct { get; set; }
        public string InterestInBrand { get; set; }
        public string AreaOfSalon { get; set; }
        public string WantAreaForImprovement { get; set; }
        public Nullable<int> Ambiance { get; set; }
        public Nullable<int> Displine { get; set; }
        public Nullable<int> Behaviour { get; set; }
        
    }
}
