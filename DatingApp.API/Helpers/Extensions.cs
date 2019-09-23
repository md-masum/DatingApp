using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse responce, string message)
        {
            responce.Headers.Add("Application-Error", message);
            responce.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            responce.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse responce, int currentPage, int itemPerPage, int totalItems, int totalPages)
        {
            var paginationHader = new PaginationHeader(currentPage, itemPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            responce.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHader, camelCaseFormatter));
            responce.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if(theDateTime.AddYears(age) > DateTime.Today)
                age--;

            return age;
        }
    }
}