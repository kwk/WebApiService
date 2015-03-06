using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//
using ApiService;

namespace ApiService.Test
{
    [TestClass]
    public class SyncCallWebService : WebApiService
    {
        public SyncCallWebService()
        {
            this.ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx/";
        }

        [TestMethod]
        public void TestShouldReturnWeatherForecastForNewYork()
        {
            var parameters = new List<ApiServiceParameter>();
            const string methodName = "GetCityForecastByZIP";

            parameters.Add(new ApiServiceParameter { ParamName = "ZIP", ParamValue = "10001" });

            var res = CallWebApiSync(methodName, parameters);

            if (res != null)
            {
                var apiResult = res.ReadAsStringAsync().Result;
                
                bool containsNewYork = apiResult.Contains("New York");

                Assert.IsNotNull(apiResult);
                Assert.AreEqual(true,containsNewYork);
            }

        }

        [TestMethod]
        public void TestShouldReturnWeatherForecastForNewYorkWithProxyServer()
        {
            var cookies = new CookieContainer();

            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true,
                UseDefaultCredentials = true,
                Proxy = new WebProxy("http://192.168.0.254:8080", false, new string[] { }),
                UseProxy = true
            };

            var parameters = new List<ApiServiceParameter>();
            const string methodName = "GetCityForecastByZIP";

            parameters.Add(new ApiServiceParameter { ParamName = "ZIP", ParamValue = "10001" });

            var res = CallWebApiSync(methodName, httpClientHandler, parameters);

            if (res != null)
            {
                var apiResult = res.ReadAsStringAsync().Result;
                
                bool containsNewYork = apiResult.Contains("New York");

                Assert.IsNotNull(apiResult);
                Assert.AreEqual(true,containsNewYork);
            }

        }

    }
}
