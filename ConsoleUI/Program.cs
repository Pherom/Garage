using System;
using GarageLogic;

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
            GarageMenuExecutorUI garageMenuOptionExecutor = new GarageMenuExecutorUI();
            garageMenuOptionExecutor.Execute(garageMenuForm.DisplayAndGetResult());
        }
    }
}
