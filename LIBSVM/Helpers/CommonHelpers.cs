using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    public static class CommonHelpers
    {
        private static CultureInfo usCulture = new CultureInfo("en-US");

        public static double atof(String s)
        {
            double d = Double.Parse(s, usCulture);
            if (Double.IsNaN(d) || Double.IsInfinity(d))
            {
                throw new FormatException(String.Format("'{0}' is not a valid Double value", s));
            }
            return (d);
        }

        public static double ToDouble(this string s)
        {
            return atof(s);
        }

        public static int atoi(String s)
        {
            return Int32.Parse(s, usCulture);
        }

        public static int ToInteger(this string s)
        {
            return atoi(s);
        }
    }
}
