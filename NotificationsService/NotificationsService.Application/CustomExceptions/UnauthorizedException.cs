﻿namespace NotificationsService.Application.CustomExceptions;

public class UnauthorizedException(string message) : Exception(message);
