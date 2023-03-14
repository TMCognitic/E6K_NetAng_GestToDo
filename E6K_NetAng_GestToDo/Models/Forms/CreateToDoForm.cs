using System.ComponentModel.DataAnnotations;

namespace E6K_NetAng_GestToDo.Models.Forms
{
    public class CreateToDoForm
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; }
    }
}
