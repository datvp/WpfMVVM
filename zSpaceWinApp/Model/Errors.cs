using System.ComponentModel.DataAnnotations;

namespace zSpaceWinApp.Model
{
    public class Errors
    {
        [Key]
        [Required]
        public string FuncId { get; set; }
        [Required]
        public string Description { get; set; }
        public string OccuredOn { get; set; }
        public string ModifiedBy { get; set; }

    }
}
