namespace Sat.Recruitment.Services.Implementations
{
    using Sat.Recruitment.Services.Interfaces;
    
    public class MoneyCalculator
    {
        private IMoneyCalculator _moneyCalculator;

        public MoneyCalculator(IMoneyCalculator moneyCalculator)
        {
            _moneyCalculator = moneyCalculator;
        }

        public void SetCalculator(IMoneyCalculator calculator) => _moneyCalculator = calculator;

        public decimal Calculate(decimal money) => _moneyCalculator.Calculatemoney(money);
    }
}
