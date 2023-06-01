using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjVideoClub.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Display(Name = "Nome categoria")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public string? NomeCat { get; set; }

        //para a categoria Filme
        public virtual ICollection<Filme>? Filmes { get; set; }
    }
}
