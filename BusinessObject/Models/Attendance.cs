using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu ca làm việc không được để trống")]
        [DataType(DataType.Time)]
        [Display(Name = "Bắt đầu ca làm")]
        public DateTime WorkShiftStart { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc ca làm việc không được để trống")]
        [DataType(DataType.Time)]
        [Display(Name = "Kết thúc ca làm")]
        public DateTime WorkShiftEnd { get; set; }

        [Required(ErrorMessage = "Mã quản lý không được để trống")]
        [Display(Name = "Quản lý")]
        public int ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Manager Manager { get; set; }
    }
}