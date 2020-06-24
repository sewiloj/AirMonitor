﻿using AirMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AirMonitor.ViewModels
{
    class DetailsViewModel : BaseViewModel
    {
        private Measurement _measurement;
        private int _caqiValue = 57;
        private string _caqiTitle = "Świetna jakość!";
        private string _caqiDescription = "Możesz bezpiecznie wyjść z domu bez swojej maski anty-smogowej i nie bać się o swoje zdrowie.";
        private int _pm25Value = 34;
        private int _pm25Percent = 137;
        private int _pm10Value = 67;
        private int _pm10Percent = 135;
        private double _humidityPercent = 0.95;
        private int _pressureValue = 1027;
        public Measurement Measurement
        {
            get => _measurement;
            set { SetProperty(ref _measurement, value);
                UpdateValues();
            } 
        }
        public int CaqiValue
        {
            get => _caqiValue;
            set => SetProperty(ref _caqiValue, value);
        }

        public string CaqiTitle
        {
            get => _caqiTitle;
            set => SetProperty(ref _caqiTitle, value);
        }

        public string CaqiDescription
        {
            get => _caqiDescription;
            set => SetProperty(ref _caqiDescription, value);
        }

        public int Pm25Value
        {
            get => _pm25Value;
            set => SetProperty(ref _pm25Value, value);
        }
        public int Pm25Percent
        {
            get => _pm25Percent;
            set => SetProperty(ref _pm25Percent, value);
        }
        public int Pm10Value
        {
            get => _pm10Value;
            set => SetProperty(ref _pm10Value, value);
        }
        public int Pm10Percent
        {
            get => _pm10Percent;
            set => SetProperty(ref _pm10Percent, value);
        }
        public double HumidityPercent
        {
            get => _humidityPercent;
            set => SetProperty(ref _humidityPercent, value);
        }
        public int PressureValue
        {
            get => _pressureValue;
            set => SetProperty(ref _pressureValue, value);
        }

        private void UpdateValues()
        {
            if (Measurement?.Current == null) return;
            var current = Measurement?.Current;
            var index = current.Indexes?.FirstOrDefault(c => c.Name == "AIRLY_CAQI");
            var values = current.Values;
            var standards = current.Standards;

            CaqiValue = (int)Math.Round(index?.Value ?? 0);
            CaqiTitle = index.Description;
            CaqiDescription = index.Advice;
            Pm25Value = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PM25")?.Value ?? 0);
            Pm10Value = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PM10")?.Value ?? 0);
            HumidityPercent = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "HUMIDITY")?.Value ?? 0);
            PressureValue = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PRESSURE")?.Value ?? 0);
            Pm25Percent = (int)Math.Round(standards?.FirstOrDefault(s => s.Pollutant == "PM25")?.Percent ?? 0);
            Pm10Percent = (int)Math.Round(standards?.FirstOrDefault(s => s.Pollutant == "PM10")?.Percent ?? 0);
        }
    }
}
