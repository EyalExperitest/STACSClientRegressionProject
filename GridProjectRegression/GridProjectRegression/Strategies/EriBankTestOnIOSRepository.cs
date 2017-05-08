using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class EriBankTestOnIOSRepository : ClientStrategy
    {

        public override void RunStartegy()
        {
            String deviceName = client.GetDeviceProperty("device.name");
            Console.WriteLine(deviceName);
           // client.Uninstall("com.experitest.ExperiBank");
           // client.Install(@"C:\MobileApps\IPA\EriBank.ipa", true, false);

            client.Launch("com.experitest.ExperiBank", true, true);
            client.WaitForElement("EriBankIOS", "Username", 0, 60000);
            client.ElementSendText("EriBankIOS", "Username", 0, "company");
            client.ElementSendText("EriBankIOS", "Password", 0, "company");
            client.Click("EriBankIOS", "loginButton", 0, 1);
            client.Click("EriBankIOS", "makePaymentButton", 0, 1);
            client.ElementSendText("EriBankIOS", "Phone", 0, "09785643765");
            client.ElementSendText("EriBankIOS", "Name", 0, "Eyal");
            client.ElementSendText("EriBankIOS", "Amount", 0, "100");
            client.Click("EriBankIOS", "countryButton", 0, 1);
            client.Click("EriBankIOS", "element 0", 0, 1);
            client.Click("EriBankIOS", "sendPaymentButton", 0, 1);
            client.Click("EriBankIOS", "element 1", 0, 1);
            client.Click("EriBankIOS", "logoutButton", 0, 1);
            client.Click("EriBankIOS", "Username_1", 0, 1);
            client.Click("EriBankIOS", "element 2", 0, 1);
            client.Click("EriBankIOS", "Password_1", 0, 1);
            client.Click("EriBankIOS", "element 2", 0, 1);


        }
    }
}
