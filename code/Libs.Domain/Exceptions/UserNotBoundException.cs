namespace Libs.Domain.Exceptions;

public class UserNotBoundException : Exception
{
    private const string USER_NOT_BOUND = "User is not bound to this entity.";

    public UserNotBoundException() : base(USER_NOT_BOUND) { }
    public UserNotBoundException(string message) : base(message) { }
}