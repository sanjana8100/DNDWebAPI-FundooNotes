using CommonLayer.Models;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity UserReg(RegistrationModel registrationModel);
        public bool CheckIfEmailExists(string email);
        public string Login(LoginModel loginModel);
        public ForgotPasswordModel UserForgotPassword(string email);
        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel);
        public UserEntity UserLogin(LoginModel loginModel);
    }
}