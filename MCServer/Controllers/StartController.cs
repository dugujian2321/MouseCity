using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MCServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        [HttpGet("Test")]
        public string CreateGame([FromQuery] StartDTO startDTO)
        {
            Dictionary<int, long> ageDistribution = DistributeAge(startDTO);
            var ageDistributionDetail = AgeDistributionDetail(ageDistribution);
            long childrenPopulation = (long)(startDTO.Population * startDTO.ChildrenRate);
            long adults = startDTO.Population - childrenPopulation;
            long maleAdult = (long)(adults * startDTO.MalePercentage);
            long femaleAdult = adults - maleAdult;
            long totalPopulation = startDTO.Population;
            long year = 0;
            string result = string.Empty;
            double averageAge = 0;
            long lastYearPopulation;
            //每次循环代表1年
            while (totalPopulation > 0 && year <= 10000)
            {
                lastYearPopulation = totalPopulation;
                Dictionary<int, long> deathAmount = CalculateDeathDistribution(ageDistributionDetail, startDTO);
                double totalAge = 0;

                for (int i = 0; i < ageDistributionDetail.Count; i++)
                {
                    int age = i + 1;
                    totalAge += age * deathAmount[age];
                }
                if (totalAge != 0)
                    averageAge = (averageAge + totalAge / deathAmount.Sum(x => x.Value)) / 2;

                IncreaseAge(ageDistributionDetail);
                long newBorn = (long)(totalPopulation * startDTO.BirthRate);
                ageDistributionDetail[1] = newBorn;
                year++;
                try
                {
                    totalPopulation = ageDistributionDetail.Sum(x => x.Value);
                    result += $@"{year}年后，鼠鼠总数为{totalPopulation}";
                    result += $@"；上一年度新生{newBorn}（{Math.Round(newBorn * 100.0 / lastYearPopulation, 4)}%）";
                    var deathAmountValue = deathAmount.Sum(x => x.Value);
                    result += $@"；上一年度死亡{deathAmountValue}（{Math.Round(deathAmountValue * 100.0 / lastYearPopulation, 4)}%）";
                    result += $@"；平均寿命为{averageAge}";
                    result += Environment.NewLine;
                }
                catch
                {
                    break;
                }
            }

            return result;
        }

        private double AmountToNextAge(double population)
        {
            double average = population / 10.0;
            int eachAge = (int)(Math.Ceiling(average) - 1);
            int leftAmount = (int)(population - eachAge * 9);
            return population > eachAge + leftAmount ? eachAge + leftAmount : population;
        }

        private void IncreaseAge(IDictionary<int, long> ageDistributionDetail)
        {
            for (int i = ageDistributionDetail.Count - 1; i > 0; i--)
            {
                int age = i + 1;
                if (age == 101)
                    ageDistributionDetail[age] += ageDistributionDetail[age - 1];
                else
                    ageDistributionDetail[age] = ageDistributionDetail[age - 1];
            }
        }
        private Dictionary<int, long> CalculateDeathDistribution(Dictionary<int, long> ageDistributionDetail, StartDTO startDTO)
        {
            Dictionary<int, long> deathAmounts = new Dictionary<int, long>();
            for (int age = 1; age <= ageDistributionDetail.Count; age++)
            {
                deathAmounts.Add(age, 0);
            }
            List<double> deathRate = new List<double>();
            deathRate.Add(startDTO.DeathRate1_10);
            deathRate.Add(startDTO.DeathRate11_20);
            deathRate.Add(startDTO.DeathRate21_30);
            deathRate.Add(startDTO.DeathRate31_40);
            deathRate.Add(startDTO.DeathRate41_50);
            deathRate.Add(startDTO.DeathRate51_60);
            deathRate.Add(startDTO.DeathRate61_70);
            deathRate.Add(startDTO.DeathRate71_80);
            deathRate.Add(startDTO.DeathRate81_90);
            deathRate.Add(startDTO.DeathRate91_100);
            deathRate.Add(startDTO.DeathRateGT100);

            int rateIndex = 0;
            for (int age = 1; age <= ageDistributionDetail.Count; age++)
            {
                long deathAmount = (long)(ageDistributionDetail[age] * deathRate[rateIndex]) > 1 ? (long)(ageDistributionDetail[age] * deathRate[rateIndex]) : LiveOrDie(deathRate[rateIndex]);

                if (ageDistributionDetail[age] >= deathAmount)
                {
                    ageDistributionDetail[age] -= deathAmount;
                    deathAmounts[age] = deathAmount;
                }

                if (age % 10 == 0 && age <= 100)
                    rateIndex++;

                if (age > 100)
                    rateIndex = deathRate.Count - 1;
            }

            return deathAmounts;
        }

        private int LiveOrDie(double deathRate)
        {
            double liveRangeUpper = deathRate * 100000;
            Random rnd = new Random();
            int i = rnd.Next(1, 100001);
            return i <= liveRangeUpper ? 1 : 0;
        }

        private Dictionary<int, long> DistributeAge(StartDTO startDTO)
        {
            Dictionary<int, long> ageDistribution = new Dictionary<int, long>();

            ageDistribution.Add(1, 0);
            ageDistribution.Add(2, 0);
            ageDistribution.Add(3, 0);
            ageDistribution.Add(4, 0);
            ageDistribution.Add(5, 0);
            ageDistribution.Add(6, 0);
            ageDistribution.Add(7, 0);
            ageDistribution.Add(8, 0);
            ageDistribution.Add(9, 0);
            ageDistribution.Add(10, 0);
            ageDistribution.Add(11, 0);

            Func<long> getCurrPopulation = () => (long)(ageDistribution.Sum(x => x.Value));
            do
            {
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[1] = (long)(startDTO.Population * startDTO.Age1_10Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[2] = (long)(startDTO.Population * startDTO.Age11_20Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[3] = (long)(startDTO.Population * startDTO.Age21_30Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[4] = (long)(startDTO.Population * startDTO.Age31_40Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[5] = (long)(startDTO.Population * startDTO.Age41_50Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[6] = (long)(startDTO.Population * startDTO.Age51_60Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[7] = (long)(startDTO.Population * startDTO.Age61_70Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[8] = (long)(startDTO.Population * startDTO.Age71_80Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[9] = (long)(startDTO.Population * startDTO.Age81_90Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[10] = (long)(startDTO.Population * startDTO.Age91_100Percentage);
                if (getCurrPopulation() >= startDTO.Population) break;
                ageDistribution[11] = (long)(startDTO.Population * startDTO.AgeGT100Percentage);
            } while (1 < 0);
            long unassignedAmount = startDTO.Population - getCurrPopulation();
            if (unassignedAmount > 0)
            {
                int i = 0;
                while (i < ageDistribution.Count)
                {
                    int key = i + 1;
                    if (unassignedAmount % (ageDistribution.Count - i) == 0)
                    {
                        var keys = GenerateDistinctInt(1, 12, ageDistribution.Count - i);
                        int avg = (int)(unassignedAmount / (ageDistribution.Count - i));
                        foreach (var k in keys)
                        {
                            ageDistribution[k] += avg;
                        }
                        break;
                    }
                    i++;
                }

            }

            return ageDistribution;
        }


        private Dictionary<int, long> AgeDistributionDetail(Dictionary<int, long> ageDistribution)
        {
            Dictionary<int, long> result = new Dictionary<int, long>();
            int ageDistributionKey = 1;
            long amountOfCurrGroup = ageDistribution[1];
            Random rnd = new Random();
            bool addAll = false;
            for (int age = 0; age < 140; age++)
            {
                if (age > 99)
                    ageDistributionKey = ageDistribution.Count;
                int resultKey = age > 99 ? 101 : age + 1;
                if (!result.Keys.Contains(resultKey))
                    result.Add(resultKey, 0);

                if (addAll)
                {
                    result[resultKey] = amountOfCurrGroup;
                    amountOfCurrGroup = 0;
                    addAll = false;
                }
                else
                {
                    if (amountOfCurrGroup > 0)
                    {
                        long amountOfCurrAge = rnd.NextLong(1, amountOfCurrGroup + 1);
                        amountOfCurrGroup -= amountOfCurrAge;
                        result[resultKey] = amountOfCurrAge;
                    }
                }

                if (resultKey % 10 == 0 && ageDistributionKey < 11)
                {
                    ageDistributionKey++;
                    amountOfCurrGroup = ageDistribution[ageDistributionKey];
                }

                if ((resultKey + 1) % 10 == 0)
                    addAll = true;
            }

            return result;
        }
        /// <summary>
        /// 生成∈[min,max)的整数
        /// </summary>
        /// <param name="max">exclusive</param>
        /// <param name="min">inclusive</param>
        /// <param name="number"></param>
        /// <returns></returns>
        private List<int> GenerateDistinctInt(int min, int max, int number)
        {
            List<int> result = new List<int>();
            Random random = new Random();
            for (int i = 0; i < number; i++)
            {
                int temp = random.Next(min, max);
                while (result.Contains(temp))
                {
                    temp = random.Next(min, max);
                }
                result.Add(temp);
            }
            return result;
        }
    }
}
