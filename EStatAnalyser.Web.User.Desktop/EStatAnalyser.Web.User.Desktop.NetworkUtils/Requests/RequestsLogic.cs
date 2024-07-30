using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using EStatAnalyser.Web.User.Desktop.Core.Requests;
using EStatAnalyser.Web.User.Desktop.NetworkUtils.Converters;
using System.Net.Http.Json;
using System.Text;

namespace EStatAnalyser.Web.User.Desktop.NetworkUtils.Requests
{
    public class RequestsLogic
    {
        public static async Task<string> GetAll()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var connectionString = WebConfiguration.ConnectionString + "get-all";
                    client.Timeout = TimeSpan.FromSeconds(7);
                    var response = await client.GetAsync(connectionString);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
            }
            catch 
            {
                return null;
            }
        }

        public static async Task<string> GetById(int Id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(7);
                    var response = await client.GetAsync(WebConfiguration.ConnectionString + "get-by-id/" + Id);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
            }
            catch 
            {
                return null;
            }
        }

        public static async Task<StringRequest> AddData(string data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(7);
                    var destination = WebConfiguration.ConnectionString + "add-data";
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(destination, content);
                    response.EnsureSuccessStatusCode();
                    return new StringRequest { Success = true };
                }
            }
            catch (Exception ex)
            {
                return new StringRequest { Message = ex.Message, Success = false };
            }
        }

        public static async Task<StringRequest> DeleteEntity(int Id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(7);
                    var response = await client.DeleteAsync(WebConfiguration.ConnectionString + "delete-data/" + Id);
                    response.EnsureSuccessStatusCode();
                    return new StringRequest { Success = true };
                }
            }
            catch (Exception ex)
            {
                return new StringRequest { Message = ex.Message, Success = false };
            }
        }

        public static async Task<StringRequest> UpdateEntity(string data)
        {
            try
            {
                throw new NotImplementedException();
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(7);
                    var response = await client.PutAsJsonAsync(WebConfiguration.ConnectionString + "update-entity", data);
                    response.EnsureSuccessStatusCode();

                    return new StringRequest { Success = true };
                }
            }
            catch (Exception ex)
            {
                return new StringRequest { Message = ex.Message, Success = false };
            }
        }
    }
}