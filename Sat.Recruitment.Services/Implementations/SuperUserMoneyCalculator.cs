using Sat.Recruitment.Services.Interfaces;

namespace Sat.Recruitment.Services.Implementations
{
    public class SuperUserMoneyCalculator : IMoneyCalculator
    {
        public decimal Calculatemoney(decimal money)
        {
            return money > 100 ? money + (money * 0.2m) : 0;
        }
    }
}
