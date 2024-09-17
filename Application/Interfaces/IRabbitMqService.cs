using RabbitMQ.Client;

public interface IRabbitMqService
{
    string GetQueueName();
    ConnectionFactory GetConnectionFactory();
    void SendMessage(object message);
}
