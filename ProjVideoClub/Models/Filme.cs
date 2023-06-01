using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace ProjVideoClub.Models
{
    public class Filme
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public string? Titulo { get; set; }

        [Display(Name = "Ano")]
        public int Ano { get; set;}

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set;}

        // para a entidade RegistoAluguer
        public virtual ICollection<RegistoAluguer>? RegistoAluguer { get; set; }
    }
}
