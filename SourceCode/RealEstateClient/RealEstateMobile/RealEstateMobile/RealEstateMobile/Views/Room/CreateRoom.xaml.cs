using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstateMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRoom : PopupPage
    {
        public CreateRoom()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAllPopupAsync();
        }
    }
}