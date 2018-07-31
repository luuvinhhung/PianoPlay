using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PianoPlay_BE.Infrastructure
{
    public static class StandardString
    {
        public static string StandardUserName(string strInput)
        {
            strInput = strInput.Trim().ToLower();
            while (strInput.Contains("  "))
                strInput = strInput.Replace("  ", " ");
            string strResult = "";
            string[] arrResult = strInput.Split(' ');
            foreach (string item in arrResult)
                strResult += item.Substring(0, 1).ToUpper() + item.Substring(1) + " ";
            return strResult.TrimEnd();
        }
        public static string StandardSongName(string strInput)
        {
            strInput = strInput.Trim().ToLower();
            while (strInput.Contains("  "))
                strInput = strInput.Replace("  ", " ");
            string strResult = "";
            string[] arrResult = strInput.Split(' ');
            foreach (string item in arrResult)
                strResult += item.Substring(0, 1) + item.Substring(1) + " ";
            return strResult.TrimEnd();
        }
    }
}