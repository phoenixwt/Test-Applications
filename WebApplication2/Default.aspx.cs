using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchedLib;
using WebApplication2.Quantum;

namespace WebApplication2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add test comments
            // Add another comments.
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //var binding = new WSHttpBinding(SecurityMode.Message);
            //binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            //var endpointAddress = new EndpointAddress("http://192.168.98.88/WCFServices/QuantumOperations.svc");
            //var client = new QuantumOperationsClient(binding, endpointAddress);
            //client.ClientCredentials.UserName.UserName = "devabbvie";
            //client.ClientCredentials.UserName.Password = "Ph47dg#9";

            //var ss = client.UpdateOrderStatus();

            this.test();

            return;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Set soap header for authorization
            var soapHeader = new QuantumSoapHeader();
            soapHeader.Username = "devabbvie";
            soapHeader.Password = "Ph47dg#9";

            // Initialize web service
            var quantum = new QuantumOperations();
            quantum.QuantumSoapHeaderValue = soapHeader;
            quantum.Url = "https://staging.hcpdirect.com/webservice/QuantumOperations.asmx";

            // Create parameter
            var orderStatus = new OrderStatus();
            orderStatus.ClientOrderID = "3663";
            orderStatus.IsComplete = true;
            orderStatus.Shipments = new ShippingItem[]
            {
                new ShippingItem()
                {
                    MailingListID = "3663-A0807",
                    ShippingMethod = "USPS",
                    TrackingNumber = new string[] { "036066215123427" }
                },
                //new ShippingItem()
                //{
                //    MailingListID = "7993-IA307",
                //    ShippingMethod = "USPS",
                //    TrackingNumber = new string[] { "036066215129276", "036066215129283" }
                //},
                //new ShippingItem()
                //{
                //    MailingListID = "7993-IA304",
                //    ShippingMethod = "USPS",
                //    TrackingNumber = new string[] { "036066215129276", "036066215129283", "036066215123283" }
                //}
            };

            //var orderStatus = new OrderStatus();
            //orderStatus.ClientOrderID = "3662";
            //orderStatus.IsComplete = true;
            //orderStatus.Shipments = new ShippingItem[]
            //{
            //    new ShippingItem()
            //    {
            //        MailingListID = "3662-A0807-10697748",
            //        ShippingMethod = "UPS",
            //        TrackingNumber = new string[] { "1111111" }
            //    },
            //    new ShippingItem()
            //    {
            //        MailingListID = "3662-A0807-14076678",
            //        ShippingMethod = "UPS",
            //        TrackingNumber = new string[] { "2222222" }
            //    },
            //    new ShippingItem()
            //    {
            //        MailingListID = "3662-A0807-7531090",
            //        ShippingMethod = "UPS",
            //        TrackingNumber = new string[] { "3333333311", "33333322222" }
            //    }
            //};

            // Invoke web service method
            var responses = quantum.UpdateOrderStatus(new OrderStatus[] { orderStatus });

            if (string.IsNullOrEmpty(responses.First().ClientOrderID))
            {
                // Web Service Error, for example username or password is incorrect.
                var errorMessage = responses.First().Error;
            }
            else
            {
                // Get response of each order
                foreach (var response in responses)
                {
                    if (response.Success)
                    {
                        // Success
                        var orderId = response.ClientOrderID;
                        var updatedStatus = response.StatusName;
                    }
                    else
                    {
                        // Failure
                        var orderId = response.ClientOrderID;
                        var errorMessage = response.Error;
                    }
                }
            }

            return;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // Genarate Jason file by below script, and save as Jason?.txt.
            // select '{ "EmailDeliveryID": ' + convert(varchar(max), ed.EmailDeliveriesID) + ', "email": "' + ed.[To] + '", "event": "delivered", "timestamp": 1376944440, "reason": "Test" },'
            // from EmailDeliveries ed
            // where ed.EmailDeliveriesID not in (select eds.EmailDeliveriesID from EmailDeliveriesStatus eds)
            for (int i = 1; i <= 3; i++)
            {
                var json = File.ReadAllText("E:\\Jason" + i + ".txt");
                byte[] data = Encoding.Default.GetBytes(json);

                // Create request
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:60552/SendGridHandler.ashx");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "text/xml";

                // Create async request stream
                using (var requestStream = httpWebRequest.EndGetRequestStream(httpWebRequest.BeginGetRequestStream(null, null)))
                {
                    requestStream.Write(data, 0, data.Length);
                }

                IAsyncResult asyncResult = (IAsyncResult)httpWebRequest.BeginGetResponse(
                            delegate(IAsyncResult callback)
                            {
                                // Get the RequestState object from the async result.
                                HttpWebRequest request = (HttpWebRequest)callback.AsyncState;

                                // Call EndGetResponse, which produces the WebResponse object
                                // that came from the request issued above.
                                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callback);

                                // Start reading data from the response stream.
                                Stream responseStream = response.GetResponseStream();
                            },
                            httpWebRequest); 
            }

            return;
        }

        private void test()
        {
            ICSM_Orders ios = new ICSM_Orders();

            // first order
            ICSM_ShipData sd = new ICSM_ShipData("name", "delivertoname", "123-456-7890", "company", "address", "city", "st", "zipcode", "0");
            ICSM_LineItems lis = new ICSM_LineItems();
            ICSM_LineItem li1 = new ICSM_LineItem("AB605-981605-MI", 5);
            lis.AddNewItem(li1);
            ICSM_LineItem li2 = new ICSM_LineItem("AB605-982701-MI", 10);
            lis.AddNewItem(li2);
            ICSM_Order io1 = new ICSM_Order(lis, sd, "P000002", "MZ4Y3124", "111", "102426", "1", "prospectid", "fedexid");

            //second order
            ICSM_ShipData sd2 = new ICSM_ShipData("name2", "delivertoname2", "987-654-3210", "company2", "address2", "city2", "st2", "zipcode2", "0");
            ICSM_LineItems lis2 = new ICSM_LineItems();
            ICSM_LineItem li21 = new ICSM_LineItem("AB605-895554", 1);
            ICSM_LineItem li22 = new ICSM_LineItem("AB605-895553", 2);
            lis2.AddNewItem(li21);
            lis2.AddNewItem(li22);
            ICSM_LineItem li23 = new ICSM_LineItem("AB605-894048", 3);
            lis2.AddNewItem(li23);
            ICSM_Order io2 = new ICSM_Order(lis2, sd2, "P000002", "MZ4Y3124", "222", "102426", "1", "prospectid", "fedexid");

            ios.Add(io1);
            ios.Add(io2);
            
            SchedLib.InventoryReport ir = new InventoryReport();
            ir.ProcessOrdersToICSM(ios, @"E:\test1.xml");

        }
    }
}