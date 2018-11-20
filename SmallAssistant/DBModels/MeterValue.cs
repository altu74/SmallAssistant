using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallAssistant.DBModels
{
    public class MeterValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeterValueId { get; set; }
        [Required]
        public int MeterId { get; set; }
        [Required]
        public DateTime MeterDate { get; set; }
        [Required]
        public decimal Value { get; set; }
        public Meter Meter { get; set; }

    }
}