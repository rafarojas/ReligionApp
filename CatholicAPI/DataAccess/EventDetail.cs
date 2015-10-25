using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

public partial class EventDetail
{
    public int ID;
    public string Name;
    public DateTime? startDate, endDate;
    public string detail;
    public int userID;
    public string externalURL;
    //public XElement ImagesPath;
    //public XmlReader doc;
    public Dictionary<string, string> Images;

    public EventDetail()
    {

    }
}
