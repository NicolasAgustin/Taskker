using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Taskker.Models;
using Taskker.Models.DAL;
using Taskker.Models.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;

namespace Taskker.Models.Services
{
    public class NotAuthenticated : Exception
    {
        public NotAuthenticated()
        {
        }

        public NotAuthenticated(string message)
            : base(message)
        {
        }

        public NotAuthenticated(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class NotesService : INotesService
    {
        // Agregar a configuracion
        //private string Baseurl = "https://8pwqcmgedh.execute-api.us-east-1.amazonaws.com/dev/";
        private string Baseurl = ConfigurationManager.AppSettings["FastNotesEndpoint"];
        private string UserApi = ConfigurationManager.AppSettings["ApiUser"];
        private string PasswordApi = ConfigurationManager.AppSettings["ApiPass"];
        private string Token { get; set; }
        private bool IsAuthenticated
        {
            // Aca deberia chequearse si el token es valido
            get { return !string.IsNullOrEmpty(Token); }
            set { IsAuthenticated = value; }
        }

        public NotesService()
        {

        }

        public async Task<bool> Auth(string user, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    ApiUser authInfo = new ApiUser() { user = user, password = password };

                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("auth", authInfo);

                    if (response.IsSuccessStatusCode)
                    {
                        var notesResponse = response.Content.ReadAsStringAsync().Result;
                        var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(notesResponse);
                        this.Token = values["data"];
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Create(string text, int userid)
        {
            try
            {
                if (!IsAuthenticated)
                {
                    await Auth(UserApi, PasswordApi);
                }

                Note newNote = new Note() { text = text, created_by = userid, closed = false };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);

                    HttpResponseMessage response = await client.PostAsJsonAsync("create", newNote);

                    if (response.IsSuccessStatusCode)
                    {
                        var notesResponse = response.Content.ReadAsStringAsync().Result;
                        var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(notesResponse);
                        return true;
                    }
                }

                return false;
            } catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Note>> GetNotes(int userid)
        {
            if (!IsAuthenticated) {
                await Auth(UserApi, PasswordApi);
            }

            List<Note> notes = new List<Note>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.GetAsync(string.Format("get_notes/{0}", userid));

                    if (response.IsSuccessStatusCode)
                    {
                        var notesResponse = response.Content.ReadAsStringAsync().Result;
                        notes = JsonConvert.DeserializeObject<List<Note>>(notesResponse);
                    }
                }
            } catch (Exception) { }

            return notes;
        }

        public async Task<bool> UpdateNote(Note updatedNote)
        {
            try
            {
                if (!IsAuthenticated)
                {
                    await Auth(UserApi, PasswordApi);
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);

                    HttpResponseMessage response = await client.PostAsJsonAsync("update", updatedNote);

                    if (response.IsSuccessStatusCode)
                    {
                        var notesResponse = response.Content.ReadAsStringAsync().Result;
                        var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(notesResponse);
                        return true;
                    }
                }

                return false;

            } catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteNote(string noteid)
        {
            try
            {
                if (!IsAuthenticated)
                {
                    await Auth(UserApi, PasswordApi);
                }

                List<Note> notes = new List<Note>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage response = await client.GetAsync(string.Format("delete/{0}", noteid));

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }

                return false;

            } catch (Exception)
            {
                return false;
            }
            
        }
    }
}