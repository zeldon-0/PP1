using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PP1.ServiceLayer.BackgroundServices;
using PP1.ServiceLayer.Services;
using PP2.ServiceLayer.DataLayer;
using PP2.ServiceLayer.DataLayer.Contracts;
using Xunit;

namespace PP2.Tests
{
    public class MessageRepoTests
    {
        [Fact]
        public async Task Should_create_message_in_database()
        {
            //Arrange
            string dbName = Guid.NewGuid().ToString();
            DbContextOptions<MessageContext> options=
                new DbContextOptionsBuilder<MessageContext>().UseInMemoryDatabase(dbName).Options;
            await using var context = new MessageContext(options);
            IMessageRepo messageRepo = new MessageRepo(context);

            var message = new MessageModel
            {
                Id = 1,
                Data = 1
            };

            //Act
            var thread = new Thread(() => messageRepo.Save(message));
            thread.Start();
            var savedMessage = messageRepo.GetById(message.Id);

            //Assert
            savedMessage.Should().NotBeNull();
            savedMessage.Id.Should().Be(1);
        }
    }
}
