using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// via Nuget
using System.Net.Http; // Microsoft HTTP Client Libraries


namespace ApiService
{
    /// TODO: Implement usage of a Proxy-Server 
    
    public class ApiServiceParameter
    {
        public string ParamName { get; set; }
        public string ParamValue { get; set; }

        /// <summary>
        /// ParamName + ParamValue = query
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ParamName + "=" + ParamValue;
        }
    }

    public class WebApiService
    {
        /// <summary>
        /// Uri of the webservice
        /// </summary>
        public string ServiceUri { get; set; }

        /// <summary>
        /// Performs a synchronous call to the passed method name. 
        /// </summary>
        /// <param name="methodName">Method to call</param>
        /// <param name="parameters">List of the query parameters</param>
        /// <returns>HttpContent</returns>
        public HttpContent CallWebApiSync(string methodName, List<ApiServiceParameter> parameters = null)
        {
            if (string.IsNullOrWhiteSpace(ServiceUri)) return null;

            string uri = ServiceUri + methodName; 

            if (parameters != null && parameters.Count > 0)
            {
                uri += "?";

                foreach (var apiServiceParameter in parameters)
                {
                    uri += apiServiceParameter.ToString() + "&";
                }

                if (uri[uri.Length - 1] == '&')
                {
                    uri = uri.Substring(0, uri.Length - 1);
                }
            }

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;
                return response.Content;
            }
        }

        /// <summary>
        /// Performs a synchronous call to the passed method name. 
        /// </summary>
        /// <param name="methodName">Method to call</param>
        /// <param name="parameters">List of the query parameters</param>
        /// <returns>HttpContent</returns>
        public HttpContent CallWebApiSync(string methodName, HttpClientHandler handler, List<ApiServiceParameter> parameters = null)
        {
            if (string.IsNullOrWhiteSpace(ServiceUri)) return null;

            string uri = ServiceUri + methodName;

            if (parameters != null && parameters.Count > 0)
            {
                uri += "?";

                foreach (var apiServiceParameter in parameters)
                {
                    uri += apiServiceParameter.ToString() + "&";
                }

                if (uri[uri.Length - 1] == '&')
                {
                    uri = uri.Substring(0, uri.Length - 1);
                }
            }

            using (var client = new HttpClient(handler))
            {
                var response = client.GetAsync(uri).Result;
                return response.Content;
            }
        }
    }


}
