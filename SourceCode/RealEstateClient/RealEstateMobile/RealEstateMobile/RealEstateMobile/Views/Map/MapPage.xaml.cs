using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace RealEstateMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            mapCanvas.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.0029317912212212, 105.820226663232323), Distance.FromKilometers(5)));
            var pin = new Pin
            {
                Position = new Position(21.0029317912212212, 105.820226663232323),
                Label = "Ngã Tư Sở",
                Address = "Ngã Tư Sở"
            };
            var pin2 = new Pin
            {
                Position = new Position(21.0029317912212212, 105.620226663232323),
                Label = "Ngã Tư Sở",
                Address = "Ngã Tư Sở"
            };
            var pin3 = new Pin
            {
                Position = new Position(21.0029317912212212, 105.420226663232323),
                Label = "Ngã Tư Sở",
                Address = "Ngã Tư Sở"
            };
            mapCanvas.Pins.Add(pin);
            mapCanvas.Pins.Add(pin2);
            mapCanvas.Pins.Add(pin);
        }
    }
}