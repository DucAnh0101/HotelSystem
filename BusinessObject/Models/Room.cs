using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Room")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Trạng thái phòng không được để trống")]
        [StringLength(20, ErrorMessage = "Trạng thái không được vượt quá 20 ký tự")]
        [Display(Name = "Trạng thái")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Giá thuê phòng không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá thuê phải lớn hơn 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá thuê")]
        public decimal PriceForRent { get; set; }

        [Required(ErrorMessage = "Giá theo giờ không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá theo giờ phải lớn hơn 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá theo giờ")]
        public decimal PricePerHour { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Mã công việc")]
        public int? WorkId { get; set; }

        public virtual ICollection<WorkShift> WorkShifts { get; set; } = new List<WorkShift>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public enum Status
    {
        Rented,
        Cleanning,
        Available
    }

    public enum RoomType
    {
        Single,
        Double,
        Suite,
        Deluxe
    }
}
