using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace SmartChoiceApp.Database
{
    public class Database
    {
        #region Properties
        private static HttpClient Client { get; set; }
        private static string URL { get; set; }
        private readonly string UrlHome = "https://smartchoiceapi.herokuapp.com/";
        object obj { get; set; }
        #endregion

        #region Constructor
        public Database()
        {
            URL = "";
            Client = new HttpClient();
        }
        #endregion

        #region User Functions

        public bool SignUp()
        {
            return true;
        }
        public async Task<User> GetUser(string ID)
        {
            URL = UrlHome + "users/" + ID.ToString();
            var httpResponse = await Client.GetAsync(URL);
            var response = await httpResponse.Content.ReadAsStringAsync();
            return JObject.Parse(response)["Result"].ToObject<User>();
        }
        #endregion

        #region Product Funcions
        public async Task<ProductDetail> GetProductDetail(int ID)
        {
            URL = UrlHome + "checkinformation/" + ID.ToString();
            var httpResponse = await Client.GetAsync(URL);
            var response = await httpResponse.Content.ReadAsStringAsync();
            if ((int)JObject.Parse(response)["Result"]["messageUpdate"]["affectedRows"] == 1)
            {
                return JObject.Parse(response)["Result"].ToObject<ProductDetail>();
            }
            else
            {
                return null;
            }

        }

        public void GetProductType()
        {

        }

        public async Task<Manufacturer> GetManufacturerDetail(int ID)
        {
            URL = UrlHome + "producers/" + ID.ToString();
            var httpResponse = await Client.GetAsync(URL);
            var response = await httpResponse.Content.ReadAsStringAsync();
            var manufacturer = JObject.Parse(response)["Result"].ToObject<Manufacturer>();
            return manufacturer;
        }

        public async Task<List<PestilentInsect>> GetPestilentInsect(int ID)
        {
            URL = UrlHome + "checkinformation/pestilentinsect/" + ID.ToString();
            var httpResponse = await Client.GetAsync(URL);
            var response = await httpResponse.Content.ReadAsStringAsync();
            if (JObject.Parse(response)["Result"].HasValues == true)
            {
                return JObject.Parse(response)["Result"].ToObject<List<PestilentInsect>>();
            }
            else
            {
                return null;
            }
        }

        public void GetReviewList()
        {

        }

        public async Task<bool> AddReview(Review userReview)
        {
            URL = UrlHome + "users/postcomment";
            obj = new
            {
                MaNguoiDung = userReview.MaNguoiDung,
                MaLoaiSanPham = userReview.MaLoaiSanPham,
                Rating = userReview.Rating.ToString(),
                NoiDung = userReview.NoiDung,
                NgayBinhLuan = DateTime.Now.ToString("yyyy/MM/dd"),
            };

            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await Client.PostAsync(URL,data);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                if (responseString == null || responseString == "{}")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        #endregion
        #region Account 
        public async Task<bool> Login(string tendn, string matkhau)
        {
            URL = UrlHome + "users/login";
            obj = new { TenDangNhap = tendn, MatKhau = matkhau };
            var json = JsonConvert.SerializeObject(obj);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await Client.PostAsync(URL, data);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = httpResponse.Content.ReadAsStringAsync().Result;
                if(responseString == null || responseString == "{}")
                {
                    return false;
                }
                else
                {
                    var user = JObject.Parse(responseString)["Result"].ToObject<User>();
                    App.mainUser = new User();
                    App.mainUser = user;
                    return true;
                }
            }
            return false;

        }

        public async Task<bool> SignUp(User user)
        {
            URL = UrlHome + "users/";
            var json = JsonConvert.SerializeObject(user);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await Client.PostAsync(URL, data);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = httpResponse.Content.ReadAsStringAsync().Result;
                if ((int)JObject.Parse(responseString)["Message"]["affectedRows"] == 1)
                {
                    return true;
                }
                else return false;

            }
            else return false;
        }

        public async Task<bool> CheckExist(string tendangnhap)
        {
            URL = UrlHome + "users/checkexist";
            obj = new { TenDangNhap = tendangnhap }; 
            var json = JsonConvert.SerializeObject(obj);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await Client.PostAsync(URL, data);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = httpResponse.Content.ReadAsStringAsync().Result;
                return (bool)JObject.Parse(responseString)["Result"];              
            }
            else return false;
        }

        public async Task<bool> UpdateUser(User user)
        {
            URL = UrlHome + "users";
            var json = JsonConvert.SerializeObject(user);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await Client.PutAsync(URL, data);           
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                if ((int)JObject.Parse(responseString)["Message"]["affectedRows"] == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateAvatar(MediaFile file, string userID)
        {
            URL = UrlHome + "users/updateavatar";
            StreamContent scontent = new StreamContent(file.GetStream());
            scontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                FileName = "user" + userID,
                Name = "image"
            };
            scontent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            var multi = new MultipartFormDataContent();
            multi.Add(scontent);
            var httpResponse = await Client.PostAsync(URL, multi);
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            if ((string)JObject.Parse(responseString)["message"] == "success")
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
