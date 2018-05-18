using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using BLL;

namespace BLL.Tests
{
    [TestClass]
    public class UnitTest1
    {
        UnityContainer _container;
        IBLL _bll;
        string  StoragePath = @".\1";

        public UnitTest1()
        {
            _container = new UnityContainer();
            _container.RegisterType<IBLL, BLL>();
        }

        [TestMethod]
        public void TestInitial()
        {
            _bll = GetBLL();
            _bll.Add(new Note
            {
                Name = "qqq",
                Content = "demo 1"
            });
            _bll.Add(new Note
            {
                Name = "www",
                Content = "demo 2"
            });
            _bll.Add(new WebAcc
            {
                Name = "www",
                Url = "www.jdpa.com"
            });
            _bll.Add(new CreditCard
            {
                Name = "Visa",
                Number = "1111 2222 3333 demo"
            });
            _bll.Add(new CreditCard
            {
                Name = "Amex",
                Number = "4444 5555 3333 7777",
                ExpDate = DateTime.UtcNow
            });
            _bll.Flush();

            var bll2 = GetBLL();
            var cc2 = bll2.GetItemById(4);
            Assert.IsTrue(cc2.GetType() == typeof(CreditCard));
            Assert.IsTrue(cc2.Name == "Visa");
            File.Delete(StoragePath);
        }

        [TestMethod]
        public void TestDelete()
        {
            _bll = GetBLL();
            _bll.Add(new Note
            {
                Name = "qqq",
                Content = "demo 1"
            });
            _bll.Add(new Note
            {
                Name = "www",
                Content = "demo 2"
            });
            _bll.Add(new WebAcc
            {
                Name = "www",
                Url = "www.jdpa.com"
            });
            _bll.Add(new CreditCard
            {
                Name = "Visa",
                Number = "1111 2222 3333 demo"
            });
            _bll.Add(new CreditCard
            {
                Name = "Amex",
                Number = "4444 5555 3333 7777",
                ExpDate = DateTime.UtcNow
            });
            _bll.Flush();

            var bll2 = GetBLL();
            bll2.DeleteById(4);
            var cc2 = bll2.GetItemById(4);
            Assert.IsNull(cc2);
            File.Delete(StoragePath);
        }

        [TestMethod]
        public void TestSearch()
        {
            _bll = GetBLL();
            _bll.Add(new Note
            {
                Name = "qqq",
                Content = "demo 1"
            });
            _bll.Add(new Note
            {
                Name = "www",
                Content = "demo 2"
            });
            _bll.Add(new WebAcc
            {
                Name = "www",
                Url = "www.jdpa.com"
            });
            _bll.Add(new CreditCard
            {
                Name = "Visa",
                Number = "1111 2222 3333 demo"
            });
            _bll.Add(new CreditCard
            {
                Name = "Amex",
                Number = "4444 5555 3333 7777",
                ExpDate = DateTime.UtcNow
            });
            _bll.Flush();

            var bll2 = GetBLL();
            var col2 = new List<BaseObj>( bll2.GetFiltered("ww"));
            Assert.IsTrue(col2.Count == 2);
            File.Delete(StoragePath);
        }

        private IBLL GetBLL()
        {
            var bll = _container.Resolve<IBLL>(new ResolverOverride[]
                {
                    new ParameterOverride("filePath", StoragePath)
                });
            return bll;
        }
    }
}
