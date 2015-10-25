using System; 
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CatholicAPI
{
    public class MyService : ICatholicService
    {
        #region CatholicApp

        public List<EventDetail> GetEvents()//string EventName, DateTime StartDate, string EndDate, string EventDetail, int UserID, string ExternalDetailURL, xml Images)
        {
            List<EventDetail> _event_Objects = new List<EventDetail>();

            try
            {
                Dictionary<string, string> _setOfImages;

                System.DateTime CurrentDate = new DateTime();
                CurrentDate = DateTime.Now;

                _event_Objects = CatholicDB.Get_Events(CurrentDate);

                foreach (EventDetail _eventObj in _event_Objects)
                {
                    _setOfImages = CatholicDB.GetImagesByEventID(_eventObj.ID);
                    _eventObj.Images = _setOfImages;
                    //_eventObj.startDate.ToString("d");
                }

                //var jser = new System.Web.Script.Serialization.JavaScriptSerializer();
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //json = JsonConvert.SerializeObject(_event_Objects, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd hh:mm:ss" });
                //JsonConvert.SerializeObject(this, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd hh:mm:ss" });
                //json = jss.Serialize(_event_Objects);
                //        this.Context.Response.ContentType = "application/json; charset=utf-8";
                //        this.Context.Response.Write(jss.Serialize(_event_Objects));

                //HttpContext.Current.Response.Write(jser.Serialize(_event_Objects));
            }
            catch (Exception ex)
            {
                int xd = 3;
            }
            return _event_Objects;
        }

        #endregion

    }
}
