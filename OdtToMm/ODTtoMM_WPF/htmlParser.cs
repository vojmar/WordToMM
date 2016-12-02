using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;

namespace ODTtoMM_WPF
{
    public static class htmlParser
    {   
        /// <summary>
        /// hoď tam entitu a možná to vyhodí znak
        /// </summary>
        public static string htmlParse(string input)
        {
            return HttpUtility.HtmlDecode(input);
        }
    }
}
