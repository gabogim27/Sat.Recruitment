namespace Sat.Recruitment.Repository.Implementations
{
    using Sat.Recruitment.Domain.Entities;
    using Sat.Recruitment.Domain.Enums;
    using Sat.Recruitment.Repository.Helpers.Interfaces;
    using Sat.Recruitment.Repository.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IUserFileHelper _userFileHelper;

        public UserRepository(IUserFileHelper userFileHelper)
        {
            _userFileHelper = userFileHelper;
        }

        public async Task<List<User>> ListUsersAsync()
        {
            var users = new List<User>();
            var reader = _userFileHelper.GetUserReader();
            while (reader.Peek() >= 0)
            {
                var userData = await reader.ReadLineAsync();
                if (!string.IsNullOrWhiteSpace(userData))
                {
                    var userSplitted = userData.Split(',');
                    users.Add(new User
                    {
                        Name = userSplitted[0].Trim(),
                        Email = userSplitted[1].Trim(),
                        Phone = userSplitted[2].Trim(),
                        Address = userSplitted[3].Trim(),
                        UserType = Enum.TryParse(userSplitted[4].Trim(), out UserType userTypeValue) ? userTypeValue : UserType.Undefined,
                        Money = decimal.Parse(userSplitted[5].Trim()),
                    });
                }
            }

            reader.Close();
            return users;
        }

        public async Task<bool> SaveUserAsync(User user)
        {
            return await _userFileHelper.SaveUser(user);
        }
    }
}
