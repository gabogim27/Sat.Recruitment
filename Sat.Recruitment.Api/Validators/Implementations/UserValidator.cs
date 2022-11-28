namespace Sat.Recruitment.Api.Validators.Implementations
{
    using Sat.Recruitment.Api.Dtos;
    using Sat.Recruitment.Api.Validators.Interfaces;
    using Sat.Recruitment.Domain.Enums;
    using System;
    using System.Collections.Generic;

    public class UserValidator : IValidator<UserValidator, UserDto>
    {
        public bool IsValid(UserDto user, out string message)
        {
            var errorList = new List<string>();
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                errorList.Add("The Email is required");
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                errorList.Add("The Name is required");
            }

            if (string.IsNullOrWhiteSpace(user.Address))
            {
                errorList.Add("The Address is required");
            }

            if (string.IsNullOrWhiteSpace(user.Phone))
            {
                errorList.Add("The Phone is required");
            }

            if (!Enum.IsDefined(typeof(UserType), user.UserType))
            {
                errorList.Add("The User Type is incorrect");
            }

            if (errorList.Count > 0)
            {
                message = string.Join("\r\n", errorList);
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}
