using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjVideoClub.Models
{
    public class RegistoAluguer
    {
        public int Id { get; set; }

        [Display(Name = "Filme")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public int FilmeId { get; set; }
        public virtual Filme? Filme { get; set; }

        [Display(Name = "Utilizador")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public int UtilizadorId { get; set; }
        public virtual Utilizador? Utilizador { get; set;}

        [Display(Name = "Data Início")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Fim")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public DateTime? DataFim { get; set; }
    }
}
