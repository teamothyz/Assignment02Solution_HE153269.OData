using BusinessObject.Models;

namespace BusinessObject.API.Response
{
    public class GetInfoResponseModel
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

        public Publisher Publisher { get; set; } = null!;

        public Role Role { get; set; } = null!;
    }
}
