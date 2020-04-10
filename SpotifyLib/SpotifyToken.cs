using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyLib
{
    public sealed class SpotifyToken
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string State { get; set; }

    }
}
