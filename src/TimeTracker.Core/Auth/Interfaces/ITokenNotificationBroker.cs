using CSharpFunctionalExtensions;

namespace TimeTracker.Core.Auth.Interfaces;

public interface ITokenNotificationBroker
{
    Task<Result> SendToken(string recipient, string recipientName, string subject, string token, string resetUrl);
}