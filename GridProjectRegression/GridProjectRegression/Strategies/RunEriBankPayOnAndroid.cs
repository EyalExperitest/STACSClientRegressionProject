using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class RunEriBankPayOnAndroid : ClientStrategy
    {
        public override void RunStartegy()
        {
            String deviceName = client.GetDeviceProperty("device.name");
            Console.WriteLine(deviceName);
            client.Uninstall("cloud:com.experitest.ExperiBank");
            client.Install(@"C:\MobileApps\apk\eribank.apk", true, false);
            client.Launch("cloud:com.experitest.ExperiBank/.LoginActivity", true, true);
            client.WaitForElement("NATIVE", "hint=Username", 0, 30000);
            client.ElementSendText("NATIVE", "hint=Username", 0, "company");
            client.ElementSendText("NATIVE", "hint=Password", 0, "company");
            client.Click("NATIVE", "text=Login", 0, 1);
            client.Click("NATIVE", "text=Make Payment", 0, 1);
            client.ElementSendText("NATIVE", "hint=Phone", 0, "09785634");
            client.ElementSendText("NATIVE", "hint=Name", 0, "Eyal");
            client.ElementSendText("NATIVE", "hint=Amount", 0, "100");
            client.Click("NATIVE", "text=Select", 0, 1);
            client.Click("NATIVE", "text=Switzerland", 0, 1);
            client.Click("NATIVE", "text=Send Payment", 0, 1);
            client.Click("NATIVE", "text=Yes", 0, 1);
            client.Click("NATIVE", "text=Logout", 0, 1);
        }
    }
}
