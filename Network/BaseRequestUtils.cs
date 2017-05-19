using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFBENews.Network
{
    class BaseRequestUtils
    {
        /// <summary>
        /// Add user info to the request.
        /// At the moment, we ignore eveything but the platform parameter
        /// </summary>
        /// <param name="rootObject"></param>
        public static void AddUserInfo(Dictionary<string, dynamic> rootObject)
        {
            var jsonObject = new List<Dictionary<string, string>>();
            var dict = new Dictionary<string, string> {["K1G4fBjF"] = "2"}; //Platform
            jsonObject.Add(dict);
            rootObject["LhVz6aD2"] = jsonObject;
        }
    }
}
