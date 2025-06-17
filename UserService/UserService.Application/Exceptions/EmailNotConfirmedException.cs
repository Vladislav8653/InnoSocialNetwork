namespace UserService.Application.Exceptions;

public class EmailNotConfirmedException(string message) : Exception(message);