﻿namespace CleanArchSample.Infrastructure.MessageBroker
{
    internal sealed class RabbitMqSettings
    {
        public required string Host { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
