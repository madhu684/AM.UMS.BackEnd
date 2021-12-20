using AM.UMS.BackEnd.DAL;
using AM.UMS.BackEnd.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject.DAL
{
    [TestFixture]
    public class AuthService
    {
        private AuthenticationService _authenticationService;
        public void Setup()
        {
            _authenticationService = new AuthenticationService();
        }

        [Test]
        public void Login_WithCorrectForExistingUsername_ReturnAccountForCorrectUsername()
        {
            ////Arrange
            string username = "mysqladmin";
            string password = "mysqladmin123";
           

            ////Act
            User user = _authenticationService.Authenticate(username, password);

            ////Assert
            string actualUsername = user.Username;
            Assert.AreEqual(username, actualUsername);
        }

        [Test]
        public void Login_WithInCorrectForExistingUsername_ThrowInvalidPasswordExceptionForUsername()
        {
            ////Arrange
            string username = "mysqladmin";
            string password = "mysqladmin1234";

            ////Act
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _authenticationService.Authenticate(username, password));

            ////Assert
            string actualUsername = ex.ToString();
            Assert.AreEqual(username, actualUsername);
        }

    }
}
