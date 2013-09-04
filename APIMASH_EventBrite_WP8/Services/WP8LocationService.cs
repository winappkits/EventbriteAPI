//-----------------------------------------------------------------------------
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//
//-----------------------------------------------------------------------------


using APIMASH_Core.Services;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Windows.Devices.Geolocation;

namespace APIMASH_EventBrite_WP8.Services
{
    class WP8LocationService : ILocationService
    {
        private Geolocator _geolocator = new Geolocator();

        public WP8LocationService()
        {

        }

        public bool HasUserOptedIn()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                return (bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"];
            }

            return false;

        }


        public void SaveLocationConsent(bool value)
        {

            IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = value;

            IsolatedStorageSettings.ApplicationSettings.Save();


        }

        private async Task<Geoposition> GetInnerGeoPositionAsync()
        {

            if (!HasUserOptedIn() || _geolocator.LocationStatus == PositionStatus.Disabled)
                return null;

            
            _geolocator.DesiredAccuracyInMeters = 10;

            Geoposition geo = await _geolocator.GetGeopositionAsync();


            return geo;
        }

        public async Task<string> GetCoordinatesAsync()
        {
            try
            {
                var geo = await GetInnerGeoPositionAsync();

                if (geo == null)
                    return string.Empty;


                return string.Format("{0} , {1}", geo.Coordinate.Latitude.ToString("0.00"), geo.Coordinate.Longitude.ToString("0.00"));

            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {

                    throw new Exception("the application does not have the right capability or the location master switch is off");

                }
                else
                {
                    throw ex;
                }
            }
        }

        public async Task<string> GetLocationAsync()
        {
            try
            {
                var geo = await GetInnerGeoPositionAsync();

                if (geo == null || geo.CivicAddress == null)
                    return string.Empty;

                return geo.CivicAddress.City;

            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {

                    throw new Exception("the application does not have the right capability or the location master switch is off");

                }
                else
                {
                    throw ex;
                }
            }
        }

    }


    public class GeoCoordinateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString() == string.Empty)
                return null;

            var coor = value.ToString().Split(',');

            return new System.Device.Location.GeoCoordinate() { Latitude = double.Parse(coor[0]), Longitude = double.Parse(coor[1]) };

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var geo = (System.Device.Location.GeoCoordinate)value;

            return string.Format("{0},{1}", geo.Latitude, geo.Longitude);
        }
    }

}
