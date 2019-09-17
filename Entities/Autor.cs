using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiNetCore.Helpers;

namespace WebApiNetCore.Entities
{
    public class Autor : IValidatableObject 
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "El campo nombre debe tener {1} caracteres o mas")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [Range(18, 120)]
        public int Edad { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }
        public List<Libros> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula", new string[] { nameof(Nombre) });
                }
            }
        }
    }
}
