
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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ApiMashExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }
    public static void RemoveRange<T>(this ObservableCollection<T> collection, IEnumerable<T> itemsToRemove) where T : ApiMashBaseViewModel
    {
        var list = itemsToRemove.ToList<T>();

        foreach (var item in list)
        {
            foreach (var tItem in collection)
            {
                if (tItem.Id == item.Id)
                {
                    collection.Remove(tItem);
                    break;
                }
            }
        }        
    }

}
