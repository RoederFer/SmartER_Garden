using SmartER_Garden_Library.SmartER_Garden_Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartER_Garden_WPF
{
    internal class RestHelper
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly Uri baseUri = new Uri("http://localhost:7000/api/htl16/students/", UriKind.Absolute);
        private readonly HttpClient client = new HttpClient();

        public RestHelper()
        {
            client.BaseAddress = baseUri;
        }

        internal async Task<IEnumerable<EintragWindow>> GetEintraege()
        {
            HttpResponseMessage msg = await client.GetAsync("");
            if (msg.IsSuccessStatusCode)
            {
                var eintraege = await JsonSerializer.DeserializeAsync<List<EintragWindow>>(await msg.Content.ReadAsStreamAsync(), options);
                return eintraege;
            }
            else
            {
                throw new Exception(await msg.Content.ReadAsStringAsync());
            }
        }

        internal async Task DeleteEintrag(int id)
        {
            HttpResponseMessage msg = await client.DeleteAsync(id.ToString());
        }

        internal async Task ChangeEinrag(Eintrag e)
        {
            string eintrag = JsonSerializer.Serialize(e, options);
            StringContent sc = new StringContent(eintrag, Encoding.UTF8, "application/json");
            HttpResponseMessage msg = await client.PostAsync("", sc);
        }
    }
}