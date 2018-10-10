using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WorldGeneralLib.Functions
{
    public class JudgeNumber
    {
        //ture   : is number
        //false  : not number
        public static bool VerifyNumber(string text)
        {
            Regex reg = new Regex("^[0-9]+$");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isPositiveInteger(string text)
        {
            Regex reg = new Regex("^[1-9]\\d*$");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isPositiveUINT1632(string text)
        {
            Regex reg = new Regex("^[0-9]\\d*$");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool isNegativeInteger(string text)
        {
            Regex reg = new Regex("^-[1-9]\\d*$");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isWhloeNumber(string text)
        {
            if (text.Equals("0") || isPositiveInteger(text) || isNegativeInteger(text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isPositiveDecimal(string text)
        {
            Regex reg = new Regex("^[0]\\.[1-9]*|^[1-9]\\d*\\.\\d*");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isNegativeDecimal(string text)
        {
            Regex reg = new Regex("^-[0]\\.[1-9]*|^-[1-9]\\d*\\.\\d*");
            Match match = reg.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isDecimal(string text)
        {
            return isPositiveDecimal(text) || isNegativeDecimal(text);
        }

        public static bool isRealNumber(string text)
        {
            return isWhloeNumber(text) || isDecimal(text);
        }
    }
}
