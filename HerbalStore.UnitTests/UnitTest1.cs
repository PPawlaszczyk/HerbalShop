using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Collections.Generic;
using HerbalStore.Domain.Entities;
using HerbalStore.Domain.Abstract;
using HerbalStore.WebUI.Controllers;
using System.Web.Mvc;
using HerbalStore.WebUI.Models;
using HerbalStore.WebUI.HtmlHelpers;


namespace HerbalStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate() // dzielenie dokumentu na strony
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductID =1, Name = "P1" },
                new Product {ProductID =2, Name = "P2" },
                new Product {ProductID =3, Name = "P3" },
                new Product {ProductID =4, Name = "P4" },
                new Product {ProductID =5, Name = "P5" },
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //dzialanie
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;
            //asercja
            Product[] prodAarray = result.Products.ToArray();
            Assert.IsTrue(prodAarray.Length == 2);
            Assert.AreEqual(prodAarray[0].Name, "P4");
            Assert.AreEqual(prodAarray[1].Name, "P5");

        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //definowanie metody pomoczniczej html
            HtmlHelper myHelper = null;
            //konfigurowanie delegatu z lambda
            //przygotowanie
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 10,
                ItemsPerPage = 4
            };
            Func<int, string> pageUrlDelegate = i => "Strona" + 1;
            //dzialanie
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            //assercje
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Stronal"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Strona2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Strona3"">3</a>", result.ToString());

        }



        [TestMethod]
        public void Can_send_Pagination_View_Model() // dzielenie dokumentu na strony
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductID =1, Name = "P1" },
                new Product {ProductID =2, Name = "P2" },
                new Product {ProductID =3, Name = "P3" },
                new Product {ProductID =4, Name = "P4" },
                new Product {ProductID =5, Name = "P5" },
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //dzialanie
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;
            //asercja
            PagingInfo pageinfo = result.PagingInfo;
            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPage,3);
            Assert.AreEqual(pageinfo.TotalItems, 5);
            Assert.AreEqual(pageinfo.TotalPages, 2);

        }

    }
}
