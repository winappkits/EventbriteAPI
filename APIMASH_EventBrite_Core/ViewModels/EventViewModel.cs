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


using APIMASH_Core;
using APIMASH_Core.Services;
using APIMASH_Core.ViewModels;
using APIMASH_Eventbrite_Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMASH_EventBrite_Core.ViewModels
{
    public class EventViewModel : ApiMashBaseViewModel
    {
        #region Constants

        public const int SHORTNAME_LENGTH = 25; 
        
        #endregion

        #region Properties
        private Uri _logoUrl;
        public Uri LogoUrl
        {
            get { return _logoUrl; }
            set { SetProperty(ref _logoUrl, value, "LogoUrl"); }
        }

        private ILocationService _locationProvider;
        public ILocationService LocationProvider
        {
            get { return _locationProvider; }
            set { _locationProvider = value; }
        }

        private GeoLocationViewModel _myLocation;
        public GeoLocationViewModel MyLocation
        {
            get
            {
                if (_myLocation == null)
                {
                    _myLocation = new GeoLocationViewModel() { LocationProvider = LocationProvider };
                }
                return _myLocation;
            }
            set { _myLocation = value; }
        }

        private ObservableCollection<GeoLocationViewModel> _locations;
        public ObservableCollection<GeoLocationViewModel> Locations
        {
            get
            {

                if (_locations == null)
                {
                    _locations = new ObservableCollection<GeoLocationViewModel>();
                }

                return _locations;
            }
            set { _locations = value; }
        }

        private Uri _eventUrl;
        public Uri EventUrl
        {
            get { return _eventUrl; }
            set { SetProperty(ref _eventUrl, value, "EventUrl"); }
        }

        private string _categories;
        public string Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value, "Categories"); }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value, "StartDate"); }
        }

        public String StartDateFormatted
        {
            get
            {
                return _startDate.ToString("ddd, dd MMM yyyy hh:mm");
            }
        }
        public string ShortName
        {
            get
            {

                if (string.IsNullOrEmpty(this.Name))
                    return string.Empty;

                if (this.Name.Length <= SHORTNAME_LENGTH)
                    return this.Name;

                return this.Name.Substring(0, SHORTNAME_LENGTH) + "...";
            }
        } 
        #endregion

        #region Validation

        protected override string ValidateProperty(object value, string propertyName = null)
        {
            if (propertyName == "Id")
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return "The event Id required.";
                }
            }

            return string.Empty;
        }
        
        #endregion

        #region LoadData Methods

        public async override Task<bool> LoadDataAsync()
        {
            if (this.HasErrors) return false;

            dynamic apiResult = await ApiMashInvoke.InvokeAsync<ExpandoObject>(
                                                ApiMashInvoke.ApiEndPointUriBuilder(ApiResources.EVENT_GET,
                                                    new KeyValuePair<string, string>("app_key", ApiResources.APP_KEY),
                                                    new KeyValuePair<string, string>("id", this.Id.ToString())));


            this.Locations.Clear();

            LoadDataFromEventDTO(apiResult.@event);

            this.IsDataLoaded = await this.MyLocation.LoadDataAsync();

            return this.IsDataLoaded;

        }

        internal void LoadDataFromEventDTO(dynamic eventDTO)
        {
            var dic = (IDictionary<string, object>)eventDTO;

            this.Id = eventDTO.id;
            this.Name = eventDTO.title;
            this.Description = eventDTO.description;
            this.StartDate = DateTime.Parse(eventDTO.start_date);


            if (dic.ContainsKey("logo"))
                this.LogoUrl = new Uri(eventDTO.logo);

            if (dic.ContainsKey("url"))
                this.EventUrl = new Uri(eventDTO.url);

            if (dic.ContainsKey("category"))
                this.Categories = eventDTO.category;

            if (dic.ContainsKey("venue"))
            {
                dynamic venue = eventDTO.venue;
                this.Locations.Clear();
                this.Locations.Add(new GeoLocationViewModel() { Name = this.Name, Latitude = venue.latitude, Longitude = venue.longitude });

            }

        }

        public override bool LoadData()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
