using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class ElectricVehicle : Vehicle
    {
        private const string k_ElectricVehicleIsAlreadyFullyChargedExceptionMessage = "Vehicle is already fully charged";
        private readonly float m_MaxBatteryTimeInHours;
        private float m_RemainingBatteryTimeInHours;

        public float MaxBatteryTimeInHours
        {
            get
            {
                return m_MaxBatteryTimeInHours;
            }
        }

        public float RemainingBatteryTimeInHours
        {
            get
            {
                return m_RemainingBatteryTimeInHours;
            }
        }

        public ElectricVehicle(string i_ModelName, string i_LicensePlateNumber, List<Wheel> i_Wheels, VehicleOwnerData i_OwnerData, Specifications i_Specifications, float i_MaxBatteryTimeInHours)
            : this(i_ModelName, i_LicensePlateNumber, i_Wheels, i_OwnerData, i_Specifications, i_MaxBatteryTimeInHours, i_MaxBatteryTimeInHours)
        {

        }

        public ElectricVehicle(string i_ModelName, string i_LicensePlateNumber, List<Wheel> i_Wheels, VehicleOwnerData i_OwnerData, Specifications i_Specifications, float i_MaxBatteryTimeInHours, float i_RemainingBatteryTimeInHours)
            : base(i_ModelName, i_LicensePlateNumber, i_Wheels, i_OwnerData, i_Specifications, 0)
        {
            float addedBatteryInMinutes;
            if (i_MaxBatteryTimeInHours <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i_MaxBatteryTimeInHours));
            }

            addedBatteryInMinutes = i_RemainingBatteryTimeInHours * 60;
            m_MaxBatteryTimeInHours = i_MaxBatteryTimeInHours;
            Charge(addedBatteryInMinutes);
        }

        public void Charge(float i_AddedBatteryTimeInMinutes)
        {
            float addedBatteryTimeInHours = (i_AddedBatteryTimeInMinutes / 60);
            if (addedBatteryTimeInHours < 0 || addedBatteryTimeInHours > m_MaxBatteryTimeInHours - m_RemainingBatteryTimeInHours)
            {
                if (m_MaxBatteryTimeInHours - m_RemainingBatteryTimeInHours == 0)
                {
                    throw new InvalidOperationException(k_ElectricVehicleIsAlreadyFullyChargedExceptionMessage);
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxBatteryTimeInHours - m_RemainingBatteryTimeInHours);
                }
            }

            m_RemainingBatteryTimeInHours += addedBatteryTimeInHours;
            CurrentEnergyPercentage = (m_RemainingBatteryTimeInHours / m_MaxBatteryTimeInHours) * 100;
        }
    }
}
