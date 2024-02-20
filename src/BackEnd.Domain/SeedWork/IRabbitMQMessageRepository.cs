using System;
namespace BackEnd.Domain.SeedWork;

public interface IRabbitMQMessageRepository
{
    Task PublishMessage<TOutPut>(string queueName, TOutPut message, bool disposed = true);
    Task ConsumeMessage<TOutPut>(string queueName);
}