using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCServer
{
    public class StartDTO : DTOBase
    {
        /// <summary>
        /// 初始人口，人口为0时游戏结束
        /// </summary>
        public long Population { get; set; }
        /// <summary>
        /// 平均寿命
        /// </summary>
        public int DeathAge { get; set; }
        /// <summary>
        /// 男性生育年龄
        /// </summary>
        public int MaleChildBearingMinAge { get; set; }
        public int MaleChildBearingMaxAge { get; set; }
        /// <summary>
        /// 女性生育年龄
        /// </summary>
        public int FemaleChildBearingMinAge { get; set; }
        public int FemaleChildBearingMaxAge { get; set; }
        /// <summary>
        /// 分娩成功率
        /// </summary>
        public double RateOfBornSuccess { get; set; }
        /// <summary>
        /// 女性分娩死亡率
        /// </summary>
        public double DeathRateOfFemaleWhenDelivering { get; set; }
        /// <summary>
        /// 国家名称
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        ///  男性比例，剩余为女性比例，不考虑其他性别
        /// </summary>
        public double MalePercentage { get; set; }
        /// <summary>
        /// 道德
        /// </summary>
        public double Moral { get; set; }
        /// <summary>
        /// 居民健康指数
        /// </summary>
        public double Health { get; set; }
        /// <summary>
        /// 教育
        /// </summary>
        public double Education { get; set; }
        /// <summary>
        /// 饱食度
        /// </summary>
        public double FoodFull { get; set; }
        /// <summary>
        /// 工作意愿
        /// </summary>
        public double WillingToWork { get; set; }
        /// <summary>
        /// 出生率
        /// </summary>
        public double BirthRate { get; set; }
        /// <summary>
        /// 生育意愿
        /// </summary>
        public double WillingToBorn1stChild { get; set; }
        public double WillingToBorn2ndChild { get; set; }
        public double WillingToBorn3rdChild { get; set; }
        public double WillingToBorn4thOrMoreChild { get; set; }
        /// <summary>
        /// 农民占比
        /// </summary>
        public double FarmerPercentage { get; set; }
        /// <summary>
        /// 总财富
        /// </summary>
        public double TotalMoney { get; set; }

        /// <summary>
        /// 儿童比例
        /// </summary>
        public double ChildrenRate { get; set; }

        public double Age1_10Percentage { get; set; } = 0.19;
        public double Age11_20Percentage { get; set; } = 0.18;
        public double Age21_30Percentage { get; set; } = 0.17;
        public double Age31_40Percentage { get; set; } = 0.15;
        public double Age41_50Percentage { get; set; } = 0.1;
        public double Age51_60Percentage { get; set; } = 0.1;
        public double Age61_70Percentage { get; set; } = 0.05;
        public double Age71_80Percentage { get; set; } = 0.03;
        public double Age81_90Percentage { get; set; } = 0.02;
        public double Age91_100Percentage { get; set; } = 0.01;
        public double AgeGT100Percentage { get; set; } = 0;

        public double DeathRate1_10 { get; set; } = 1.4 * 0.001;
        public double DeathRate11_20 { get; set; } = 0.46 * 0.001;
        public double DeathRate21_30 { get; set; } = 1.09 * 0.001;
        public double DeathRate31_40 { get; set; } = 2.22 * 0.001;
        public double DeathRate41_50 { get; set; } = 4.32 * 0.001;
        public double DeathRate51_60 { get; set; } = 11.59 * 0.001;
        public double DeathRate61_70 { get; set; } = 27.38 * 0.001;
        public double DeathRate71_80 { get; set; } = 80.03 * 0.001;
        public double DeathRate81_90 { get; set; } = 218.35 * 0.001;
        public double DeathRate91_100 { get; set; } = 577.55 * 0.001;
        public double DeathRateGT100 { get; set; } = 950 * 0.001;

    }
}
