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
    public class EventCategoryViewModel : ApiMashBaseViewModel
    {
        #region Properties

        private Uri _baseUri = new Uri("ms-appx:///Assets/");
        public Uri BaseUri
        {
            get { return _baseUri; }
            set { SetProperty(ref _baseUri, value, "BaseUri"); }
        }

        private Uri _ImageUrl;
        public Uri ImageUrl
        {
            get { return _ImageUrl; }
            set { SetProperty(ref _ImageUrl, value, "ImageUrl"); }
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

        private ObservableCollection<EventViewModel> _eventsInCategory;
        public ObservableCollection<EventViewModel> EventsInCategory
        {
            get
            {

                if (_eventsInCategory == null)
                {
                    _eventsInCategory = new ObservableCollection<EventViewModel>();

                }
                return _eventsInCategory;
            }
            private set { _eventsInCategory = value; }
        } 
        
        #endregion

        #region LoadData Methods

        public async override Task<bool> LoadDataAsync()
        {

            if (this.MyLocation.Longitude == 0 && this.MyLocation.Latitude == 0) return false;

            dynamic apiDTO = await ApiMashInvoke.InvokeAsync<ExpandoObject>(
                                               ApiMashInvoke.ApiEndPointUriBuilder(ApiResources.EVENT_SEARCH,
                                                   new KeyValuePair<string, string>("app_key", ApiResources.APP_KEY),
                                                   new KeyValuePair<string, string>("longitude", this.MyLocation.Longitude.ToString()),
                                                   new KeyValuePair<string, string>("latitude", this.MyLocation.Latitude.ToString()),
                                                   new KeyValuePair<string, string>("max", "20"),
                                                   new KeyValuePair<string, string>("within", "1"),
                                                   new KeyValuePair<string, string>("category", this.Name.ToString())
                                                   ));



            var first = true;
            EventsInCategory.Clear();
            foreach (dynamic evnt in apiDTO.events)
            {
                //As per the data model the first item in the event collection is a summary object, I am skipping it...                
                if (!first)
                {
                    var targetEvt = new EventViewModel() { MyLocation = this.MyLocation };

                    targetEvt.LoadDataFromEventDTO(evnt.@event);

                    EventsInCategory.Add(targetEvt);

                }

                first = false;
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
