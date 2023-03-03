using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Extentions
{
    public static class AppExtentions
    {
        public static bool IsEmail(this string email)
        {
            return Regex.IsMatch(email, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        }
        public static bool IsDetails(this string details)
        {
            return Regex.IsMatch(details, "^([a-zA-Z]+?)([-\\s'][a-zA-Z]+)*?$");
        }
        public static bool IsNumber(this string number)
        {
            return Regex.IsMatch(number, "^\\+?[1-9][0-9]{7,14}$");
        }
    }
}
