using ResumeSmart.Api.DB.Entities;
using ResumeSmart.Api.Models;

namespace ResumeSmart.Api.Services.Interfaces;

public interface IUserService
{
    public Task CreateUser(CreateUserRequest request);
    public Task<IList<Users>> GetAllUsers();
}