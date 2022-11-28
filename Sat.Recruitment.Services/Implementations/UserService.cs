namespace Sat.Recruitment.Services.Implementations
{
    using Sat.Recruitment.Domain.Entities;
    using Sat.Recruitment.Domain.Enums;
    using Sat.Recruitment.Repository.Interfaces;
    using Sat.Recruitment.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var allUsers = await _userRepository.ListUsersAsync();
            if (allUsers.Any(x => x.Name.Equals(user.Name) || x.Email.Equals(user.Email) || x.Phone.Equals(user.Phone)))
            {
                throw new Exception("User is duplicated.");
            }

            user.Email = NormalizeEmail(user.Email);
            CalculateUserMoney(user);
            return await _userRepository.SaveUserAsync(user);
        }

        private void CalculateUserMoney(User user)
        {
            var moneyCalculator = new MoneyCalculator(new NormalUserMoneyCalculator());
            switch (user.UserType)
            {
                case UserType.Normal:
                    user.Money = moneyCalculator.Calculate(user.Money);
                    break;
                case UserType.SuperUser:
                    moneyCalculator.SetCalculator(new SuperUserMoneyCalculator());
                    user.Money = moneyCalculator.Calculate(user.Money);
                    break;
                case UserType.Premium:
                    moneyCalculator.SetCalculator(new PremiumUserMoneyCalculator());
                    user.Money = moneyCalculator.Calculate(user.Money);
                    break;
                default:
                    break;
            }
        }

        public async Task<List<User>> ListUsersAsync()
        {
            return await _userRepository.ListUsersAsync();
        }

        private string NormalizeEmail(string email)
        {
            try
            {
                var emailSplitted = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                var atIndex = emailSplitted[0].IndexOf("+", StringComparison.Ordinal);
                emailSplitted[0] = atIndex < 0 ? emailSplitted[0].Replace(".", "") : emailSplitted[0].Replace(".", "").Remove(atIndex);
                return string.Join("@", new string[] { emailSplitted[0], emailSplitted[1] });
            }
            catch (Exception)
            {
                throw new Exception("E-mail not in the correct format.");
            }
            
        }
    }
}
