using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Staffs")]
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "UserID không được để trống")]
        [Display(Name = "Người dùng")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<WorkShift> WorkShiftList { get; set; } = new List<WorkShift>();
    }

    public enum WorkingStatus
    {
        Free,
        Working,
        OffDuty,
    }
}
