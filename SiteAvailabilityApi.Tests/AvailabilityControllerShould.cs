using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SiteAvailabilityApi.Controllers;
using SiteAvailabilityApi.Models;
using SiteAvailabilityApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Tests
{
    [TestClass]
    public class AvailabilityControllerShould
    {
        private Mock<ISiteAvailablityService> _siteAvailablityServiceMock;
        private AvailabilityController _availabilityController;

        public AvailabilityControllerShould()
        {
            _siteAvailablityServiceMock = new Mock<ISiteAvailablityService>();
            _availabilityController = new AvailabilityController(_siteAvailablityServiceMock.Object);
        }

        [TestMethod]
        public async Task GetSiteHistoryByUserAsync()
        {
            // Arrange
            var listOfSiteDto = new List<SiteDto>()
            {
                new SiteDto()
                {
                    Site = "http://www.google.com",
                    Status= true,
                    Timestamp = DateTime.Now,
                    UserId = "123"
                },
                 new SiteDto()
                {
                    Site = "http://www.yahoo.com",
                    Status= true,
                    Timestamp = DateTime.Now,
                    UserId = "123"
                }
            };
            _siteAvailablityServiceMock.Setup(x => x.GetSiteHistoryByUser(It.IsAny<string>())).ReturnsAsync(listOfSiteDto);

            // Act
            var result = await _availabilityController.GetSiteHistoryByUserAsync("123");
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            _siteAvailablityServiceMock.Verify(x => x.GetSiteHistoryByUser(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void PostMessage()
        {
            var site = new SiteDto()
            {
                Site = "http://www.google.com",
                Status = true,
                Timestamp = DateTime.Now,
                UserId = "123"
            };
            // Arrange
            _siteAvailablityServiceMock.Setup(x => x.SendSiteToQueue(It.IsAny<SiteDto>()));

            // Act
            var result = _availabilityController.Post(site) as OkResult;

            // assert
            Assert.AreEqual(200, result.StatusCode);
            _siteAvailablityServiceMock.Verify(x => x.SendSiteToQueue(It.IsAny<SiteDto>()), Times.Once);
        }

        [TestMethod]
        public void ThrowExceptionInPostMessage()
        {
            var site = new SiteDto()
            {
                Site = "http://www.google.com",
                Status = true,
                Timestamp = DateTime.Now,
                UserId = "123"
            };
            // Arrange
            _siteAvailablityServiceMock.Setup(x => x.SendSiteToQueue(It.IsAny<SiteDto>())).Throws<Exception>();

            // Act
            var result = _availabilityController.Post(site) as BadRequestObjectResult;

            // assert
            Assert.AreEqual(400, result.StatusCode);
            _siteAvailablityServiceMock.Verify(x => x.SendSiteToQueue(It.IsAny<SiteDto>()), Times.Once);
        }


        [TestMethod]
        public async Task ThrowsExceptionWhileGettingSiteHistoryByUser()
        {
            // Arrange
            var listOfSiteDto = new List<SiteDto>()
            {
                new SiteDto()
                {
                    Site = "http://www.google.com",
                    Status= true,
                    Timestamp = DateTime.Now,
                    UserId = "123"
               }
            };
            _siteAvailablityServiceMock.Setup(x => x.GetSiteHistoryByUser(It.IsAny<string>())).Throws<Exception>();

            // Act
            var result = await _availabilityController.GetSiteHistoryByUserAsync("123");
            var okResult = result as BadRequestObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
            _siteAvailablityServiceMock.Verify(x => x.GetSiteHistoryByUser(It.IsAny<string>()), Times.Once);
        }
    }
}
