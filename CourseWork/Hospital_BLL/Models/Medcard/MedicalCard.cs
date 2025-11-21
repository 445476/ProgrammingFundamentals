using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital_BLL.Models.Medcard
{
    public class MedicalCard
    {
        [JsonInclude] 
        public List<Entry> Entries { get; set; }
        [JsonInclude] 
        public Guid Id { get; set; }

        public MedicalCard(Guid patientId)
        {
            Id = patientId;
            Entries = new List<Entry>();
        }
        public MedicalCard()
        {
            Entries = new List<Entry>();
        }
        public void AddEntry(Entry entry)
        {
            Entries.Add(entry);
        }
    }
}
