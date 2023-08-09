using CommonLayer.Models;
using CommonLayer.RequestModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public UserRepo(FundooDBContext fundooDBContext, IConfiguration configuration)
        {
            this.fundooDBContext = fundooDBContext;
            this.configuration = configuration;
        }

        public UserEntity UserReg(RegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();

                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;
                userEntity.Email = registrationModel.Email;
                userEntity.Password = EncodePasswordToBase64(registrationModel.Password);
                userEntity.CreatedAt = DateTime.Now;
                userEntity.UpdatedAt = DateTime.Now;

                if (!CheckIfEmailExists(registrationModel.Email))
                {
                    fundooDBContext.Users.Add(userEntity);
                    fundooDBContext.SaveChanges();

                    return userEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckIfEmailExists(string email)
        {
            bool EmailExists = fundooDBContext.Users.Any(x => x.Email == email);
            if (EmailExists)
            {
                return true;
            }
            return false;
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string Login(LoginModel loginModel)
        {
            try
            {
                string EncodedPassword = EncodePasswordToBase64(loginModel.Password);
                var loginUser = fundooDBContext.Users.FirstOrDefault(x => x.Email == loginModel.Email && x.Password == EncodedPassword);
                if (loginUser != null)
                {
                    var token = GenerateToken(loginUser.Email, loginUser.UserId);
                    return token;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateToken(string Email, int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public ForgotPasswordModel UserForgotPassword(string email)
        {
            try
            {
                var result = fundooDBContext.Users.Where(x => x.Email == email).FirstOrDefault();

                ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
                forgotPasswordModel.Email = result.Email;
                forgotPasswordModel.Token = GenerateToken(result.Email, result.UserId);
                forgotPasswordModel.UserID = result.UserId;

                return forgotPasswordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (resetPasswordModel.ConfirmPassword.Equals(resetPasswordModel.Password) && CheckIfEmailExists(email))
                {
                    var result = fundooDBContext.Users.Where(x => x.Email == email).FirstOrDefault();
                    result.Password = EncodePasswordToBase64(resetPasswordModel.ConfirmPassword);
                    fundooDBContext.SaveChanges();
                    return resetPasswordModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserEntity UserLogin(LoginModel loginModel)
        {
            try
            {
                string EncodedPassword = EncodePasswordToBase64(loginModel.Password);
                UserEntity loginUser = fundooDBContext.Users.FirstOrDefault(x => x.Email == loginModel.Email && x.Password == EncodedPassword);
                if (loginUser != null)
                {
                    return loginUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

