
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


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIMASH_Core
{
    public static class ApiMashInvoke
    {

        public static async Task<T> InvokeAsync<T>(Uri apiEndPointUri)
        {

            if (apiEndPointUri == null)
                throw new ArgumentNullException();

            using (var httpClient = new HttpClient())
            {
                var responseBody = await httpClient.GetStringAsync(apiEndPointUri);

                return JsonConvert.DeserializeObject<T>(responseBody);
            }
        }

        public static async Task<TTarget> InvokeAsync<TSource, TTarget>(Uri apiEndPointUri, Func<TSource, TTarget> mapper)
        {
            if (apiEndPointUri == null || mapper == null)
                throw new ArgumentNullException();


            using (var httpClient = new HttpClient())
            {
                var responseBody = await httpClient.GetStringAsync(apiEndPointUri);

                return mapper(JsonConvert.DeserializeObject<TSource>(responseBody));
            }
        }

        public static Uri ApiEndPointUriBuilder(string apiEndPoint, params KeyValuePair<string, string>[] parameters)
        {

            if (string.IsNullOrEmpty(apiEndPoint))
                throw new ArgumentException("Invalid argument. The apiEndPoint, can't be null or empty", "apiEndPoint");

            var blrd = new UriBuilder(apiEndPoint);

            if (parameters != null)
                blrd.Query = string.Join("&", parameters.Select<KeyValuePair<string, string>, string>(p => p.Key == null ? p.Value : p.Key + "=" + p.Value).ToArray<string>());

            return blrd.Uri;

        }
    }
}
