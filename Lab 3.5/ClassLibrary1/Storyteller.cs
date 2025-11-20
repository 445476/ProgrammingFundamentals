using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_5.Models
{
    public class Storyteller : IEquipmentProvider
    {
        public bool HasEquipment { get; private set; }


        public Storyteller(bool hasEquipment)
        {
            HasEquipment = hasEquipment;
        }

        public string EntertainKids()
        {
            if (!HasEquipment) return "Not enough equipment to entertain";
            return "Entertaining kids with stories!";
        }
    }
}
