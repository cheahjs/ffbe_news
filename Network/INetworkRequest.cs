using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFBENews.Network
{
    public interface INetworkRequest
    {
        void HandleResponse(Dictionary<string, dynamic> response);
        string CreateBody();
        string GetEncodeKey();
        string GetRequestId();
        string GetUrl();
    }
}
