using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteDemo
{
    class Program
    {
        private static Dictionary<string, string> _cities = new Dictionary<string, string>
        {
            ["010"] = "北京",
            ["028"] = "成都",
            ["0512"] = "苏州"
        };

        public static async Task WeatherForecast(HttpContext context)
        {
            //var city = (string)context.GetRouteData().Values["city"];
        }

        static void Main(string[] args)
        {




            Console.WriteLine("Hello World!");
        }
    }
}
