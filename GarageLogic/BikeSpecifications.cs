using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
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

        private eLicenseType m_LicenseType;
        private int m_EngineCapacityInCC;

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

        public override void InitSpecifications(List<object> i_SpecificationAnswers)
        {
            if ((int)i_SpecificationAnswers[1] <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            m_LicenseType = (eLicenseType)i_SpecificationAnswers[0];
            m_EngineCapacityInCC = (int)i_SpecificationAnswers[1];
        }

        public BikeSpecifications(string i_TypeOfVehicle) : base(i_TypeOfVehicle) { }

        //public BikeSpecifications(eLicenseType i_LicenseType, int i_EngineCapacityInCC)
        //    : base(VehicleFactory.eVehicleType.BIKE)
        //{
        //    if (i_EngineCapacityInCC <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(i_EngineCapacityInCC));
        //    }

        //    m_LicenseType = (eLicenseType)i_LicenseType;
        //    m_EngineCapacityInCC = (int)i_EngineCapacityInCC;
        //}
    }
}
