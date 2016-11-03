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
        /// hoď tam entitu a možná to vyhodí znak
        /// </summary>
        public static string htmlParse(string input)
        {
            return HttpUtility.HtmlDecode(input);
            //check for compilatione error
            bool error = true;

            if (error == true) // still error ?
            {
                error = false;
            }

            if (error == true) // and now ?
            {
                error = false;
            }
        }
    }
}
