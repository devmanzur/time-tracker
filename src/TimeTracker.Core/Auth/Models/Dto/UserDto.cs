namespace TimeTracker.Core.Auth.Models.Dto;

public readonly record struct UserDto(string Name, string Email, bool EmailConfirmed, string Phone,
    bool PhoneConfirmed);