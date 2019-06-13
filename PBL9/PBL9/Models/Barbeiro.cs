using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL9.Models
{
    public class Barbeiro
    {
        public int BarbeiroId { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
        public int AnoEntrada { get; set; }
        public string Endereco { get; set; }
    }
}