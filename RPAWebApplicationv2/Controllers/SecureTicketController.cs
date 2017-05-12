using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace RPAWebApplicationv2.Controllers
{
    public class SecureTicketController : Controller
    {
        // GET: SecureTicket
        public JsonResult GetOptions(String ticketID)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse( CloudConfigurationManager.GetSetting("blobStorageConn"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("rpaticketsblob");

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ticketID);

            string strJson = blockBlob.DownloadText();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strJson);
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            return Json(jsonText, JsonRequestBehavior.AllowGet);
            //return Newtonsoft.Json.JsonConvert.SerializeObject(strJson, Newtonsoft.Json.Formatting.None);
        }
    }
}