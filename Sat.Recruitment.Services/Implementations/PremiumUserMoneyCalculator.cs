namespace Sat.Recruitment.Services.Implementations
{
    using Sat.Recruitment.Services.Interfaces;

    public class PremiumUserMoneyCalculator : IMoneyCalculator
    {
        public decimal Calculatemoney(decimal money)
        {
            return money > 100 ? money + (money * 2) : 0;
        }
    }
}
