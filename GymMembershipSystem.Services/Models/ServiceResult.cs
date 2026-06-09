namespace GymMembershipSystem.Services.Models;

public class ServiceResult
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;

    public static ServiceResult Success(string message) => new() { Succeeded = true, Message = message };
    public static ServiceResult Failure(string message) => new() { Succeeded = false, Message = message };
}
