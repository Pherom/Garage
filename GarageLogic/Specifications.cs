using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Specifications
    {
        private VehicleFactory.eVehicleType m_VehicleType;
        
        public VehicleFactory.eVehicleType VehicleType
        {
            get
            {
                return m_VehicleType;
            }
        }

        public Specifications(VehicleFactory.eVehicleType i_VehicleType)
        {
            m_VehicleType = i_VehicleType;
        }
    }
}
