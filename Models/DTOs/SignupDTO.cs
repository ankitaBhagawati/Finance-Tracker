namespace Models.DTOs;

public class SignupDTO
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}