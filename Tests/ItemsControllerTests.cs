using Moq;
using CrudWebAPITU2.Models;
using CrudWebAPITU2.Controllers;
using System.Web.Http.Results;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;



namespace Tests
{
    public class ItemsControllerTests
    {
        private Mock<IItemContext> mockContext;
        private Mock<DbSet<ItemModel>> mockSet;
        private ItemsController controller;


        [SetUp]
        public void Setup()
        {
            var data = new List<ItemModel>
        {
            new ItemModel { Id = 1, Name = "Item1", Description = "Desc1", Price = 10.0m },
            new ItemModel { Id = 2, Name = "Item2", Description = "Desc2", Price = 20.0m }
        }.AsQueryable();

            mockSet = new Mock<DbSet<ItemModel>>();
            mockSet.As<IQueryable<ItemModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ItemModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ItemModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ItemModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<IItemContext>();
            mockContext.Setup(c => c.Item).Returns(mockSet.Object);

            controller = new ItemsController();
        }

        [Test]
        public void GetItems_ReturnsAllItems()
        {
            // Act
            var result = controller.GetItems() as OkNegotiatedContentResult<List<ItemModel>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [Test]
        public void GetItem_ExistingId_ReturnsItem()
        {
            // Arrange
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns(new ItemModel { Id = 1, Name = "Item1", Description = "Desc1", Price = 10.0m });

            // Act
            var result = controller.GetItem(1) as OkNegotiatedContentResult<ItemModel>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Id);
        }

        [Test]
        public void GetItem_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((ItemModel)null);

            // Act
            var result = controller.GetItem(999) as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Item not found!!!", result.Content);
        }

        [Test]
        public void PostItem_ValidModel_ReturnsOk()
        {
            // Arrange
            var item = new ItemModel { Id = 3, Name = "NewItem", Description = "NewDesc", Price = 25.0m };

            // Act
            var result = controller.PostItem(item) as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Item Inserted successfully....", result.Content);
            mockSet.Verify(m => m.Add(It.IsAny<ItemModel>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void PutItem_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var item = new ItemModel { Id = 1, Name = "UpdatedItem", Description = "UpdatedDesc", Price = 30.0m };
            var mockEntry = new Mock<DbEntityEntry<ItemModel>>();
            mockContext.Setup(m => m.Entry(It.IsAny<ItemModel>())).Returns(mockEntry.Object);

            // Act
            var result = controller.PutItem(1, item) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeleteItem_ExistingId_ReturnsOk()
        {
            // Arrange
            var item = new ItemModel { Id = 1, Name = "DeleteItem", Description = "DeleteDesc", Price = 40.0m };
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns(item);

            // Act
            var result = controller.DeleteItem(1) as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Item Deleted successfully....", result.Content);
            mockSet.Verify(m => m.Remove(It.IsAny<ItemModel>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
