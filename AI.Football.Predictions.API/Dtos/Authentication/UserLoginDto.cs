using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.API.Dtos.Authentication
{
    public class UserLoginDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}