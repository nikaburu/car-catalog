using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CarCatalog.Dal;

namespace CarCatalog
{
    class Program
    {
        static void Main()
        {
            //CarsFinder singleton usage
            var app = new Application(CarsFinder.Instance);

            app.Run();
        }
    }
}