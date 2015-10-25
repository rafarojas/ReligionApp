using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CatholicAPI
{
    public struct Test
    {
        public int id;
        public string name;
        public string description;
    }

    public struct Priority
    {
        public int id;
        public string description;
    }

    [Serializable()]
    public class TestStatus : ISerializable
    {
        public int status;
        public string log;
        public string resultPath;

        //Serialization function
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("CatholicTestStatus", status);
            info.AddValue("CatholicTestLog", log);
            info.AddValue("CatholicTestResultPath", resultPath);
        }
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyService" in both code and config file together.
    [ServiceContract]
    public interface ICatholicService
    {
        
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<EventDetail> GetEvents();

    }
}
