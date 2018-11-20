using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallAssistant.DBModels
{
    public class RateType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RateTypeId { get; set; }
        [Required]
        public string RateTypeName { get; set; }
        public IList<Rate> Rates { get; set; }

        public RateType()
        {
            Rates = new List<Rate>();
        }

    }
}