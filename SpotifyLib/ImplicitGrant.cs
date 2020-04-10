using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using System.Runtime.Serialization;

namespace SpotifyLib
{
    public class ImplicitGrant : AuthSpotify
    {

        public ImplicitGrant()
        {
            Token = new SpotifyToken();
        }

        // TODO: scopes
        public void GetToken(string clientId = "")
        {
            var _clientId = clientId.Length == 0 ? Util.readClientId() : clientId;
            var _responseType = "token";
            var _redirectUri = "http://localhost:4002";

            Util.OpenBrowser(string.Format("https://accounts.spotify.com/authorize?client_id={0}&response_type={1}&redirect_uri={2}&scope=streaming+user-modify-playback-state", _clientId, _responseType, _redirectUri));

            var parameters = ExchangeAuth();

            foreach(string key in parameters.Keys)
            {
                switch (key)
                {
                    case "access_token":
                        Token.AccessToken = parameters[key];
                        break;
                    case "token_type":
                        Token.TokenType = parameters[key];
                        break;
                    case "expires_in":
                        Token.ExpiresIn = int.Parse(parameters[key]);
                        break;
                    case "state":
                        Token.State = parameters[key];
                        break;
                }
            }
        }

    }
}
