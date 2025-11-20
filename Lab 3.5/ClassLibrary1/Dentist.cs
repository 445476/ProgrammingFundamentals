using Lab_3_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_5.Models
{
     public class Dentist : IEquipmentProvider

{

    public bool HasEquipment { get; private set; }


    public string Treat()

    {

        if (!HasEquipment) return "Not enough equipment to entertain kids with marvelous dental practices";

        return "Entertaining kids with drill!";

    }

}
}
