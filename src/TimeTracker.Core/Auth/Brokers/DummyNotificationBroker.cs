using TimeTracker.Core.Auth.Interfaces;
using CSharpFunctionalExtensions;

namespace TimeTracker.Core.Auth.Brokers;

public class DummyNotificationBroker : ITokenNotificationBroker
{
    public async Task<Result> SendToken(string recipient, string recipientName, string subject, string token, string resetUrl)
    {
        return Result.Success();
    }
}