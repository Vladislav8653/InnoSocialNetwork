﻿namespace NotificationsService.Application.CustomExceptions;

public class AlreadyExistsException(string message) : Exception(message);
