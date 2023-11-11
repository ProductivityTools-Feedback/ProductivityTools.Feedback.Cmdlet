using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Feedback.Cmdlet.ClientCaller
{
    public class TokenResponse
    {
        public string kind { get; set; }
        public string idToken { get; set; }
        public string refreshToken { get; set; }
        public string exiresIn { get; set; }

        public bool isNewUser { get; set; }
    }
}
