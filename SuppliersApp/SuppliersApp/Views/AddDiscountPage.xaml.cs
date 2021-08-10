using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SQLite;
using SuppliersApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuppliersApp.Views
{
   

    public partial class AddDiscountPage:PopupPage
{
        
        public clsItem itemList;
      
        public int id { get; set; }
        public AddDiscountPage(clsItem c)
        {
            InitializeComponent();
           
            this.itemList = c;
           
        }
        private async void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //if(Convert.ToString(DiscType.SelectedValue) == "%")
            //{
            //    Convert.ToInt32(DicEntry.Text);

            //}
            if (!string.IsNullOrEmpty(DicEntry.Text))
            {
                TaskButton.IsEnabled = true;
                if (!string.IsNullOrEmpty(Tax.Text))
                {
                    if (DiscType.Text == "Rs")
                    {
                        itemList.MRP = (itemList.FixRate - Convert.ToDecimal(DicEntry.Text)) * itemList.Quantity2;
                        TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                        itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                        itemList.MRP = itemList.ValueExTax;
                    }
                    else
                    {
                        
                            if (Convert.ToInt32(DicEntry.Text) > 100)
                            {
                                DicEntry.Text = null;
                                await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Dicount in % can't be greater than 100."));

                            }
                            else
                            {
                                var a = Convert.ToDecimal(DicEntry.Text);
                                var b = (a / 100) * itemList.FixRate;
                                itemList.Discount = b;
                                itemList.MRP = (itemList.FixRate - itemList.Discount) * itemList.Quantity2;
                                TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                                itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                                itemList.MRP = itemList.ValueExTax;
                        }
                        
                    }
                }
                else if (string.IsNullOrEmpty(DicEntry.Text))
                {


                    if (string.IsNullOrEmpty(Tax.Text) || Tax.Text == "0")
                    {
                        TaskButton.IsEnabled = false;
                        TaxExValue.Text = "";
                        itemList.GrossVal = 0;
                        itemList.ValueExTax = 0;
                    }
                    else if (!string.IsNullOrEmpty(Tax.Text))
                    {
                        itemList.MRP = itemList.FixRate * itemList.Quantity2;
                        TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                        itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                        itemList.MRP = itemList.ValueExTax;
                    }
                    

                }

            }
            else if (string.IsNullOrEmpty(DicEntry.Text)|| DicEntry.Text=="0")
            {
                if (!string.IsNullOrEmpty(Tax.Text))
                {
                   
                       
                    itemList.MRP = (itemList.FixRate) * itemList.Quantity2;
                    TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                    itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                    itemList.MRP = itemList.ValueExTax;
                }
            }
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            itemList.Discount=Convert.ToDecimal(DicEntry.Text);
            itemList.DiscountType = DiscType.Text;
            itemList.TaxValue= Convert.ToDecimal(Tax.Text);
            itemList.ValueExTax= Convert.ToDecimal(TaxExValue.Text);

            //if (DiscType.Text == "%")
            //{
            //    var a = ((itemList.Discount / 100) * itemList.NetRate) + data.TaxValue;
            //    data.MRP = (data.NetRate - a);
            //}
            //else
            //{
            //    var b = (data.NetRate - data.Discount) + data.TaxValue;
            //    data.MRP = b;
            //}

           
          await   PopupNavigation.PopAsync();
            MessagingCenter.Send<clsItem>(itemList, "GetItemList");
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();

        }

        private async void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            

            if (!string.IsNullOrEmpty(Tax.Text))
            {
                if (!string.IsNullOrEmpty(DicEntry.Text))
                {
                    if (DiscType.Text == "Rs")
                    {
                        itemList.MRP = (itemList.FixRate - Convert.ToDecimal(DicEntry.Text)) * itemList.Quantity2;
                        TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                        itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                        itemList.MRP = itemList.ValueExTax;
                    }
                    else
                    {
                        if (Convert.ToInt32(DicEntry.Text) > 100)
                        {
                            DicEntry.Text = null;
                            await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Dicount in % can't be greater than 100."));

                        }
                        else
                        {
                            var a = Convert.ToDecimal(DicEntry.Text);
                            var b = (a / 100) * itemList.FixRate;
                            itemList.Discount = b;
                            itemList.MRP = (itemList.FixRate - itemList.Discount) * itemList.Quantity2;
                            TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                            itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                            itemList.MRP = itemList.ValueExTax;
                        }
                    }
                }
                else
                {
                    itemList.MRP = itemList.FixRate * itemList.Quantity2;
                    TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                    itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                    itemList.MRP = itemList.ValueExTax;
                }
            }
            else if (string.IsNullOrEmpty(Tax.Text) || Tax.Text == "0")
            {
                TaxExValue.Text = "";
                itemList.GrossVal = 0;
                itemList.ValueExTax = 0;
            }
        }

        private async void Tax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Tax.Text))
            {
                if (!string.IsNullOrEmpty(DicEntry.Text))
                {
                    if (DiscType.Text == "Rs")
                    {
                        itemList.MRP = (itemList.FixRate - Convert.ToDecimal(DicEntry.Text)) * itemList.Quantity2;
                        TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                        itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                        itemList.MRP = itemList.ValueExTax;
                    }
                    else
                    {
                        if (Convert.ToInt32(DicEntry.Text) > 100)
                        {
                            DicEntry.Text = null;
                            await Application.Current.MainPage.Navigation.PushPopupAsync(new AlertPage("E", "Dicount in % can't be greater than 100."));

                        }
                        else
                        {
                            var a = Convert.ToDecimal(DicEntry.Text);
                            var b = (a / 100) * itemList.FixRate;
                            itemList.Discount = b;
                            itemList.MRP = (itemList.FixRate - itemList.Discount) * itemList.Quantity2;
                            TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                            itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                            itemList.MRP = itemList.ValueExTax;
                        }
                    }
                }
                else
                {
                    itemList.MRP = itemList.FixRate * itemList.Quantity2;
                    TaxExValue.Text = Convert.ToString(itemList.MRP - (Convert.ToDecimal(Tax.Text) * itemList.Quantity2));
                    itemList.ValueExTax = Convert.ToDecimal(TaxExValue.Text);
                    itemList.MRP = itemList.ValueExTax;
                }
            }
            else if (string.IsNullOrEmpty(Tax.Text) || Tax.Text == "0")
            {
                TaxExValue.Text = "";
                itemList.GrossVal = 0;
                itemList.ValueExTax = 0;
            }

        }
    }

}