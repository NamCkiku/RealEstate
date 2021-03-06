﻿using RealEstateMobile.ViewModels;
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
    public partial class RoomDetailPage : ContentPage
    {
        public RoomDetailPage()
        {
            InitializeComponent();
            BindingContext = new RoomViewModel(this);
        }
    }
}