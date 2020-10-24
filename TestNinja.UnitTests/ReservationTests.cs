using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class ReservationTests
    {
        [Fact]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanBeCancelledBy_SameUserCancellingTheReservaction_ReturnsTrue()
        {
            // Arrange
            var user = new User();
            var reservation = new Reservation { MadeBy = user };

            // Act
            var result = reservation.CanBeCancelledBy(user);
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanBeCancelledBy_AnotherUserCancellingReservation_ReturnsFalse()
        {
            // Arrange
            var reservation = new Reservation();
            // OR 
            // var reservation = new Reservation { MadeBy = new User() };


            // Act
            var result = reservation.CanBeCancelledBy(new User());
            
            // Assert
            Assert.False(result);
        }
    }
}
