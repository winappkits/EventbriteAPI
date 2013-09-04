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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using APIMASH_EventBrite_Core.ViewModels;

namespace APIMASH_EventBrite_WP8
{
    public partial class EventsInCategory : PhoneApplicationPage
    {
        private EventCategoryViewModel _model = new EventCategoryViewModel();

        public EventsInCategory()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Utils.ShowHideProgressBar(this.progress, true);

            DataContext = null;

            SetModelPropertiesFromQS();

            if( await _model.LoadDataAsync())            
                DataContext = _model;

            Utils.ShowHideProgressBar(this.progress, false);
            
        }

        private  void SetModelPropertiesFromQS()
        {

            _model.Name = Utils.GetValueFromQueryString<string>(this, string.Empty, (v) => v, "category");
            _model.MyLocation.Name = Utils.GetValueFromQueryString<string>(this, string.Empty, (v) => v, "location");
            _model.MyLocation.Latitude = Utils.GetValueFromQueryString<double>(this, 0, (v) => double.Parse(v), "latitude");
            _model.MyLocation.Longitude = Utils.GetValueFromQueryString<double>(this, 0, (v) => double.Parse(v), "longitude");



        }

        private void EventsLongListSelector_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var evnt = (EventViewModel)this.EventsLongListSelector.SelectedItem;

            if (evnt == null)
                throw new ArgumentNullException();

            if (_model.HasErrors)
            {
                MessageBox.Show(_model.GetErrorsFlattened("\n\r"), "Error", MessageBoxButton.OK);

                return;
            }

            NavigationService.Navigate(new Uri(string.Format("/Event.xaml?id={0}", evnt.Id)
                                            , UriKind.Relative));

        }
        
    }
}