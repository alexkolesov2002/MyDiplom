using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Учет_работы_мастерских
{
    public static class Bufstring
    {
        public static string TGstring;
    }
    public class UserRequest
    {
        [JsonProperty("id_test")]
        public int id_test { get; set; }
        [JsonProperty("login")]
        public string login { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
    }
    public class REST
    {
        public static async Task Get()
        {
            string ff = "";
            List<UserRequest> ur = new List<UserRequest>();
            WebRequest request = WebRequest.Create("http://alex2348-001-site1.itempurl.com/api/test/getuser");
            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    ff = (reader.ReadToEnd());
                }

            }
            var parsed = JsonConvert.DeserializeObject<List<UserRequest>>(ff);
            response.Close();
            Bufstring.TGstring = "";
            foreach (UserRequest i in parsed)
            {
                Console.WriteLine("id" + "=" + i.id_test + "\n" + "log" + "=" + i.login + "\n" + "pas" + "=" + i.password + "\n");
                Bufstring.TGstring += "id" + "=" + i.id_test + "\n" + "log" + "=" + i.login + "\n" + "pas" + "=" + i.password + "\n" + "\n";
            }


        }

        static async Task Post()
        {
            UserRequest newuser = new UserRequest() { login = "отправка", password = "Из консолиbb" };
            WebRequest request = WebRequest.Create("https://localhost:44317/api/test/CreateUser");
            request.Method = "POST";
            var jsonstring = JsonConvert.SerializeObject(newuser);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonstring);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            //WebResponse response = await request.GetResponseAsync();
            //using (Stream stream = response.GetResponseStream())
            //{
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        Console.WriteLine(reader.ReadToEnd());
            //    }
            //}
            //response.Close();

            Console.WriteLine("Что-то сделалось");

            Console.ReadKey();
        }
        static async Task Put()
        {
            UserRequest ChangeUser = new UserRequest() { id_test = 1, login = "Я изменил логин ", password = "Я изменил пароль" };
            WebRequest request = WebRequest.Create("https://localhost:44317/api/test/UpdateUser");
            request.Method = "PUT";
            var jsonstring = JsonConvert.SerializeObject(ChangeUser);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonstring);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            //WebResponse response = await request.GetResponseAsync();
            //using (Stream stream = response.GetResponseStream())
            //{
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        Console.WriteLine(reader.ReadToEnd());
            //    }
            //}
            //response.Close();

            Console.WriteLine("Я изменил запись");

            Console.ReadKey();
        }
        static async Task Del()
        {
            int id_test = 3;
            WebRequest request = WebRequest.Create("https://localhost:44317/api/test/DeleteUser/" + id_test);
            request.Method = "DELETE";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.ReadKey();
        }
    }
}

