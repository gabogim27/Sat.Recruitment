using Sat.Recruitment.Services.Interfaces;

namespace Sat.Recruitment.Services.Implementations
{
    public class NormalUserMoneyCalculator : IMoneyCalculator
    {
        public decimal Calculatemoney(decimal money)
        {
            if (money > 100)
            {
                return money + (money * 0.12m);
            }
            
            if (money > 10 && money < 100)
            {
                return money + (money * 0.8m);
            }

            return money;
        }
    }
}
