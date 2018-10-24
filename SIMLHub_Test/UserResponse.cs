using ICAN.SIC.Abstractions.IMessageVariants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMLHub_Test
{
    class UserResponse : IUserResponse
    {
        string text;

        public UserResponse(string message)
        {
            this.text = message;
        }

        public string Text
        {
            get { return text; }
        }
    }
}
