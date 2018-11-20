using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallAssistant.DBModels
{
    public class Rate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateId { get; set; }
        [Required]
        public int RateTypeId { get; set; }
        [Required]
        public string RateName { get; set; }
        public RateType RateType { get; set; }
        public IList<RateValue> RateValues { get; set; }
        public IList<MeterRate> MeterRates { get; set; }

        public Rate()
        {
            RateValues = new List<RateValue>();
            MeterRates = new List<MeterRate>();
        }

    }
}