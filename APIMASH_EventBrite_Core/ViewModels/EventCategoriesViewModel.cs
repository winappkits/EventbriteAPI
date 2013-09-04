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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIMASH_Core.ViewModels;
using APIMASH_Core.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using APIMASH_Core;
using System.Dynamic;
using APIMASH_Eventbrite_Core;

namespace APIMASH_EventBrite_Core.ViewModels
{
    public class EventCategoriesViewModel : ApiMashBaseViewModel
    {
        #region Constants

        public const int MAX_EVENTSPERCATEGORY = 15;
        
        #endregion

        #region Properties

        private ILocationService _locationProvider;
        public ILocationService LocationProvider
        {
            get { return _locationProvider; }
            set { _locationProvider = value; }
        }

        private bool _loadEvents = false;
        public bool LoadEvents
        {
            get { return _loadEvents; }
            set { SetProperty(ref _loadEvents, value, "LoadEvents"); }
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

        private ObservableCollection<EventCategoryViewModel> _eventCategories;
        public ObservableCollection<EventCategoryViewModel> EventCategories
        {
            get
            {

                if (_eventCategories == null)
                {
                    _eventCategories = new ObservableCollection<EventCategoryViewModel>();
                }

                return _eventCategories;
            }
            private set { _eventCategories = value; }
        }
        

        #endregion

        #region Commands
        private RelayCommand<string> _commandFindCurrentLocation;
        public ICommand FindCurrentLocation
        {
            get
            {

                if (_commandFindCurrentLocation == null)
                {
                    _commandFindCurrentLocation = new RelayCommand<string>((s) =>
                    {
                        return true;
                    }, async (s) =>
                    {
                        var result = await MyLocation.LoadDataAsync();
                    });
                }

                return _commandFindCurrentLocation;
            }
        }        
        #endregion

        #region LoadData Methods

        public override bool LoadData()
        {

            throw new NotImplementedException();
        }

        public async override Task<bool> LoadDataAsync()
        {
            var cat = new string[] { "conferences", "conventions", "entertainment", "fundraisers", "meetings", "other", "performances", "reunions", "sales", "seminars", "social", "sports", "tradeshows", "travel", "religion", "fairs", "food", "music", "recreation" };


            CreateEventCategoriesFromArray(cat);

            var result = await this.MyLocation.LoadDataAsync();

            if (this.LoadEvents)
            {
                result = result && await LoadEventsFromCategories(cat);

                //Remove categories w/o events....
                var emptyCats = from ec in this.EventCategories
                                where ec.EventsInCategory.Count() == 0
                                select ec;

                this.EventCategories.RemoveRange<EventCategoryViewModel>(emptyCats);
            }

            this.IsDataLoaded = result;

            return this.IsDataLoaded;
        } 

        #endregion

        #region Private Methods

        private void CreateEventCategoriesFromArray(string[] cat)
        {

            if (cat == null)
                throw new ArgumentNullException();

            this.EventCategories.Clear();

            this.EventCategories.AddRange(cat.Select((c, i) => new EventCategoryViewModel { Name = c, Id = i })
                                            .OrderBy<EventCategoryViewModel, string>(c => c.Name));

            LoadCategoriesImages();           

        }

        private async Task<bool> LoadEventsFromCategories(string[] cat)
        {
            if (cat == null)
                throw new ArgumentNullException();


            dynamic apiResult = await ApiMashInvoke.InvokeAsync<ExpandoObject>(
                                               ApiMashInvoke.ApiEndPointUriBuilder(ApiResources.EVENT_SEARCH,
                                                   new KeyValuePair<string, string>("app_key", ApiResources.APP_KEY),
                                                   new KeyValuePair<string, string>("longitude", this.MyLocation.Longitude.ToString()),
                                                   new KeyValuePair<string, string>("latitude", this.MyLocation.Latitude.ToString()),
                                                   new KeyValuePair<string, string>("max", MAX_EVENTSPERCATEGORY.ToString()),
                                                   new KeyValuePair<string, string>("within", "1"),
                                                   new KeyValuePair<string, string>("category", string.Join(",", cat))
                                                   ));

            var first = true;
            foreach (dynamic evnt in apiResult.events)
            {
                //As per the data model the first item in the event collection is a summary object, I am skipping it...                
                if (!first)
                {
                    var targetEvt = new EventViewModel();

                    targetEvt.LoadDataFromEventDTO(evnt.@event);

                    AddEventToCategory(targetEvt);
                }

                first = false;
            }

            return true;

        }

        private void AddEventToCategory(EventViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException();

            var cats = item.Categories.Split(',');

            if (cats.Length > 0)
            {
                foreach (var cat in cats)
                {
                    var eventCat = this.EventCategories
                                    .Select<EventCategoryViewModel, EventCategoryViewModel>(ec => ec)
                                    .Where<EventCategoryViewModel>(ec => ec.Name.ToLower().Equals(cat.ToLower()))
                                    .FirstOrDefault<EventCategoryViewModel>();

                    if (eventCat != null)
                        eventCat.EventsInCategory.Add(item);

                }
            }

        }

        private void LoadCategoriesImages()
        {
            foreach (var cat in this.EventCategories)
            {
                cat.ImageUrl = new Uri(cat.BaseUri,string.Format("{0}_asset.png", cat.Name));
            }
        }

        #endregion
       
    }
}
