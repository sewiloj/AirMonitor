using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel
    {
        private readonly INavigation _navigation;
        private ICommand _detailsCommand;
        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        public ICommand DetailsCommand => _detailsCommand ?? (_detailsCommand = new Command(GoToDetails));

        private void GoToDetails()
        {
            _navigation.PushAsync(new DetailsPage());
        }
    }
}
