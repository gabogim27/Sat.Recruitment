namespace Sat.Recruitment.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Sat.Recruitment.Api.Dtos;
    using Sat.Recruitment.Api.Validators.Implementations;
    using Sat.Recruitment.Api.Validators.Interfaces;
    using Sat.Recruitment.Api.ViewModels;
    using Sat.Recruitment.Domain.Entities;
    using Sat.Recruitment.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<UsersController> _logger;

        private readonly IValidator<UserValidator, UserDto> _userValidator;

        private readonly IMapper _mapper;
        public UsersController(
            IUserService userService, 
            IValidator<UserValidator, UserDto> userValidator, 
            ILogger<UsersController> logger, 
            IMapper mapper)
        {
            _userService = userService;
            _userValidator = userValidator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<ActionResult<Result>> CreateUser(UserDto userDto)
        {
            try
            {
                var message = string.Empty;
                if (!_userValidator.IsValid(userDto, out message))
                {
                    return BadRequest(message);
                }

                var userMapped = _mapper.Map<User>(userDto);
                var success = await _userService.CreateUserAsync(userMapped);
                message = success ? "User created successfully" : "User not created";

                return Ok(new Result() 
                {
                    IsSuccess = success,
                    Errors = message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred when trying to create a new user.", ex);
                return new Result()
                {
                    IsSuccess = false,
                    Errors = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("/get-users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            try
            {
                var usersFromTxt = await _userService.ListUsersAsync();
                var usersDto = _mapper.Map<IList<User>, IList<UserDto>>(usersFromTxt);
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred when trying to create a new user.", ex);
                return new EmptyResult();
            }
        }
    }
}
