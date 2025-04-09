namespace UserService.Domain.CustomExceptions;

public class EmailNotConfirmedException(string message) : Exception(message);