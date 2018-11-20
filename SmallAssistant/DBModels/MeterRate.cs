using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallAssistant.DBModels
{
    public class MeterRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeterRateId { get; set; }
        [Required]
        public int MeterId { get; set; }
        [Required]
        public int RateId { get; set; }
        [Required]
        public DateTime ActiveFrom { get; set; }
        //[Required]
        //public decimal MeterRateValue { get; set; }
        public Rate Rate { get; set; }
        public Meter Meter { get; set; }
    }
}