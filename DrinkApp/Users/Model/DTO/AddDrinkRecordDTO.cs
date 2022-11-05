using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Users.Model.DTO
{
    public class AddDrinkRecordDTO
    {
        [JsonIgnoreAttribute]
        public DateTime DateTime { get; set; }
        [JsonRequired]
        public int Mililiters { get; set; }
        [JsonRequired]
        public string Patientno { get; set; }
        [JsonIgnore]
        public Patient patient { get; set; }
    }
}
