using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallAssistant.DBModels
{
    public class RateValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateValueId { get; set; }
        [Required]
        public int RateId { get; set; }
        public decimal? MeterValueFrom { get; set; }
        public decimal? MeterValueTo { get; set; }
        public TimeSpan? TimeFrom { get; set; }
        public TimeSpan? TimeTo { get; set; }
        [Required]
        public DateTime ActiveFrom { get; set; }
        [Required]
        [DefaultValue(0)]
        public decimal Value { get; set; }
        public Rate Rate { get; set; }

    }
}