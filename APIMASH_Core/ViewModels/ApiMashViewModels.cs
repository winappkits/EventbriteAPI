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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace APIMASH_Core.ViewModels
{
    public abstract class ApiMashBaseViewModel : ApiMashBindable
    {

        #region Properties

        private bool _isDataLoaded = false;
        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            protected set {this.SetProperty(ref _isDataLoaded , value, "IsDataLoaded"); }
        }

        private long _id;
        public long Id
        {
            get { return _id; }
            set { this.SetProperty(ref _id, value, "Id"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref _name, value , "Name"); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { this.SetProperty(ref _description, value , "Description"); }
        }
        #endregion

        #region Abstract Methods

        public abstract Task<bool> LoadDataAsync();
        public abstract bool LoadData();

        #endregion
    }
   
    public class ItemViewModel : ApiMashBaseViewModel
    {
    
        public override Task<bool> LoadDataAsync()
        {
            throw new NotImplementedException();
        }

        public override bool LoadData()
        {
            return true;
        }
    }
}
