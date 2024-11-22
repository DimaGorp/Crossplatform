using System.ComponentModel.DataAnnotations;
namespace WebApp.Models;

public class AccountModel
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(500)]
    public string FullName { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 8)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
