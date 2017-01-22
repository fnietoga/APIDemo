using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDemo.ServiceFabric.DeviceDataWebInterface.Utils
{
    public static class RandomValues
    {
        static Random random = new Random();

        public static double GetRandomDouble(double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        } 

        public static int GetRandomInt(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
