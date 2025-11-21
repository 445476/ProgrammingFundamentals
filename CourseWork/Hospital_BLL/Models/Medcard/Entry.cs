using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital_BLL.Models.Medcard
{    public class Entry 
    {
        [JsonInclude]
        public Guid Id { get; set; }
        [JsonInclude] 
        public DateTime Date { get; set; }
        [JsonInclude] 
        public string Diagnosis { get; set; }
        [JsonInclude] 
        public string Notes { get; set; }

        // Порожній конструктор для десеріалізації
        public Entry() { }

        // Конструктор
        public Entry(Guid appointmentId, DateTime date, string diagnosis, string notes)
        {
            appointmentId = appointmentId;
            Date = date;
            Diagnosis = diagnosis;
            Notes = notes;
        }
    }
}
