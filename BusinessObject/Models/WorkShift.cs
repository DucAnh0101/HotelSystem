using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("WorkShift")]
    public class WorkShift
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Mã phòng không được để trống")]
        [Display(Name = "Phòng")]
        public int RoomId { get; set; }

        [Display(Name = "Nhân viên")]
        public int? StaffId { get; set; }

        [Display(Name = "Quản lý")]
        public int? ManagerId { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu ca làm việc không được để trống")]
        [DataType(DataType.Time)]
        [Display(Name = "Bắt đầu ca làm")]
        public DateTime WorkShipStart { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc ca làm việc không được để trống")]
        [DataType(DataType.Time)]
        [Display(Name = "Kết thúc ca làm")]
        public DateTime WorkShipEnd { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        [ForeignKey("StaffId")]
        public virtual Staff Staff { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Manager Manager { get; set; }
    }
}
