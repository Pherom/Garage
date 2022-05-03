using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            BIKE,
            CAR,
            TRUCK
        }

        public void createListFromClassesTypes()
        {
            List<string> typeOfVehicles = new List<String>();
			string typeOfVehicle, typeOfEnergy, vehicleToBeAdded;
		  	Type typeOfObj = typeof(VehicleTypes);
		 	Type[] nestType = typeOfObj.GetNestedTypes();
            Console.WriteLine("The number of nested types is {0} \n", nestType.Length);
            foreach(Type t in nestType) {
				typeOfVehicle = t.Name.ToUpper();
				Type[] EnergyTypesOfThisVehicle = t.GetNestedTypes();
				if (EnergyTypesOfThisVehicle.Length != 0) {
					foreach(Type energySystem in EnergyTypesOfThisVehicle) {
						typeOfEnergy = energySystem.Name.ToUpper();
						//Console.WriteLine("The number of nested types is {0}.{1}", typeOfVehicle, typeOfEnergy);
						vehicleToBeAdded = typeOfVehicle + "." + typeOfEnergy;
						typeOfVehicles.Add(vehicleToBeAdded);
				 	}
				}
				else {
					vehicleToBeAdded = typeOfVehicle + "." + "FUELED";
					typeOfVehicles.Add(vehicleToBeAdded);
				}
			}

			foreach (string s in typeOfVehicles) {
				Console.WriteLine(s);
			}
        }
		
        
        //public class VehicleTypes
        //{
        //    private VehicleTypes() { }
        //    public class Motorbike
        //    {
        //        public const int k_WheelsNumber = 2;
        //        public const float k_MaxPressure = 31f;
        //        private Motorbike() { }
        //        public class Fueled
        //        {
        //            public const GasolineFueledVehicle.eFuelType k_CurrentFuelType = GasolineFueledVehicle.eFuelType.OCTAN98;
        //            public const float k_FuelTankCapacityInLiters = 6.2f;
        //            private Fueled() { }
        //        }
        //        public class Electric
        //        {
        //            public const float k_MaxBatteryTimeInHours = 2.5f;
        //            private Electric() { }
        //        }
        //    }
        //    public class Car
        //    {
        //        public const int k_WheelsNumber = 4;
        //        public const float k_MaxPressure = 29f;
        //        private Car() { }
        //        public class Fueled
        //        {
        //            public const GasolineFueledVehicle.eFuelType k_CurrentFuelType = GasolineFueledVehicle.eFuelType.OCTAN95;
        //            public const float k_FuelTankCapacityInLiters = 38f;

        //            private Fueled() { }
        //        }
        //        public class Electric
        //        {
        //            public const float k_MaxBatteryTimeInHours = 3.3f;
        //            private Electric() { }
        //        }
        //    }
        //    public class Truck
        //    {
        //        private int m_WheelsNumber = 4;
        //        private float m_MaxPressure = 29f;
        //        private GasolineFueledVehicle.eFuelType m_CurrentFuelType = GasolineFueledVehicle.eFuelType.SOLER;
        //        private float m_FuelTankCapacityInLiters = 120f;

        //        private Truck() { }
        //    }
        //}


        public Vehicle createCar()
        {
            Vehicle res = null;


            return res;
        }
    }
}
