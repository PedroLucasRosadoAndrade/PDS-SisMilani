using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace ProjetoDePDS3_A.Models
{
    internal class FilmeValitator: AbstractValidator<Filme>
    {
        public FilmeValitator()
        {

        
              RuleFor(x => x.Nome).NotEmpty().WithMessage("O campo `Nome` é Obrigatório. Favor Preencher");
       
        
        }
    }

   

    
}
