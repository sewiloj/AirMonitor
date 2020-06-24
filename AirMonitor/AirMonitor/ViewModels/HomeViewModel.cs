using AirMonitor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private ICommand _detailsCommand;
        private List<Measurement> _items;

        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Initialize();
        }

        public ICommand DetailsCommand => _detailsCommand ?? (_detailsCommand = new Command<Measurement>(GoToDetails));
        public List<Measurement> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private void GoToDetails(Measurement measurement)
        {
            _navigation.PushAsync(new DetailsPage(measurement));
        }


        private async Task Initialize()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            var location = await GetLocation();
            var installations = await GetInstallations(location);
            var measurements = await GetMeasurementsForInstallations(installations);
            Items = new List<Measurement>(measurements);
        }

        private async Task<Location> GetLocation()
        {
            try
            {
                Location location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    location = await Geolocation.GetLocationAsync(request);
                }

                if (location != null)
                {
                        var type = location.Latitude.GetType(); 
                    System.Diagnostics.Debug.WriteLine(location.Latitude);
                    System.Diagnostics.Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}, location_get_success");
                }

                return location;
            }
            catch (FeatureNotSupportedException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (FeatureNotEnabledException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (PermissionException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return null;
        }

        private async Task<IEnumerable<Installation>> GetInstallations(Location location, double maxDistance = 3, int maxResults = 1)
        {
            string url = $"installations/nearest?lat={location.Latitude}&lng={location.Longitude}&maxDistanceKM={maxDistance}&maxResults={maxResults}";
            if (location == null)
            {
                System.Diagnostics.Debug.WriteLine("Location error");
                return null;
            }
            var response = await GetHttpResponse<IEnumerable<Installation>>(url);
            System.Diagnostics.Debug.WriteLine("GetInstallations complete.");
            return response;
        }

        private async Task<IEnumerable<Measurement>> GetMeasurementsForInstallations(IEnumerable<Installation> installations)
        {
            if (installations == null)
            {
                System.Diagnostics.Debug.WriteLine("No installations data.");
                return null;
            }
            var measurements = new List<Measurement>();
            foreach (var installation in installations)
            {
                string url = $"measurements/installation?installationId={installation.Id}";
                var response = await GetHttpResponse<Measurement>(url);

                if (response != null)
                {
                    response.Installation = installation;
                    response.CurrentDisplayValue = (int)Math.Round(response.Current?.Indexes?.FirstOrDefault()?.Value ?? 0);
                    measurements.Add(response);
                }
            }
            System.Diagnostics.Debug.WriteLine("GetMeasurementsForInstallations complete.");
            return measurements;
        }

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://airapi.airly.eu/v2/")
            };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Accept-Language", "pl");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            client.DefaultRequestHeaders.Add("apikey", "8yoYVYdRUjpijI9S1ToY6QIZdDUEHDu2");

            return client;
        }

        private async Task<T> GetHttpResponse<T>(string url)
        {
            try
            {
                HttpClient client = GetHttpClient();
                var response = await client.GetAsync(url);

                if (response.Headers.TryGetValues("X-RateLimit-Limit-day", out var dayLimit) && response.Headers.TryGetValues("X-RateLimit-Remaining-day", out var dayLimitRemaining))
                {
                    System.Diagnostics.Debug.WriteLine($"Day limit: {dayLimit?.FirstOrDefault()}, remaining: {dayLimitRemaining?.FirstOrDefault()}");
                }

                switch ((int)response.StatusCode)
                {
                    case 200:
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(content);
                        return result;
                    case 429:
                        System.Diagnostics.Debug.WriteLine("Too many requests");
                        break;
                    default:
                        var errorContent = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine($"Response error: {errorContent}");
                        return default;
                }
            }
            catch (JsonReaderException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return default;
        }
    }
}
