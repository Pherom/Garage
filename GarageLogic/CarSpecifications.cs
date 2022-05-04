using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
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

        private readonly eColor m_Color;
        private readonly int m_DoorCount;

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

        public CarSpecifications(eColor i_Color, int i_DoorCount)
            : base(VehicleFactory.eVehicleType.CAR)
        {
            if (i_DoorCount < 2 || i_DoorCount > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(i_DoorCount));
            }

            m_Color = i_Color;
            m_DoorCount = i_DoorCount;
        }

        public struct SpecificationStruct
        {
            public string m_NameOfField;
            public Type m_ValueTypeName;
            //public object m_Value;
        }

        //public List<object> getSpecificationsList()
        //{
        //    List<SpecificationStruct> res = new List<SpecificationStruct>(2); // Number of specifications

        //    //SpecificationStruct currentSpecification = new SpecificationStruct();
        //    //currentSpecification.m_NameOfField = "m_Color";
        //    //= typeof(SomeTypeName);
        //    //res.Add(currentSpecification);

        //    return res;
        //}
    }
}
