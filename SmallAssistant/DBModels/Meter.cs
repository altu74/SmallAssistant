using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallAssistant.DBModels
{
    public class Meter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeterId { get; set; }
        [Required]
        public string MeterName { get; set; }
        public string MeterNumber { get; set; }
        public IList<MeterValue> MeterValues{ get; set; }
        public IList<MeterRate> MeterRates { get; set; }

        public Meter()
        {
            MeterValues = new List<MeterValue>();
            MeterRates = new List<MeterRate>();
        }
    }
}