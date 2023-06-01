using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace ProjVideoClub.Models
{
    public class Utilizador
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public string? Nome { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public string? Email { get; set; }

        [Display(Name = "Password")]
        public string? Password { get; set; }

        //para a entidade item:
        public virtual ICollection<RegistoAluguer>? RegistoAluguer { get;set; }
    }
}
