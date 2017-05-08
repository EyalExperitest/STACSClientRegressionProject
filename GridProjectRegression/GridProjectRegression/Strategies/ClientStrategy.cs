using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using experitestClient;

namespace GridProjectRegression
{ 
    public abstract class ClientStrategy
    {
        protected Client client;
        public ClientStrategy()
        {
        }

        public Client Client
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }
        public abstract void RunStartegy();

    }
}