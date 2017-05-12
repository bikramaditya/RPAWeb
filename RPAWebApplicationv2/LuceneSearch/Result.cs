using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuceneSearch
{
    public class RPAResult
    {
        public int ID { get; set; }
        public string KeyWords { get; set; }
        public int ScriptID { get; set; }
        public string ScriptText { get; set; }
        public string UserConfirmationMsg { get; set; }
        public float Score { get; set; }
    }
}
