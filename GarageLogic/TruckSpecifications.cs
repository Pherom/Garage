using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    internal class TruckSpecifications : Specifications
    {
        private bool m_HasRefrigiration;
        private float m_CargoCapacity;

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

        public override void InitSpecifications(List<object> i_SpecificationAnswers)
        {
            if ((float)i_SpecificationAnswers[1] <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            m_HasRefrigiration = (bool)i_SpecificationAnswers[0];
            m_CargoCapacity = (float)i_SpecificationAnswers[1];
        }

        public TruckSpecifications(string i_TypeOfVehicle) : base(i_TypeOfVehicle) { }

        //public TruckSpecifications(bool i_HasRefrigiration, float i_CargoCapacity)
        //    : base(VehicleFactory.eVehicleType.TRUCK)
        //{
        //    if (i_CargoCapacity <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(i_CargoCapacity));
        //    }

        //    m_HasRefrigiration = i_HasRefrigiration;
        //    m_CargoCapacity = i_CargoCapacity;
        //}
    }
}
