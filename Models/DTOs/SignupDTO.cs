namespace Models.DTOs;

public class SignupDTO
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}
public class SignupResult
{
    public bool Success { get; set; }
    public int Code { get; set; }
}