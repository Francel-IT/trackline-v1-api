using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trigger
{
    public class Asset  

    {

        public int Id { get; set; }

        public Guid Guid { get; set; }


        public string Tag { get; set; }

        public string Itemname { get; set; }

        public Guid? Type { get; set; }

        public Guid? Category { get; set; }


        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string Active { get; set; }

        public string Status { get; set; }

        public Guid Location { get; set; }

        public bool IsConsumed { get; set; }

        public bool IsAllowedToGoOut { get; set; }

        public string AssetImage { get; set; }

        public string AssetImagePath { get; set; }

    }

}
