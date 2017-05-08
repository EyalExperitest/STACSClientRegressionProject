using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class EriBankTestOnIOS : ClientStrategy
    {

        public override void RunStartegy()
        {
            String deviceName = client.GetDeviceProperty("device.name");
            Console.WriteLine(deviceName);
            //client.Uninstall("com.experitest.ExperiBank");
            //client.Install(@"C:\MobileApps\IPA\EriBank.ipa", true, false);

            try
            {
                client.Launch("com.experitest.ExperiBank", true, true);

            }
            catch (Exception e)
            {
                String stackTrace = e.StackTrace;
                throw;
            }
            client.ElementSendText("NATIVE", "placeholder=Username", 0, "company");
            client.ElementSendText("NATIVE", "placeholder=Password", 0, "company");
            client.Click("NATIVE", "accessibilityLabel=loginButton", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=makePaymentButton", 0, 1);
            client.ElementSendText("NATIVE", "placeholder=Phone", 0, "09785634");
            client.ElementSendText("NATIVE", "placeholder=Name", 0, "Eyal");
            client.ElementSendText("NATIVE", "placeholder=Amount", 0, "-100");
            client.Click("NATIVE", "accessibilityLabel=countryButton", 0, 1);
            client.Click("NATIVE", "xpath=//*[@accessibilityLabel='Greece']", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=sendPaymentButton", 0, 1);
            client.Click("NATIVE", "xpath=//*[@text='Yes']", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=logoutButton", 0, 1);
            client.Click("NATIVE", "placeholder=Username", 0, 1);
            client.Click("NATIVE", "class=UIButton", 0, 1);
            client.Click("NATIVE", "placeholder=Password", 0, 1);
            client.Click("NATIVE", "class=UIButton", 0, 1);

        }
    }
}
