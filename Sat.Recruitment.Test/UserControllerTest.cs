namespace Sat.Recruitment.Test
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Sat.Recruitment.Api.Controllers;
    using Sat.Recruitment.Api.Dtos;
    using Sat.Recruitment.Api.Validators.Implementations;
    using Sat.Recruitment.Api.Validators.Interfaces;
    using Sat.Recruitment.Api.ViewModels;
    using Sat.Recruitment.Domain.Entities;
    using Sat.Recruitment.Services.Interfaces;
    using System.Threading.Tasks;
    using Xunit;

    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {
        private readonly UsersController usersController;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UsersController>> _mockLogger;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IValidator<UserValidator, UserDto>> _mockUserValidator;

        public UserControllerTest()
        {
            _mockUserValidator = new Mock<IValidator<UserValidator, UserDto>>();
            _mockLogger = new Mock<ILogger<UsersController>>();
            _mockUserService = new Mock<IUserService>();
            var mapperConfig = new MapperConfiguration(x => x.CreateMap<UserDto, User>());
            _mapper = mapperConfig.CreateMapper();
            usersController = new UsersController(_mockUserService.Object, _mockUserValidator.Object, _mockLogger.Object, _mapper);
        }

        [Fact]
        public async Task UserCreatedOkTest()
        {
            var message = string.Empty;
            _mockUserValidator.Setup(x => x.IsValid(It.IsAny<UserDto>(), out message)).Returns(true);
            _mockUserService.Setup(x => x.CreateUserAsync(It.IsAny<User>())).Returns(Task.FromResult(true));
            var userDto = new UserDto
            {
                Address = "Test Address",
                Email = "test@hotmail.com",
                Money = 500,
                Name = "Test Name",
                Phone = "12354",
                UserType = "Normal"
            };

            var result = await usersController.CreateUser(userDto);
            var res = (OkObjectResult)result.Result;
            var resultObject = (Result)res.Value;
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<ActionResult<Result>>(result);
            Assert.Equal(200, res.StatusCode);
            Assert.True(resultObject.IsSuccess);
            Assert.Equal("User created successfully", resultObject.Errors);
        }

        [Fact]
        public async Task UserCreateCheckEmailTest()
        {
            var message = "The Email is required";
            _mockUserValidator.Setup(x => x.IsValid(It.IsAny<UserDto>(), out message)).Returns(false);
            var userDto = new UserDto
            {
                Address = "Test Address",
                Email = "",
                Money = 500,
                Name = "Test Name",
                Phone = "12354",
                UserType = "Normal"
            };

            var result = await usersController.CreateUser(userDto);
            var res = (BadRequestObjectResult)result.Result;
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, res.StatusCode);
            Assert.Equal("The Email is required", res.Value);
        }

        [Fact]
        public async Task UserCreateCheckNameTest()
        {
            var message = "The Name is required";
            _mockUserValidator.Setup(x => x.IsValid(It.IsAny<UserDto>(), out message)).Returns(false);
            var userDto = new UserDto
            {
                Address = "Test Address",
                Email = "test@hotmail.com",
                Money = 500,
                Name = "",
                Phone = "12354",
                UserType = "Normal"
            };

            var result = await usersController.CreateUser(userDto);
            var res = (BadRequestObjectResult)result.Result;
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, res.StatusCode);
            Assert.Equal("The Name is required", res.Value);
        }

        [Fact]
        public async Task UserNotCreatedErrorEmailTest()
        {
            var message = string.Empty;
            _mockUserValidator.Setup(x => x.IsValid(It.IsAny<UserDto>(), out message)).Returns(true);
            _mockUserService.Setup(x => x.CreateUserAsync(It.IsAny<User>())).Throws(new System.Exception("E-mail not in the correct format."));
            var userDto = new UserDto
            {
                Address = "Test Address",
                Email = "test",
                Money = 500,
                Name = "Test Name",
                Phone = "12354",
                UserType = "Normal"
            };

            var result = await usersController.CreateUser(userDto);
            Assert.NotNull(result);
            Assert.Null(result.Result);
            Assert.Equal("E-mail not in the correct format.", result.Value.Errors);
            Assert.False(result.Value.IsSuccess);
        }

        [Fact]
        public async Task UserIsDuplicatedTest()
        {
            var message = string.Empty;
            _mockUserValidator.Setup(x => x.IsValid(It.IsAny<UserDto>(), out message)).Returns(true);
            _mockUserService.Setup(x => x.CreateUserAsync(It.IsAny<User>())).Throws(new System.Exception("User is duplicated."));
            var userDto = new UserDto
            {
                Address = "Test Address",
                Email = "test",
                Money = 500,
                Name = "Test Name",
                Phone = "12354",
                UserType = "Normal"
            };

            var result = await usersController.CreateUser(userDto);
            Assert.NotNull(result);
            Assert.Null(result.Result);
            Assert.Equal("User is duplicated.", result.Value.Errors);
            Assert.False(result.Value.IsSuccess);
        }
    }
}
