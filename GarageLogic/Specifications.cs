using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class Specifications
    {
        private string m_VehicleType;
        public string VehicleType
        {
            get 
            {
                return m_VehicleType; 
            }
        }

        protected Specifications(string i_VehicleType)
        {
            m_VehicleType = i_VehicleType;
        }
        public abstract void InitSpecifications(List<object> i_SpecificationAnswers);

        //private VehicleFactory.eVehicleType m_VehicleType;
        //public VehicleFactory.eVehicleType VehicleType
        //{
        //    get
        //    {
        //        return m_VehicleType;
        //    }
        //}

        //public Specifications(VehicleFactory.eVehicleType i_VehicleType)
        //{
        //    m_VehicleType = i_VehicleType;
        //}
    }
}
