using Xunit;
using Moq;
using ItemAPI.Controllers;
using ItemAPI.Models;
using ItemAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.tests
{

    public class ItemsControllerTests
    {
        private readonly ItemsController _controller;
        private readonly Mock<IItemsRepo> _mockRepo;

        public ItemsControllerTests()
        {
            _mockRepo = new Mock<IItemsRepo>();
            _controller = new ItemsController(_mockRepo.Object);
        }

        // Test cases go here


        [Fact]
        public void Get_ReturnsAllItems()
        {
            // Arrange
            var items = new List<ItemInfoModel> { new ItemInfoModel { Id = 1, ItemName = "Item1", ItemDescription="Decs"} };
            _mockRepo.Setup(repo => repo.GetItems()).Returns(items);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsType<List<ItemInfoModel>>(result);
            Assert.Single(result);
        }


        [Fact]
        public async Task Get_ReturnsItemById()
        {
            // Arrange
            var item = new ItemInfoModel { Id = 1, ItemName = "Item1", ItemDescription = "Decs" };
            _mockRepo.Setup(repo => repo.GetItemById(1)).ReturnsAsync(item);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ItemInfoModel>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Get_ReturnsNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetItemById(1)).ReturnsAsync((ItemInfoModel)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



        [Fact]
        public async Task Post_ReturnsOk()
        {
            // Arrange
            var item = new ItemInfoModel { Id = 1, ItemName = "Item1", ItemDescription = "Decs" };
            _mockRepo.Setup(repo => repo.SaveItem(item)).ReturnsAsync("Item saved");

            // Act
            var result = await _controller.Post(item);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Item saved", okResult.Value);
        }



        [Fact]
        public async Task Put_ReturnsOk()
        {
            // Arrange
            var item = new ItemInfoModel { Id = 1, ItemName = "Item1", ItemDescription = "Decs" };
            _mockRepo.Setup(repo => repo.UpdateItem(item)).ReturnsAsync("Item updated");

            // Act
            var result = await _controller.Put(item);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Item updated", okResult.Value);
        }



        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteItem(1)).ReturnsAsync("Item deleted");

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Item deleted", okResult.Value);
        }

    }
}

