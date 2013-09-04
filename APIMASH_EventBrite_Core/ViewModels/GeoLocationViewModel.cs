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
using APIMASH_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMASH_EventBrite_Core.ViewModels
{
    public class GeoLocationViewModel : ApiMashBaseViewModel
    {
        #region Properties

        private ILocationService _locationProvider;
        public ILocationService LocationProvider
        {
            get { return _locationProvider; }
            set { _locationProvider = value; }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value, "Longitude"); }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value, "Latitude"); }
        }

        public string Coordinates
        {
            get { return string.Format("{0}, {1}", this.Latitude, this.Longitude); }
        } 

        #endregion

        #region LoadData Methods
        public async override Task<bool> LoadDataAsync()
        {
            if (LocationProvider == null)
                return false;

            this.Name = await this.LocationProvider.GetLocationAsync();

            var coordinates = await this.LocationProvider.GetCoordinatesAsync();

            if (!string.IsNullOrEmpty(coordinates))
            {
                var split = coordinates.Split(',');
                this.Latitude = double.Parse(split[0]);
                this.Longitude = double.Parse(split[1]);


                this.Name = "Current Location";

            }

            this.IsDataLoaded = true;

            return true;
        }

        public override bool LoadData()
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
