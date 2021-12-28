using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationTask.Model
{
    public class Adapter
    {
        public bool isUsed(Product product)
        {
            return true;
        }
    }

    public class ChilledAdapter: Adapter
    {
        public bool isUsed(Cell cell)
        {
            return ((ChilledCell)cell).IsChilled;
        }
    }

    public class HazardousAdapter : Adapter
    {
        public bool isUsed(Cell cell)
        {
            return ((HazardousCell)cell).IsHazardous;
        }
    }
}
