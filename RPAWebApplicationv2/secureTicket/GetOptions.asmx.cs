using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace RPAWebApplicationv2.secureTicket
{
    /// <summary>
    /// Summary description for GetOptions
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GetOptions : System.Web.Services.WebService
    {

        [WebMethod]
        public string TicketOpsstions(String ticketID)
        {
            String strJson = "";
            using (var streamReader = new StreamReader(@"C:\Temp\options.txt", Encoding.UTF8))
            {
                strJson = streamReader.ReadToEnd();
            }
            
            return Newtonsoft.Json.JsonConvert.SerializeObject(strJson, Newtonsoft.Json.Formatting.Indented);
            //return "Hello World";
        }
    }
}
