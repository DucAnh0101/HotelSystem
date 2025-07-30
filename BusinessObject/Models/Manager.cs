using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Manager")]
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "UserID không được để trống")]
        [Display(Name = "Người dùng")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<WorkShift> WorkShifts { get; set; } = new List<WorkShift>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
