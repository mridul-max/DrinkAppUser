using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Users.Model.DTO.RespononseDTO
{
    public class DrinkRecordResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Mililiters { get; set; }
        public Patient Patient { get; set; }
    }
}
