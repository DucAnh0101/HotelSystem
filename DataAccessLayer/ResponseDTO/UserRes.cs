using BusinessObject.Models;

namespace DataAccessLayer.ResponseDTO
{
    public class UserRes
    {
        public int ID { get; set; }

        public Role Role { get; set; }

        public string FullName { get; set; }

        public string? CitizenId { get; set; }

        public DateOnly DOB { get; set; }

        public string AvtUrl { get; set; }

        public bool IsMale { get; set; }
    }
}
