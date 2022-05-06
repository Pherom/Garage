using System.Collections.Generic;

namespace GarageLogic
{
    internal class CarSpecifications : Specifications
    {
        public enum eColor
        {
            RED,
            WHITE,
            GREEN,
            BLUE
        }

        private eColor m_Color;
        private int m_DoorCount;

        public eColor Color
        {
            get
            {
                return m_Color;
            }
        }

        public int DoorCount
        {
            get
            {
                return m_DoorCount;
            }
        }

        public override void InitSpecifications(List<object> i_SpecificationAnswers)
        {
            if ((int)i_SpecificationAnswers[1] < 2 || (int)i_SpecificationAnswers[1] > 5)
            {
                throw new ValueOutOfRangeException(2, 5);
            }

            m_Color = (eColor)i_SpecificationAnswers[0];
            m_DoorCount = (int)i_SpecificationAnswers[1];
        }

        public CarSpecifications(string i_TypeOfVehicle) : base(i_TypeOfVehicle) { }
    }
}
