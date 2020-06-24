using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AirMonitor.ViewModels
{
    class DetailsViewModel : INotifyPropertyChanged
    {
        private int _caqiValue = 57;
        private string _caqiTitle = "Świetna jakość!";
        private string _caqiDescription = "Możesz bezpiecznie wyjść z domu bez swojej maski anty-smogowej i nie bać się o swoje zdrowie.";
        private int _pm25Value = 34;
        private int _pm25Percent = 137;
        private int _pm10Value = 67;
        private int _pm10Percent = 135;
        private double _humidityValue = 0.95;
        private int _pressureValue = 1027;
        public int CaqiValue
        {
            get => _caqiValue;
            set
            {
                if (_caqiValue != value)
                {
                    _caqiValue = value;
                    OnPropertyChanged("CaqiValue");
                }
            }
        }

        public string CaqiTitle
        {
            get => _caqiTitle;
            set
            {
                if (_caqiTitle != value)
                {
                    _caqiTitle = value;
                    OnPropertyChanged("CaqiTitle");
                }
            }
        }

        public string CaqiDescription
        {
            get => _caqiDescription;
            set
            {
                if (_caqiDescription != value)
                {
                    _caqiDescription = value;
                    OnPropertyChanged("CaqiDescription");
                }
            }
        }

        public int Pm25Value
        {
            get => _pm25Value;
            set
            {
                if (_pm25Value != value)
                {
                    _pm25Value = value;
                    OnPropertyChanged("Pm25Value");
                }
            }
        }
        public int Pm25Percent
        {
            get => _pm25Percent;
            set
            {
                if (_pm25Percent != value)
                {
                    _pm25Percent = value;
                    OnPropertyChanged("Pm25Percent");
                }
            }
        }
        public int Pm10Value
        {
            get => _pm10Value;
            set
            {
                if (_pm10Value != value)
                {
                    _pm10Value = value;
                    OnPropertyChanged("Pm10Value");
                }
            }
        }
        public int Pm10Percent
        {
            get => _pm10Percent;
            set
            {
                if (_pm10Percent != value)
                {
                    _pm10Percent = value;
                    OnPropertyChanged("Pm10Percent");
                }
            }
        }
        public double HumidityValue
        {
            get => _humidityValue;
            set
            {
                if (_humidityValue != value)
                {
                    _humidityValue = value;
                    OnPropertyChanged("HumidityValue");
                }
            }
        }
        public int PressureValue
        {
            get => _pressureValue;
            set
            {
                if (_pressureValue != value)
                {
                    _pressureValue = value;
                    OnPropertyChanged("PressureValue");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
