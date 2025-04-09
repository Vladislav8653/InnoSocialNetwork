namespace UserService.Domain.CustomExceptions;

public class UserNotFoundException(string message) : Exception(message);