
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
using Windows.Data.Html;
using Windows.UI.Xaml.Data;

namespace APIMASH_Eventbrite_Win8.Common
{
    /// <summary>
    /// Value converter that extracts text from a HTML fragment.
    /// </summary>
    public sealed class HtmltoTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
             if (!(value is string))
                 return string.Empty;

             return HtmlUtilities.ConvertToText(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
