namespace BusinessObject.API.Response
{
    public class LoginResponseModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Source { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int RoleId { get; set; }

        public int PublisherId { get; set; }

        public DateTime HireDate { get; set; }

        public string Role { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
