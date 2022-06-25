using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhonkChief.BLL.Interfaces;
using PhonkChief.DAL.Data;
using PhonkChief.Domain.DTO;
using PhonkChief.Domain.Entities;
using PhonkChief.Domain.Helpers;
using PhonkChief.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhonkChief.BLL.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public AccountService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ClaimsIdentity> GetIdentity(LoginViewModel model)
        {
            try
            {
                var user = await _db.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user == null || !user.Password.Verification(model.Password))
                {
                    return null;
                }


                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.NickName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return identity;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<BaseResponse<string>> LoginAsync(LoginViewModel model)
        {
            var response = new BaseResponse<string>();
            try
            {
                var identity = await GetIdentity(model);
                if (identity == null)
                {
                    response.Message = "Неверный логин или пароль";
                    return response;
                }

                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
                    );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                response.Data = encodedJwt;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<string> { Message = $"Ошибка выполнения: {ex.Message} "};
            }
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            try
            {
               var response = await _db.Users
                    .FirstOrDefaultAsync(x => x.Email == model.Email && x.NickName == model.NickName);
                if (response != null)
                    return false;

               var user = _mapper.Map<User>(model);

               await _db.Users.AddAsync(user);
               await _db.SaveChangesAsync();
               return true;
            }
            catch
            {
                return false;
            }
        }

    

        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync()
        {
            var response = new BaseResponse<IEnumerable<User>>();

            try
            {
                var users = await _db.Users.ToListAsync();

                response.Message = "Удачный запрос на получение списка пользователей";
                response.Data = users;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>> { Message = $"Неудачный запрос: {ex.Message}" };
            }
        }

        public async Task<BaseResponse<User>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<User>();

            try
            {
                var user = await _db.Users.Include(u => u.Loops).FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    response.Message = "Пользователь не найден";
                    return response;
                }

                response.Data = user;
                response.Message = $"Найден пользователь {user.NickName}";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User> { Message = $"Ошибка запроса: {ex.Message}" };
            }
        }

        public async Task<BaseResponse<User>> GetUserByNameAsync(string name)
        {
            var response = new BaseResponse<User>();

            try
            {
                var user = await _db.Users.Include(u => u.Loops).FirstOrDefaultAsync(u => u.NickName == name);

                if (user == null)
                {
                    response.Message = $"Пользователь {name} не найден";
                    return response;
                }

                response.Message = $"Пользователь {name} найден";
                response.Data = user;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User> { Message = $"Ошибка запроса: {ex.Message}" };
            }
        }


        public async Task<UserDataDto> GetUserDataByToken(string token)
        {
            var userData = new UserDataDto();
            try
            {
                var descryptToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var claims = descryptToken.Claims.ToArray();
                userData.Name = claims[0].Value;
                userData.Role = claims[1].Value;
                return userData;
            }
            catch
            {
                return null;
            }
        }
    }
}
