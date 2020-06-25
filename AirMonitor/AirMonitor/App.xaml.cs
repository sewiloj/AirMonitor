using AirMonitor.Views;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirMonitor
{
    public partial class App : Application
    {
        public static string ApiUrl { get; private set; }
        public static string MeasurementUrl { get; private set; }
        public static string InstallationUrl { get; private set; }
        public static string ApiKey { get; private set; }
        public App()
        {
            InitializeComponent();
            LoadConfig();
            MainPage = new MainTabbedPage();
        }
        public static void LoadConfig()
        {
            // Determine path
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var configName = resourceNames.FirstOrDefault(s => s.Contains("config.json"));

            using (Stream stream = assembly.GetManifestResourceStream(configName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var jsonStr = JObject.Parse(json);
                
                ApiUrl = jsonStr["ApiUrl"].Value<string>();
                MeasurementUrl = jsonStr["MeasurementUrl"].Value<string>();
                InstallationUrl = jsonStr["InstallationUrl"].Value<string>();
                ApiKey = jsonStr["ApiKey"].Value<string>();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
