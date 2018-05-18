using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var bll = new BLL.BLL(@"c:\tmp\1");
            bll.Add(new Note
            {
                Name = "qqq",
                Content = "demo 1"
            });
            bll.Add(new Note
            {
                Name = "www",
                Content = "demo 2"
            });
            bll.Add(new WebAcc
            {
                Name = "www",
                Url = "www.jdpa.com"
            });
            bll.Add(new CreditCard
            {
                Name = "Visa",
                Number = "1111 2222 3333 demo"
            });
            bll.Add(new CreditCard
            {
                Name = "Amex",
                Number = "4444 5555 3333 7777",
                ExpDate = DateTime.UtcNow
            });
            bll.Flush();

            var bll2 = new BLL.BLL(@"c:\tmp\1");
            var cc2 = bll2.GetItemById(4);

        }
    }
}
