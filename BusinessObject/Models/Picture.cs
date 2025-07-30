using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Picture")]
    public class Picture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Mã phòng không được để trống")]
        [Display(Name = "Phòng")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "URL hình ảnh không được để trống")]
        [StringLength(2000, ErrorMessage = "URL hình ảnh không được vượt quá 2000 ký tự")]
        [Display(Name = "URL hình ảnh")]
        public string PictureUrl { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}