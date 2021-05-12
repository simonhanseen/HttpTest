using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpTest.Data
{
    public class BookManager
    {
        private string URL = "https://fast-castle-50377.herokuapp.com/";
        private string strAuthorizationKey = "";

        public BookManager()
        {

        }

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            //Login information and confirmation
            if(string.IsNullOrEmpty(strAuthorizationKey))
            {
                Login login = new Login();
                User user = new User() { identifier = "student", password = "Student1234" };
                login.user = user;
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URL + "auth/local", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                string token = JsonConvert.DeserializeObject<Login>(responseBody).jwt;
                strAuthorizationKey = token;
                //string strAuthorizationKey = await GetToken(client);
            }

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",strAuthorizationKey);
            return client;
        }

        //private async Task<string> GetToken(HttpClient client)
        //{
        //    Login login = new Login();
        //    User user = new User() { identifier = "student", password = "Student1234" };
        //    login.user = user;
        //    var json = JsonConvert.SerializeObject(user);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await client.PostAsync(URL + "auth/local", content);

        //    string responseBody = await response.Content.ReadAsStringAsync();

        //    string token = JsonConvert.DeserializeObject<Login>(responseBody).jwt;

        //    return token;
        //}

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            HttpClient client = await GetClient();
            string strResult = await client.GetStringAsync(URL + "Books");

            return JsonConvert.DeserializeObject<IEnumerable<Book>>(strResult);
        }

        public async Task<Book> AddBook(string title, string isbn, string description)
        {
            Book newBook = new Book() { Title = title, ISBN = isbn, Description = description };
            HttpClient client = await GetClient();

            string json = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(URL + "Books", content);
            string responseBody = await response.Content.ReadAsStringAsync();

           return JsonConvert.DeserializeObject<Book>(responseBody);
        }

        public async Task<Book> UpdateBook(int id, string title, string isbn, string description)
        {
            Book updatedBook = new Book() { Title = title, ISBN = isbn, Description = description};
            HttpClient client = await GetClient();

            string json = JsonConvert.SerializeObject(updatedBook);
            var content = new StringContent(json);

            HttpResponseMessage response = await client.PutAsync(URL + $"Books/{id}", content);
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Book>(responseBody);
        }

        public async Task<Book> DeleteBook(int id)
        {
            HttpClient client = await GetClient();

            HttpResponseMessage response = await client.DeleteAsync(URL + $"Books/{id}");
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Book>(responseBody);
        }
    }
}
