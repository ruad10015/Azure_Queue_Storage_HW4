﻿namespace Azure_Queue_Storage_HW4.Services.Abstracts
{
    public interface IDiscountQueueService
    {
        Task SendMessageAsync(string message);
        Task<string> ReceiveMessageAsync();
    }
}
