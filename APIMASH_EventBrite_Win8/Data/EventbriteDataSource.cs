
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


using APIMASH_EventBrite_Core.ViewModels;
using APIMASH_EventBrite_WP8.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMASH_Eventbrite_Win8.Data
{
    public sealed class EventbriteDataSource
    {

        private readonly static EventCategoriesViewModel _model = new EventCategoriesViewModel() { LocationProvider = new Win8LocationService() };

        public static EventCategoriesViewModel Model
        {
            get { return _model; }
        }
    }
}
