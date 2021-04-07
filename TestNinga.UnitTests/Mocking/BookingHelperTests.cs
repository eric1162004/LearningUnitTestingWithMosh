using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinga.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_verlappingBookingsExist
    {
        private Booking _existingBooking;
        private Mock<IBookingRespository> _respository;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            _respository = new Mock<IBookingRespository>();
            _respository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking> 
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void BookStartsAndFinishesBeforeAnExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days:  2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
                Reference = "a"
            },_respository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookStartsBeforeAndFinishesInTheMiddleAnExistingBooking_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
                Reference = "a"
            }, _respository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookStartsBeforeAndFinishesAfterAnExistingBooking_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Reference = "a"
            }, _respository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
                Reference = "a"
            }, _respository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookStartsInMiddleOfAndFinishesAfterAnExistingBooking_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Reference = "a"
            }, _respository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookStartsAndFinishesAfterAnExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days:1),
                Reference = "a"
            }, _respository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingsOverlapButNewBookingIsCancalled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Status = "Cancelled",
                Reference = "a"
            }, _respository.Object);

            Assert.That(result, Is.Empty);
        }


        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
