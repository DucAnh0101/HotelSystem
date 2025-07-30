using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Mã phòng không được để trống")]
        [Display(Name = "Phòng")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        [Display(Name = "Khách hàng")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Mã quản lý không được để trống")]
        [Display(Name = "Quản lý")]
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "Thời gian vào không được để trống")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Thời gian vào")]
        public DateTime CheckInTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Thời gian ra")]
        public DateTime? CheckOutTime { get; set; }

        [Required(ErrorMessage = "Tổng tiền không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tổng tiền")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        [StringLength(20, ErrorMessage = "Trạng thái không được vượt quá 20 ký tự")]
        [Display(Name = "Trạng thái")]
        public BookingStatus Status { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Manager Manager { get; set; }
    }

    public enum BookingStatus
    {
        Pending,
        Canceled,
        Confirm
    }
}
