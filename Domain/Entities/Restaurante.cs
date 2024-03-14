using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Restaurante
    {
        public Restaurante()
        {
            Pratos = new Collection<Prato>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Nome { get; set; }

        public ICollection<Prato>? Pratos { get; set; }
    }
}
