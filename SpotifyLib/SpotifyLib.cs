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

    class SpotifyLib
    {

        private readonly AuthSpotify authSpotify;


        public static readonly HttpClient client = new HttpClient();


        public SpotifyLib(AuthSpotify auth)
        {
            authSpotify = auth;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authSpotify.Token.AccessToken);
            client.Timeout = TimeSpan.FromSeconds(4);
        }


        public string PausePlayback()
        {
            return PausePlaybackTask().Result;
        }

        private async Task<string> PausePlaybackTask()
        {
            Console.WriteLine("Pausa");

            try
            {
                var response = await client.PutAsync(string.Format("https://api.spotify.com/v1/me/player/pause"), null);

                return response.StatusCode.ToString();
            }
            catch (Exception) { }


            return null;
        }


        public string ResumePlayback()
        {
            return ResumePlaybackTask().Result;
        }

        private async Task<string> ResumePlaybackTask()
        {
            Console.WriteLine("Play");

            try
            {
                var response = await client.PutAsync(string.Format("https://api.spotify.com/v1/me/player/play"), null);

                return response.StatusCode.ToString();
            }
            catch (Exception) { }


            return null;
        }






    }
}
