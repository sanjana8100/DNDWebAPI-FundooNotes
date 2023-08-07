using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class ForgotPasswordModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
