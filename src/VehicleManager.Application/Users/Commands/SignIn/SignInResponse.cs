namespace VehicleManager.Application.Users.Commands.SignIn;

internal record SignInResponse(string Token, DateTimeOffset JwtExpires);