using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EKYC.DesktopTool
{
    public class Auth0Client
    {
        const string AUTH0_CLIENT = "";
        const string AUTH0_DOMAIN = "";

        public string baseUrl { get; set; }

        public Auth0Client()
        {
            this.baseUrl = "https://" + AUTH0_DOMAIN;
        }

        public User AuthenticateUser(string username, string password)
        {
            string endpoint = this.baseUrl + "/oauth/token";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password,
                client_id = AUTH0_CLIENT,
                grant_type = "password",
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                var userResult = JsonConvert.DeserializeObject<User>(response);
                return userResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetUserDetails(User user)
        {
            string endpoint = this.baseUrl + "/userinfo";
            string access_token = user.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;
            try
            {
                string response = wc.DownloadString(endpoint);
                user = JsonConvert.DeserializeObject<User>(response);
                user.access_token = access_token;
                return user;
            }
            catch (Exception ex)
            {
                //TODO Capture more details on domain invalid and https errors.
                return null;
            }
        }
    }
}
