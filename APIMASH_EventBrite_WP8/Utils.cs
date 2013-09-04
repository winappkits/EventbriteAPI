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


using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace APIMASH_EventBrite_WP8
{
    public  sealed class Utils
    {
     
        public static void ShowHideProgressBar(ProgressBar progBar, bool show)
        {
            var visibility = System.Windows.Visibility.Collapsed;

            if (show)
            {
                visibility = System.Windows.Visibility.Visible;
            }            

            
            progBar.Visibility = visibility;
            progBar.IsIndeterminate = show;
        }



        public static T GetValueFromQueryString<T>(PhoneApplicationPage page, T defaultValue, Func<string, T> conversionFactory, string key)
        {
            string value;

            if (page.NavigationContext.QueryString.TryGetValue(key, out value))
                return conversionFactory(value);

            return defaultValue;

        }

     
    }
}
