using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class RunEriBankPayOnAndroidRepository : ClientStrategy
    {
        public override void RunStartegy()
        {
            String deviceName = client.GetDeviceProperty("device.name");
            Console.WriteLine(deviceName);
            client.Uninstall("com.experitest.ExperiBank");
            client.Install(@"C:\MobileApps\apk\eribank.apk", true, false);
            client.Launch("cloud:com.experitest.ExperiBank/.LoginActivity", true, true);
            client.ElementSendText("EriBankAndroid", "Username", 0, "company");
            client.ElementSendText("EriBankAndroid", "Password", 0, "company");
            client.Click("EriBankAndroid", "Login", 0, 1);
            client.Click("EriBankAndroid", "Make Payment", 0, 1);
            client.ElementSendText("EriBankAndroid", "Phone", 0, "098967");
            client.ElementSendText("EriBankAndroid", "Name", 0, "Name");
            client.ElementSendText("EriBankAndroid", "Amount", 0, "Amount");
            client.WaitForElement("EriBankAndroid", "Select", 0, 10000);
            client.Click("EriBankAndroid", "Select", 0, 1);
            client.Click("EriBankAndroid", "Japan", 0, 1);
            client.Click("EriBankAndroid", "Send Payment", 0, 1);
            client.Click("EriBankAndroid", "Yes", 0, 1);
        }
    }
}
