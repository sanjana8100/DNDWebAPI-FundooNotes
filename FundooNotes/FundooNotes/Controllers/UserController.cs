using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness iuserBusiness;
        private readonly IBus ibus;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBusiness iuserBusiness, IBus ibus, ILogger<UserController> _logger)
        {
            this.iuserBusiness = iuserBusiness;
            this.ibus = ibus;
            this._logger = _logger;
        }

        [HttpPost]
        //LOCALHOST CONTROLLERNAME/METHODNAME/METHODROUTE => METHOD URL
        [Route("Register")]
        public IActionResult Registration(RegistrationModel registrationModel)
        {
            var result = iuserBusiness.UserReg(registrationModel);
            if (result != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Registration Successful", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Registration Failed", Data = result });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var result = iuserBusiness.Login(loginModel);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Login Successful", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Login Failed", Data = result });
            }
        }
        
        [HttpPost("Forgot-Password")]
        public async Task<IActionResult> UserForgotPassword(string email)
        {
            try
            {
                if (iuserBusiness.CheckIfEmailExists(email))
                {
                    SendEmail sendEmail = new SendEmail();
                    ForgotPasswordModel forgotPasswordModel = iuserBusiness.UserForgotPassword(email);

                    Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                    var endPoint = await ibus.GetSendEndpoint(uri);

                    await endPoint.Send(forgotPasswordModel);
                    sendEmail.SendingEmail(forgotPasswordModel.Email, forgotPasswordModel.Token);

                    return Ok(new ResponseModel<string> { Success = true, Message = "Email Sent Successfully", Data = email});
                }
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Email Not Sent Successfully", Data = email });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Reset-Password")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            string email = User.FindFirst(x => x.Type == "Email").Value;
            var result = iuserBusiness.ResetPassword(email, resetPasswordModel);
            if (result != null)
            {
                return Ok(new ResponseModel<ResetPasswordModel> { Success = true, Message = "Password Reset Successful", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<ResetPasswordModel> { Success = false, Message = "Password Reset Failed", Data = result });
            }
        }
    }
}
