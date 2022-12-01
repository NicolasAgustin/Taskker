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
        private string Baseurl = "https://komcfgkpg4.execute-api.us-east-1.amazonaws.com/dev/";
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

        public async Task<bool> Create(string text, int userid)
        {
            if (!IsAuthenticated)
            {
                throw new NotAuthenticated();
            }

            Note newNote = new Note() { text = text, user = userid };

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
        }

        public async Task<List<Note>> GetNotes(int userid)
        {
            if (!IsAuthenticated) {
                throw new NotAuthenticated();
            }

            List<Note> notes = new List<Note>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);

                HttpResponseMessage response = await client.GetAsync("get_notes");

                if (response.IsSuccessStatusCode)
                {
                    var notesResponse = response.Content.ReadAsStringAsync().Result;
                    notes = JsonConvert.DeserializeObject<List<Note>>(notesResponse);
                }
            }

            return notes;
        }
    }
}