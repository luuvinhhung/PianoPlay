using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PianoPlay_BE.Infrastructure
{
    public static class CheckPhoneNumber
    {
        public static bool CheckCorrectPhoneNumber(string number)
        {
            return Regex.Match(number, @"^[-+]?[0-9]*\.?[0-9]+$").Success;
        }
    }
}