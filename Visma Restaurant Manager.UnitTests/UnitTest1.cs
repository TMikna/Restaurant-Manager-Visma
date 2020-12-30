using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Visma_Restaurant_Manager.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DataBaseLoadProductsTest() {
            DataBase dataBase = new DataBase();
            dataBase.addProduct(new Product(1, "Cabbage", 10, "kg", 0, 3));

            List<Product> newProducts = new List<Product>();
            newProducts.Add(new Product(1, "Banana", 10, "kg", 0, 3));
            newProducts.Add(new Product(1, "Pizza", 10, "kg", 0, 3));

            dataBase.loadProducts(newProducts);
            Assert.Equals(dataBase.products.Count(), 3);
        }
    }
}
