using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class WikipediaTest : ClientStrategy
    {
        private int articles = 10;

        public int Articles
        {
            get
            {
                return articles;
            }

            set
            {
                articles = value;
            }
        }

        public override void RunStartegy()
        {
            String deviceName = client.GetDeviceProperty("device.name");
            Console.WriteLine(deviceName);

            client.GetDeviceProperty("device.os");



            var articleNames = new String[articles];
            client.Launch("http://www.wikipedia.com", true, false);
            client.WaitForElement("WEB", "text=English", 0, 120000);
            client.Click("WEB", "text=English", 0, 1);
            Client.WaitForElement("WEB", "id=mw-mf-main-menu-button", 0, 10000);

            for (int i = 0; i < 10; i++)
            {
                client.Click("WEB", "id=mw-mf-main-menu-button", 0, 1);
                client.Click("WEB", "text=Random", 0, 1);
                client.Click("WEB", "xpath=//*[@text='Random']", 0, 1);
                if (client.IsElementFound("WEB", "xpath=//*[@id='section_0']", 0))
                {
                    articleNames[i]= client.ElementGetProperty("WEB", "xpath=//*[@id='section_0']", 0, "text");
                    client.Report(articleNames[i], true);
                } 
                else
                {
                    articleNames[i] = "";
                }
            }

        }
    }
}
