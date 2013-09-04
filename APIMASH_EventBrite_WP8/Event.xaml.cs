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
using APIMASH_EventBrite_WP8.Services;
using System.Collections.ObjectModel;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Maps.Controls;
using System.Reflection;

namespace APIMASH_EventBrite_WP8
{
    public partial class Event : PhoneApplicationPage
    {
        private EventViewModel _model = new EventViewModel() { LocationProvider = new WP8LocationService() };
        public Event()
        {
            InitializeComponent();


            MapExtensionsSetup(this.EventMap);

        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Utils.ShowHideProgressBar(this.progress, true);

            DataContext = null;
            SetModelPropertiesFromQS();

            if (await _model.LoadDataAsync())
            {
                DataContext = _model;

                this.EventLocations.ItemsSource = _model.Locations;
            }

            Utils.ShowHideProgressBar(this.progress,  false);            
        }

        private void SetModelPropertiesFromQS()
        {
            _model.Id = Utils.GetValueFromQueryString<long>(this, 0, (v) => long.Parse(v), "id");
            
        }

        private  void MapExtensionsSetup(Map map)
        {
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(map);
            var runtimeFields = this.GetType().GetRuntimeFields();

            foreach (DependencyObject i in children)
            {
                var info = i.GetType().GetProperty("Name");

                if (info != null)
                {
                    string name = (string)info.GetValue(i);

                    if (name != null)
                    {
                        foreach (FieldInfo j in runtimeFields)
                        {
                            if (j.Name == name)
                            {
                                j.SetValue(this, i);
                                break;
                            }
                        }
                    }
                }
            }
        }

    }
}