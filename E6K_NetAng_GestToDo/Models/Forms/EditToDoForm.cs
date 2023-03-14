using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E6K_NetAng_GestToDo.Models.Forms
{
#nullable disable
    public class EditToDoForm
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; }
    }
}
