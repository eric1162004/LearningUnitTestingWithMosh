using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinga.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private TestNinja.Fundamentals.Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new TestNinja.Fundamentals.Math();
        }
        
        [Test]
        [Ignore("Becuase I wanted to!")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 2, 2)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_returnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            // Assert.That(result, Is.Not.Empty);
            // Assert.That(result.Count(), Is.EqualTo(3));

            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 })); // Ensure the result contains these items

            // Other helpful methods
            // Assert.That(result, Is.Ordered); // Ensured the result is ordered
            // Assert.That(result, Is.Unique); // Ensured the result is unique
        }

    }
}
