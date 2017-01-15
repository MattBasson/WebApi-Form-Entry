using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leads
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNotEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str) == false;
        }
    }
}