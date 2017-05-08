using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class GetDeviceAndProperties : ClientStrategy
    {

        public override void RunStartegy()
        {
            String deviceName = client.GetDeviceProperty("device.name");
            Console.WriteLine("deviceName : "+deviceName);
            String hostName = client.GetDeviceProperty("device.host");
            Console.WriteLine("hostName : " + hostName);
            String isRemote = client.GetDeviceProperty("device.remote");
            Console.WriteLine("hostName : " + hostName);

        }
    }
}
