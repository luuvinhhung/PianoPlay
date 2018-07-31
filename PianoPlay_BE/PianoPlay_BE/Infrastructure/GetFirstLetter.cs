using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PianoPlay_BE.Infrastructure
{
    public static class GetFirstLetter
    {
        public static string FirstLetter(string name)
        {
            string[] nospace = name.Split(' ');
            string result = "";
            for (int i = 0; i <= nospace.Length - 1; i++)
            {
                result += nospace[i].Substring(0, 1).ToUpper();
            }
            return result;
        }
    }
}