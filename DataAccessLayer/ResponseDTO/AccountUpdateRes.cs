using BusinessObject.Models;

namespace DataAccessLayer.ResponseDTO
{
    public class AccountUpdateRes
    {
        public int ID { get; set; }

        public bool IsDelete { get; set; } = false;

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual UserUpdateRes User { get; set; }
    }

    public class UserUpdateRes
    {
        public string FullName { get; set; }
        public Role Role { get; set; }

        public DateOnly DOB { get; set; }

        public bool IsMale { get; set; }
    }
}
