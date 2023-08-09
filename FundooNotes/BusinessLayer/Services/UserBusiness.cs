using BusinessLayer.Interface;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo iuserRepo;

        public UserBusiness(IUserRepo iuserRepo)
        {
            this.iuserRepo = iuserRepo;
        }

        public UserEntity UserReg(RegistrationModel registrationModel)
        {
            return iuserRepo.UserReg(registrationModel);
        }

        public bool CheckIfEmailExists(string email)
        {
            return iuserRepo.CheckIfEmailExists(email);
        }

        public string Login(LoginModel loginModel)
        {
            return iuserRepo.Login(loginModel);
        }

        public ForgotPasswordModel UserForgotPassword(string email)
        {
            return iuserRepo.UserForgotPassword(email);
        }

        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            return iuserRepo.ResetPassword(email, resetPasswordModel);
        }

        public UserEntity UserLogin(LoginModel loginModel)
        {
            return iuserRepo.UserLogin(loginModel);
        }
    }
}
