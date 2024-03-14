using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Prato
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(15)]
        public string? Tag { get; set; }

        public string? Imagem { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Descricao { get; set; }

        [ForeignKey("Restaurante")]
        public int RestauranteId { get; set; }

        [JsonIgnore]
        public Restaurante? Restaurante { get; set; }

     
    }
}
