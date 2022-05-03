using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Vehicle
    {
        public class VehicleOwnerData
        {
            private const string k_NameEmptyExceptionMessage = "Name cannot be empty";
            private const string k_PhoneNumberExceptionMessage = "Phone number cannot be empty";
            private const string k_InvalidPhoneNumberExceptionMessage = "Invalid phone number";
            private readonly string m_Name;
            private readonly string m_PhoneNumber;

            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            public string PhoneNumber
            {
                get
                {
                    return m_PhoneNumber;
                }
            }

            public VehicleOwnerData(string i_Name, string i_PhoneNumber)
            {
                if (i_Name == null)
                {
                    throw new ArgumentNullException(nameof(i_Name));
                }

                if (i_Name == string.Empty)
                {
                    throw new ArgumentException(k_NameEmptyExceptionMessage);
                }

                if (i_PhoneNumber == null)
                {
                    throw new ArgumentNullException(nameof(i_PhoneNumber));
                }

                if (i_PhoneNumber == string.Empty)
                {
                    throw new ArgumentException(k_PhoneNumberExceptionMessage);
                }

                if (!validatePhoneNumber(i_PhoneNumber))
                {
                    throw new FormatException(k_InvalidPhoneNumberExceptionMessage);
                }

                m_Name = i_Name;
                m_PhoneNumber = i_PhoneNumber;
            }

            private bool validatePhoneNumber(string i_PhoneNumber)
            {
                bool valid = true;
                int digitCount = 0;

                foreach (char character in i_PhoneNumber)
                {
                    if (char.IsDigit(character))
                        digitCount++;
                    else if (character != '-')
                    {
                        valid = false;
                        break;
                    }
                }

                if ( valid == true && (digitCount > 10 || digitCount < 9) )
                {
                    valid = false;
                }

                return valid;
            }
        }

        public class Wheel
        {
            private const string k_ManufacturerNameEmptyExceptionMessage = "Manufacturer name cannot be empty";
            private readonly string m_ManufacturerName;
            private readonly float m_MaxTirePressure;
            private float m_CurrentTirePressure;

            public string ManufacturerName
            {
                get
                {
                    return m_ManufacturerName;
                }
            }

            public float MaxTirePressure
            {
                get
                {
                    return m_MaxTirePressure;
                }
            }

            public float CurrentTirePressure
            {
                get
                {
                    return m_CurrentTirePressure;
                }
            }

            public Wheel(string i_ManufacturerName, float i_CurrentTirePressure, float i_MaxTirePressure)
            {
                if (i_ManufacturerName == null)
                {
                    throw new ArgumentNullException(nameof(i_ManufacturerName));
                }

                if (i_ManufacturerName == string.Empty)
                {
                    throw new ArgumentException(k_ManufacturerNameEmptyExceptionMessage);
                }

                VehicleUtils.ValidateTirePressureForThisVehicle(i_CurrentTirePressure, i_MaxTirePressure);

                m_ManufacturerName = i_ManufacturerName;
                m_CurrentTirePressure = i_CurrentTirePressure;
                m_MaxTirePressure = i_MaxTirePressure;
            }

            public void InflateTire(float i_AddedPressure)
            {
                if (i_AddedPressure < 0 || i_AddedPressure > m_MaxTirePressure - m_CurrentTirePressure)
                {
                    throw new ValueOutOfRangeException(0, m_MaxTirePressure - m_CurrentTirePressure);
                }

                m_CurrentTirePressure += i_AddedPressure;
            }
        }

        public enum eRepairStatus
        {
            IN_PROGRESS,
            FINISHED,
            PAID
        }

        private const string k_ModelNameEmptyExceptionMessage = "Model name cannot be empty";
        private const string k_WheelsEmptyExceptionMessage = "Wheels cannot be empty";
        private const string k_WheelsContainsNullExceptionMessage = "Wheels array cannot contain null";
        private readonly string m_ModelName;
        private readonly string m_LicensePlateNumber;
        private readonly List<Wheel> m_Wheels;
        private readonly VehicleOwnerData m_OwnerData;
        private readonly Specifications m_Specifications;
        private float m_CurrentEnergyPercentage;
        private eRepairStatus m_RepairStatus;

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public string LicensePlateNumber
        {
            get
            {
                return m_LicensePlateNumber;
            }
        }

        public float CurrentEnergyPercentage
        {
            get
            {
                return m_CurrentEnergyPercentage;
            }

            protected set
            {
                if (value < 0 && value > 100)
                {
                    throw new ValueOutOfRangeException(0, 100);
                }
                m_CurrentEnergyPercentage = value;
            }
        }

        public VehicleOwnerData OwnerData
        {
            get
            {
                return m_OwnerData;
            }
        }

        public eRepairStatus RepairStatus
        {
            get
            {
                return m_RepairStatus;
            }

            set
            {
                m_RepairStatus = value;
            }
        }

        public Specifications Specifications
        {
            get
            {
                return m_Specifications;
            }
        }

        protected Vehicle(string i_ModelName, string i_LicensePlateNumber, List<Wheel> i_Wheels, VehicleOwnerData i_OwnerData, Specifications i_Specifications)
            : this(i_ModelName, i_LicensePlateNumber, i_Wheels, i_OwnerData, i_Specifications, 100)
        {

        }

        protected Vehicle(string i_ModelName, string i_LicensePlateNumber, List<Wheel> i_Wheels, VehicleOwnerData i_OwnerData, Specifications i_Specifications, float i_CurrentEnergyPercentage)
        {
            if (i_ModelName == null)
            {
                throw new ArgumentNullException(nameof(i_ModelName));
            }

            if (i_ModelName == string.Empty)
            {
                throw new ArgumentException(k_ModelNameEmptyExceptionMessage);
            }

            VehicleUtils.ValidateLicensePlate(i_LicensePlateNumber);

            if (i_Wheels == null)
            {
                throw new ArgumentNullException(nameof(i_Wheels));
            }

            else if (i_Wheels.Count == 0)
            {
                throw new ArgumentException(k_WheelsEmptyExceptionMessage);
            }

            else
            {
                foreach (Wheel wheel in i_Wheels)
                {
                    if (wheel == null)
                    {
                        throw new ArgumentException(k_WheelsContainsNullExceptionMessage);
                    }
                }
            }

            if (i_OwnerData == null)
            {
                throw new ArgumentNullException(nameof(i_OwnerData));
            }

            if (i_Specifications == null)
            {
                throw new ArgumentNullException(nameof(i_Specifications));
            }

            m_ModelName = i_ModelName;
            m_LicensePlateNumber = i_LicensePlateNumber;
            m_Wheels = i_Wheels;
            m_OwnerData = i_OwnerData;
            m_RepairStatus = eRepairStatus.IN_PROGRESS;
            m_Specifications = i_Specifications;
        }

        public void InflateAllTiresToMaximumPressure()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateTire(wheel.MaxTirePressure - wheel.CurrentTirePressure);
            }
        }

        public override int GetHashCode()
        {
            return m_LicensePlateNumber.GetHashCode();
        }

        public override bool Equals(object i_ToCompareWith)
        {
            bool equals = false;
            Vehicle vehicleToCompareWith = i_ToCompareWith as Vehicle;

            if (vehicleToCompareWith != null)
            {
                equals = m_LicensePlateNumber.Equals(vehicleToCompareWith.LicensePlateNumber);
            }

            return equals;
        }

        public static bool operator == (Vehicle i_Vehicle1, Vehicle i_Vehicle2)
        {
            return i_Vehicle1.Equals(i_Vehicle2);
        }

        public static bool operator != (Vehicle i_Vehicle1, Vehicle i_Vehicle2)
        {
            return !i_Vehicle1.Equals(i_Vehicle2);
        }
    }
}
