using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class BikeSpecifications : Specifications
    {
        public enum eLicenseType
        {
            A,
            A1,
            BB,
            B1
        }

        private readonly eLicenseType m_LicenseType;
        private readonly int m_EngineCapacityInCC;

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }

        public int EngineCapacityInCC
        {
            get
            {
                return m_EngineCapacityInCC;
            }
        }

        public BikeSpecifications(eLicenseType i_LicenseType, int i_EngineCapacityInCC)
            : base(VehicleFactory.eVehicleType.BIKE)
        {
            if (i_EngineCapacityInCC <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i_EngineCapacityInCC));
            }

            m_LicenseType = i_LicenseType;
            m_EngineCapacityInCC = i_EngineCapacityInCC;
        }
    }
}
