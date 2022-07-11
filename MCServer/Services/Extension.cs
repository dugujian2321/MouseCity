using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCServer
{
    public static class Extension
    {
        /// <summary>
        /// 生成[min,max)的long型整数
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long NextLong(this Random random, long min, long max)
        {
            return min + (long)Math.Floor(random.NextDouble() * (max - min));
        }
    }
}
