using System.Collections.Generic;

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
    }
}
