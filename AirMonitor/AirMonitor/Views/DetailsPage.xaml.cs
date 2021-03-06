﻿using AirMonitor.Models;
using AirMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AirMonitor
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Measurement measurement)
        {
            InitializeComponent();

            var dvm = BindingContext as DetailsViewModel;
            dvm.Measurement = measurement;
        }

        private void HelpButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Co to jest CAQI?", "CAQI jest liczbą w skali od 1 do 100, gdzie niska wartość oznacza dobrą jakość powietrza oraz wysoka wartość oznacza złą jakość powietrza.", "Zamknij");
        }
    }
}
