using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;

namespace OdtToMm
{
    public static class htmlParser
    {
        /// <summary>
        /// Parses HTML Entities in string into readable characters
        /// </summary>
        public static string htmlParse(string input)
        {
            //NOTHING CAN GO WRONG!!!

            //SRSLY
            return HttpUtility.HtmlDecode(input);
        }


    }
}
