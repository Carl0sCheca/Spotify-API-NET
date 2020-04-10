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
using System.Collections.Generic;

namespace SpotifyLib
{
    public class AuthSpotify
    {
        public SpotifyToken Token { get; set; }


        public Dictionary<string, string> ExchangeAuth()
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return null;
            }


            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add("http://localhost:4002/");

                //Console.WriteLine("Listening...");


                for (var i = 0; i < 2; i++)
                {
                    listener.Start();

                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;


                    string responseString;
                    if (i == 0)
                    {
                        responseString = "<script>window.location = \"?\" + window.location.hash.substring(1);</script>";
                    }
                    else
                    {
                        responseString = "<HTML><BODY>Can close this window now</BODY></HTML>";
                    }



                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);



                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    output.Close();
                    listener.Stop();

                    if (i == 1)
                    {
                        return GetParameters(request.Url.Query.Substring(1));
                    }
                }
            }

            return null;

        }


        protected Dictionary<string, string> GetParameters(string urlString)
        {
            var count = urlString.Split('&').Length;

            var parameters = new Dictionary<string, string>();


            for (var i = 0; i < count; i++)
            {
                var equal = urlString.IndexOf('=');
                var ampersand = urlString.IndexOf('&');

                if (i == count - 1)
                {
                    if (ampersand == -1)
                    {
                        ampersand = urlString.Length;
                    }
                }

                if (equal < 0)
                {
                    equal = 0;
                }
                if (ampersand < 0)
                {
                    ampersand = 0;
                }

                var key = urlString.Substring(0, equal);
                var value = urlString.Substring(0, ampersand).Substring(equal + 1);

                if (key.Length == 0)
                {
                    continue;
                }

                if (!parameters.ContainsKey(key))
                {
                    parameters.Add(key, value);
                }

                if (i != count - 1)
                {
                    urlString = urlString.Substring(ampersand + 1);
                }
            }

            return parameters;

        }


    }
}
