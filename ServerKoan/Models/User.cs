namespace Api.Models
{
  public class User
  {
    public string Id { get; set; }
    public string Username { get; set; }
    // TODO: we will need to encrypt this password
    public string Password { get; set; }
  }
}
