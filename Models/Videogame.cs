using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideogameStorage.Models
{
    [Table("Videogame")]
    public class Videogame
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("videogame_id")]
        [Display(Name = "Videogame Id")] 
        // DisplayName is for frontend html displaying in cshtml file eg. @Html.DisplayNameFor(model => model.VideogameId) -> shows "Videogame Id"
        public int VideogameId { get; set; }

        [Required]
        [Column("name", TypeName ="nvarchar(100)")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Column("type", TypeName ="nvarchar(100)")]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Column("release_date", TypeName ="int")]
        [Range(1950, 2050)]
        [Display(Name = "Release Date")]
        public int ReleaseDate { get; set; }

        [Required]
        [Column("rating", TypeName ="float(1)")]
        [Range(1, 5.0)]
        [Display(Name = "Rating")]
        public float Rating { get; set; }

        [Required]
        [Column("console_exclusive", TypeName ="boolean")]
        [Display(Name = "Console Exclusive")]
        public bool ConsoleExclusive {get; set;}

        //new Videogame { Id = 0, Name = "World of Warcraft", Type = "MMO", ReleaseDate = 2004, Rating = 5, ConsoleExclusive = false } 

        public override string ToString()
        {
            return "{ " + VideogameId + ", " + Name + ", "+ Type + ", "+ ReleaseDate + ", " + Rating + ", " + ConsoleExclusive + " }";
        }
    }
}