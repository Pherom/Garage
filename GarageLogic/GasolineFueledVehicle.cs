using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class GasolineFueledVehicle : Vehicle
    {
        public enum eFuelType
        {
            OCTAN95,
            OCTAN96,
            OCTAN98,
            SOLER
        }

        private readonly eFuelType m_FuelType;
        private readonly float m_FuelTankCapacityInLiters;
        private float m_CurrentFuelAmountInLiters;

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        public float FuelTankCapacityInLiters
        {
            get
            {
                return m_FuelTankCapacityInLiters;
            }
        }

        public float CurrentFuelAmountInLiters
        {
            get
            {
                return m_CurrentFuelAmountInLiters;
            }
        }

        public GasolineFueledVehicle(string i_ModelName, string i_LicensePlateNumber, List<Wheel> i_Wheels, VehicleOwnerData i_OwnerData,
            Specifications i_Specifications, eFuelType i_FuelType, float i_FuelTankCapacityInLiters)
            : this(i_ModelName, i_LicensePlateNumber, i_Wheels, i_OwnerData, i_Specifications, i_FuelType, i_FuelTankCapacityInLiters, i_FuelTankCapacityInLiters)
        {

        }

        public GasolineFueledVehicle(string i_ModelName, string i_LicensePlateNumber, List<Wheel> i_Wheels, VehicleOwnerData i_OwnerData,
            Specifications i_Specifications, eFuelType i_FuelType, float i_FuelTankCapacityInLiters, float i_CurrentFuelAmountInLiters)
            : base(i_ModelName, i_LicensePlateNumber, i_Wheels, i_OwnerData, i_Specifications, 0)
        {
            if (i_FuelTankCapacityInLiters <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i_FuelTankCapacityInLiters));
            }

            m_FuelType = i_FuelType;
            m_FuelTankCapacityInLiters = i_FuelTankCapacityInLiters;
            Fuel(i_CurrentFuelAmountInLiters);
        }

        public void Fuel(float i_AddedFuelAmountInLiters)
        {
            if (i_AddedFuelAmountInLiters < 0 || i_AddedFuelAmountInLiters > m_FuelTankCapacityInLiters - m_CurrentFuelAmountInLiters)
            {
                throw new ValueOutOfRangeException(0, m_FuelTankCapacityInLiters - m_CurrentFuelAmountInLiters);
            }

            m_CurrentFuelAmountInLiters += i_AddedFuelAmountInLiters;
            CurrentEnergyPercentage = (m_CurrentFuelAmountInLiters / m_FuelTankCapacityInLiters) * 100;
        }
    }
}
