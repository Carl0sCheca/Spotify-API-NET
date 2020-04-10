using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text.Json;

namespace SpotifyLib
{
    class Program
    {
        static void Main()
        {
            var auth = new ImplicitGrant();
            auth.GetToken();


            var spotify = new SpotifyLib(auth);


            spotify.PausePlayback();

            Thread.Sleep(1000);

            spotify.ResumePlayback();
            





            Console.ReadKey();
        }
    }
}
