using System.ComponentModel.DataAnnotations;

namespace zSpaceWinApp.Model
{
    public class Downloads
    {
        [Key]
        [Required]
        public string DeviceId { get; set; }
        [Key]
        [Required]
        public string PackageId { get; set; }
        public string DownloadedOn { get; set; }
    }
}
