using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CSC_Assignment_1
{
    public partial class WeatherServiceForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument wsResponseXmlDoc = new XmlDocument();
            int wsCounter = 0;
            String weatherAPIKey = System.Web.Configuration.WebConfigurationManager.AppSettings["WeatherAPIKey"].Trim();

            //http://api.worldweatheronline.com/free/v2/weather.ashx?q=china&format=xml&num_of_days=5&key=371833f4a138910326c7181ec551b
            //id=jipx(spacetime0)
            UriBuilder url = new UriBuilder();
            url.Scheme = "http";// Same as "http://"

            url.Host = "api.worldweatheronline.com";
            url.Path = "free/v2/weather.ashx";// change to v2
            url.Query = "q=china&format=xml&num_of_days=5&key=" + weatherAPIKey;

            //get user's last req date and req date
            DateTime userRequestDate = DateTime.Today;
            DateTime lastRequestDate = Convert.ToDateTime(Session["UserLastRequestDate"]);
           // String userRequestCountry = Session["UserRequestCountry"].ToString();
           // String lastRequestCountry = Session["UserLastRequestCountry"].ToString();

         //   if (userRequestDate == lastRequestDate && userRequestCountry == lastRequestCountry)
         //   {
         //       //display cache data
         //   }

            do
            {
                wsCounter++;
                if (wsCounter == 3) //when counter is 3, program is at 4th loop, thus display error msg and break
                {
                    Response.ContentType = "text/html";
                    Response.Write("<h2> error  accessing web service </h2>");
                    break;
                }
                //Make a HTTP request to the global weather web service
                wsResponseXmlDoc = MakeRequest(url.ToString());

                //if (wsResponseXmlDoc != null)

                //display the XML response 
                String xmlString = wsResponseXmlDoc.InnerXml;
                Response.ContentType = "text/xml";
                Response.Write(xmlString);
                Response.End();



                // Save the document to a file and auto-indent the output.
                XmlTextWriter writer = new XmlTextWriter(Server.MapPath("xmlweather.xml"), null);
                writer.Formatting = Formatting.Indented;
                wsResponseXmlDoc.Save(writer);

                //You're never closing the writer, so I would expect it to keep the file open. That will stop future attempts to open the file

                writer.Close();
            } while (wsResponseXmlDoc == null);

            Session["UserLastRequestDate"] = DateTime.Today;
            Session["UserLastRequestCountry"] = "";


            // {
            //    Response.ContentType = "text/html";
            //    Response.Write("<h2> error  accessing web service </h2>");
            // }
        } // End Page_Load()

        public static XmlDocument MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                // set timeout to 15 seconds
                request.Timeout = 15 * 1000;
                request.KeepAlive = false;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());
                return (xmlDoc);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}