using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using LuceneSearch;

[DataContract]
public partial class RPATicket
{
    [DataMember]
    public String TicketId { get; set; }

    [DataMember]
    public String TicketNumber { get; set; }

    [DataMember]
    public List<String> Categories { get; set; }

    [DataMember]
    public List<RPAResult> Matches { get; set; }

    [DataMember]
    public String TicketTitle { get; set; }

    [DataMember]
    public String TicketDescription { get; set; }

    [DataMember]
    public String userID { get; set; }

    [DataMember]
    public String Error { get; set; }
}