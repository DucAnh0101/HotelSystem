using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.RequestDTO
{
    public class UserUpdateReq
    {
        [Required(ErrorMessage = "Mã công dân không được để trống")]
        [StringLength(12, ErrorMessage = "Mã công dân không được vượt quá 12 ký tự")]
        public string CitizenId { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        public DateOnly DOB { get; set; }

        [StringLength(255, ErrorMessage = "Avatar URL không được vượt quá 255 ký tự")]
        public string AvtUrl { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        public bool IsMale { get; set; }
    }
}
