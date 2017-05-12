using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace RPAWebApplicationv2.secureTicket
{
    public partial class TicketDetails : System.Web.UI.Page
    {
        public string ticketID;
        public RPATicket ticket;
        protected void Page_Load(object sender, EventArgs e)
        {
            ticketID = Request.QueryString.Get("TicketID");

            if (ticketID == null || ticketID.Length == 0)
            {
                return;
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("blobStorageConn"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("rpaticketsblob");

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ticketID);

            string strTicket = blockBlob.DownloadText();

            XmlSerializer serializer = new XmlSerializer(new RPATicket().GetType());
            StringReader rdr = new StringReader(strTicket);
            ticket = (RPATicket)serializer.Deserialize(rdr);
            
        }        
    }
}