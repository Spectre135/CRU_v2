using System;
using System.ServiceModel;
using WcfAuth;

namespace WCFAuthConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(AuthService));
            host.Open();
            Console.WriteLine("Auth Service Host Open Sucessfully ...");
            Console.ReadLine();
        }
    }
}
