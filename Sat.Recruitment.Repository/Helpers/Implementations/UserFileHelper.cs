namespace Sat.Recruitment.Repository.Helpers.Implementations
{
    using Microsoft.Extensions.Logging;
    using Sat.Recruitment.Domain.Configs;
    using Sat.Recruitment.Domain.Entities;
    using Sat.Recruitment.Repository.Helpers.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public class UserFileHelper : IUserFileHelper
    {
        private const string Separator = ",";

        private readonly FilePathConfig _filePath;

        private readonly ILogger<UserFileHelper> _logger;

        public UserFileHelper(FilePathConfig filePath, ILogger<UserFileHelper> logger)
        {
            _filePath = filePath;
            _logger = logger;
        }

        public async Task<bool> SaveUsers(List<User> users)
        {
            try
            {
                using (var writer = new StreamWriter(_filePath.UserFilePath))
                {
                    foreach (var user in users)
                    {
                        var usr = PrepareUserToSaveInFile(user);
                        await writer.WriteLineAsync(usr);
                    }
                }

                _logger.LogDebug("Users saved succesfully.");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} | {ex.StackTrace}");
                return false;
            }
        }

        public async Task<bool> SaveUser(User user)
        {
            try
            {
                var userToSave = PrepareUserToSaveInFile(user);
                var path = string.Concat(Directory.GetCurrentDirectory(), _filePath.UserFilePath);
                using (StreamWriter writer = File.AppendText(path))
                {
                    await writer.WriteLineAsync(Environment.NewLine + userToSave);
                }

                _logger.LogDebug($"User {user.Name}: saved succesfully.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} | {ex.StackTrace}");
                return false;
            }
        }

        public StreamReader GetUserReader()
        {
            var path = string.Concat(Directory.GetCurrentDirectory(), _filePath.UserFilePath);
            return new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.UTF8);
        }

        private string PrepareUserToSaveInFile(User user)
        {
            return string.Concat(
                                user.Name, Separator,
                                user.Email, Separator,
                                user.Phone, Separator,
                                user.Address, Separator,
                                user.UserType.ToString(), Separator,
                                user.Money.ToString());
        }
    }
}
