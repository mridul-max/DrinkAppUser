using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Users.Model
{
    public class DrinkRecord
    {
        
        [Key]
        public Guid PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public int Mililiters { get; set; }
        [JsonIgnoreAttribute]
        public Guid PatientId1 { get; set; }
        public Patient Patient { get; set; }
    }
}
