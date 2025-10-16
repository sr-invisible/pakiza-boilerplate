﻿namespace Pakiza.Application.DTOs.User;

public class ResetPasswordDTO
{
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }

    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string Token { get; set; } = default!;
}

public class ValidOTPModel
{
    public string MobileNumber { get; set; } = default!;
    public int OTP { get; set; }
}
