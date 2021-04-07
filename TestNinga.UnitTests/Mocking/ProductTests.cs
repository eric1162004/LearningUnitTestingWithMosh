using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinga.UnitTests.Mock
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void GetPrice_GolderCustomer_Apply30PercentDiscount()
        {
            var product = new Product { ListPrice = 100 };

            var result  = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));

        }
    }
}
