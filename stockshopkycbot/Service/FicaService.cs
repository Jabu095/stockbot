using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace stockshopkycbot.Service
{
    public class FicaService
    {
        public async Task<HttpResponseMessage> FicaID(Id_check id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string jsonString = JsonConvert.SerializeObject(id);
                    var res = await client.PostAsync("http://105.27.203.166:8080/stockshopfica/api/fica/id_check/", new StringContent(jsonString, Encoding.UTF8, "application/json"));
                    string responseBody = await res.Content.ReadAsStringAsync();
                    res.Content = new StringContent(responseBody, Encoding.UTF8, "application/json");

                    return res;
                }
                catch (Exception ex)
                {

                    return null;
                }
               
            }
        }
    }
}