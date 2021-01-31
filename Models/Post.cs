using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_laboration.Models
{
    public class Post
    {
        public int Id { get; set; }

        [DisplayName("Namn")]
        [StringLength(20)]
        public string Author { get; set; } = "Anonym";

        [Required(ErrorMessage = "Du måste ange ett meddelande!")]
        [StringLength(100)]
        [DisplayName("Meddelande (krävs)")]
        public string Content { get; set; }

        public string Timestamp { get; set; } = DateTime.Now.ToString();

        [DisplayName("Humör")]
        public string Mood { get; set; }

        [DisplayName("Kom ihåg mig")]
        public bool RememberMe { get; set; }
    }
}
