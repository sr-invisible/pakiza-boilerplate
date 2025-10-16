namespace Pakiza.Application.DTOs;

public class BaseDTO
{
    public Guid Id { get; set; }
    public DateTime? DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateUpdated { get; set; }

    public string? CreatedBy { get; set; } = default!;
    public string? UpdatedBy { get; set; } = default!;
    public string? Status { get; set; }
    public bool? IsActive { get; set; } = true;
}
