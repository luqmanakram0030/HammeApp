using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using SuppliersApp.iOS.Renderers;
using SuppliersApp.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RadioButton_Renderer), typeof(RadioButton_IOS))]

namespace SuppliersApp.iOS.Renderers
{
   
        public class RadioButton_IOS : ButtonRenderer
        {
            protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                base.OnElementPropertyChanged(sender, e);

                Control.TintColor = UIColor.White;
            }
        }
    
}