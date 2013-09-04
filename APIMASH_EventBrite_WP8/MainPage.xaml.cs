using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using APIMASH_EventBrite_WP8.Resources;
using APIMASH_EventBrite_Core.ViewModels;
using System.Windows.Controls.Primitives;
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


using APIMASH_Core.ViewModels;
using System.Text;
using APIMASH_EventBrite_WP8.Services;

namespace APIMASH_EventBrite_WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        EventCategoriesViewModel _model = new EventCategoriesViewModel() { LocationProvider = new WP8LocationService() };

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (!_model.LocationProvider.HasUserOptedIn())
            {
                var result = MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK;

                _model.LocationProvider.SaveLocationConsent(result);
            }

            if (!_model.IsDataLoaded)
            {
                Utils.ShowHideProgressBar(this.progress, true);

                var data = await _model.LoadDataAsync();
                DataContext = _model;

                Utils.ShowHideProgressBar(this.progress, false);

            }

        }
       
     
        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var selectedCategory = ((TextBlock)sender).DataContext as EventCategoryViewModel;

            NavigateToEventsByCategory(selectedCategory);

        }

        private void NavigateToEventsByCategory(EventCategoryViewModel category)
        {
            if (category == null)
                throw new ArgumentNullException();

            NavigationService.Navigate(new Uri(string.Format("/EventsInCategory.xaml?location={0}&latitude={1}&longitude={2}&category={3}", _model.MyLocation.Name, _model.MyLocation.Latitude, _model.MyLocation.Longitude, category.Name)
                                            , UriKind.Relative));
        }

    }
}