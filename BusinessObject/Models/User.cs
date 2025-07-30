using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống")]
        [StringLength(20, ErrorMessage = "Vai trò không được vượt quá 20 ký tự")]
        [Display(Name = "Vai trò")]
        public Role Role { get; set; } = Role.Customer;

        [Required(ErrorMessage = "Tên bạn không được để trống")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Mã công dân không được để trống")]
        [StringLength(12, ErrorMessage = "Mã công dân không được vượt quá 12 ký tự")]
        [Display(Name = "Mã công dân")]
        public string CitizenId { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateOnly DOB { get; set; }

        [StringLength(255, ErrorMessage = "Avatar URL không được vượt quá 255 ký tự")]
        [Display(Name = "Avatar")]
        public string AvtUrl { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        [Display(Name = "Giới tính")]
        public bool IsMale { get; set; }

        [Display(Name = "Tài khoản")]
        public int? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public virtual ICollection<Staff> Staffs { get; set; } = new List<Staff>();
        public virtual ICollection<Manager> Managers { get; set; } = new List<Manager>();
        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }

    public enum Role
    {
        Admin,
        Customer,
        Manager,
        Staff
    }
}
