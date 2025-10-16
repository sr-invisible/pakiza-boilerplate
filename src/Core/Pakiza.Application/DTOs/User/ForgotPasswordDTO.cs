namespace Pakiza.Application.DTOs.User;

public class ForgotPasswordDTO
{
    [Required(ErrorMessage = "Mobile number is required.")]
    [RegularExpression(@"^\d{1,14}$", ErrorMessage = "Invalid mobile number format. Use international format, e.g., +1234567890.")]
    public string MobileNumber { get; set; } = string.Empty;
}
