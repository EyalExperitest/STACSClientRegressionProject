using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class AndroidRailTest : ClientStrategy
    {

        public override void RunStartegy()
        {
            client.Launch("com.experitest.ExperiBank/.LoginActivity", true, true);
            if (client.WaitForElement("NATIVE", "xpath=//*[@text='Login']", 0, 30000))
            {
                // If statement
            }
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Username']", 0, "company");
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Password']", 0, "company");
            client.Click("NATIVE", "xpath=//*[@text='Login']", 0, 1);
            client.VerifyElementFound("NATIVE", "xpath=//*[@text='Logout']", 0);
            client.Click("NATIVE", "xpath=//*[@text='Make Payment']", 0, 1);
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Phone']", 0, "0505050505");
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Name']", 0, "user");
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Amount']", 0, "5000");
            client.Click("NATIVE", "xpath=//*[@text='Select']", 0, 1);
            client.ElementListSelect("id=countryList", "text=Iran", 0, true);
            client.Click("NATIVE", "xpath=//*[@text='Send Payment']", 0, 1);
            client.Click("NATIVE", "xpath=//*[@text='Yes']", 0, 1);
            client.VerifyElementFound("NATIVE", "xpath=//*[@text='Expense Report']", 0);
            client.Click("NATIVE", "xpath=//*[@text='Logout']", 0, 1);
            client.VerifyElementFound("NATIVE", "xpath=//*[@text='Login']", 0);
            client.SendText("{HOME}");
            client.Launch("chrome:http://www.google.com", true, true);
            if (client.WaitForElement("WEB", "xpath=//*[@id='lst-ib']", 0, 10000))
            {
                // If statement
            }
            client.Click("WEB", "xpath=//*[@id='lst-ib']", 0, 1);
            client.SendText("seeTest");
            client.Click("WEB", "xpath=//*[@name='btnG']", 0, 1);
            client.VerifyElementFound("WEB", "xpath=//*[@text='Experitest']", 0);

        }
    }
}
