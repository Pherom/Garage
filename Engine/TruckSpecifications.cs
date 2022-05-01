using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class TruckSpecifications : Specifications
    {
        private readonly bool m_HasRefrigiration;
        private readonly float m_CargoCapacity;

        public bool HasRefrigiration
        {
            get
            {
                return m_HasRefrigiration;
            }
        }

        public float CargoCapacity
        {
            get
            {
                return m_CargoCapacity;
            }
        }

        public TruckSpecifications(bool i_HasRefrigiration, float i_CargoCapacity)
            : base(VehicleFactory.eVehicleType.TRUCK)
        {
            if (i_CargoCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i_CargoCapacity));
            }

            m_HasRefrigiration = i_HasRefrigiration;
            m_CargoCapacity = i_CargoCapacity;
        }
    }
}
