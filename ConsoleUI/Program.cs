using System;
using Engine;

namespace UI
{
    internal class Program
    {
        public static void Main()
        {
            OpenGarage();
        }
        public static void OpenGarage()
        {
            GarageMenuForm garageMenuForm = new GarageMenuForm();
            GarageMenuExecuterUI garageMenuOptionExctuer = new GarageMenuExecuterUI();
            garageMenuOptionExctuer.Execute(garageMenuForm.DisplayAndGetResult());
        }
    }
}
