using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using CatholicAPI;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //string endpointUrl = "http://localhost:59162/CatholicService.svc///";
            string endpointUrl = "http://54.152.95.162/CatholicAPI/CatholicService.svc///";
            string method;
            string parameters;
            JSON_client client = new JSON_client(endpointUrl);

            //method = "FrontLineQueueTest";
            //parameters = "{\"testTag\":\"MOBILE_LOBBY_TEST\"," + "\"casinoPrefix\":\"turmajtc\"," + "\"testUrl\":\"http://downloadtur.majesticslots.com/mobile\"}";

            method = "GetEvents";
            parameters = "";// "{\"testTag\":\"MOBILE_CASHIER_TEST\"," + "\"casinoPrefix\":\"eurmajtc\"}";
            
            client.POST(method, parameters);

            Console.ReadLine();
        }
    }
}
