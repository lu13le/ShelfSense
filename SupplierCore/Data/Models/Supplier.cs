using System.ComponentModel.DataAnnotations;

namespace SupplierCore.Data.Models;

public class Supplier
{
    [Key] 
    public Guid Id { get; set; }

    [Required]
    [StringLength(150, ErrorMessage = "Supplier name cannot exceed 150 characters.")]
    public string Name { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(255, ErrorMessage = "Contact email cannot exceed 255 characters.")]
    public string ContactEmail { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
    public string PhoneNumber { get; set; }

    [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
    public string Address { get; set; }

    [Required] 
    public DateTime CreatedAt { get; set; }
}