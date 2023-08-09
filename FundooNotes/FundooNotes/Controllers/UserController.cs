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
        private readonly ILogger<UserController> logger;

        public UserController(IUserBusiness iuserBusiness, IBus ibus, ILogger<UserController> logger)
        {
            this.iuserBusiness = iuserBusiness;
            this.ibus = ibus;
            this.logger = logger;
        }

        [HttpPost]
        //LOCALHOST CONTROLLERNAME/METHODNAME/METHODROUTE => METHOD URL
        [Route("Register")]
        public IActionResult Registration(RegistrationModel registrationModel)
        {
            try
            {
                var result = iuserBusiness.UserReg(registrationModel);
                if (result != null)
                {
                    logger.LogInformation("Registration Successful");
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Registration Successful", Data = result });
                }
                else
                {
                    logger.LogError("Registration Failed");
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Registration Failed", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = iuserBusiness.Login(loginModel);
                if (result != null)
                {
                    logger.LogInformation("Login Successful");
                    return Ok(new ResponseModel<string> { Success = true, Message = "Login Successful", Data = result });
                }
                else
                {
                    logger.LogError("Login Unsuccessful");
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Login Unsuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
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

                    logger.LogInformation("Email Sent Successfully");
                    return Ok(new ResponseModel<string> { Success = true, Message = "Email Sent Successfully", Data = email});
                }
                logger.LogError("Email Not Sent Successfully");
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Email Not Sent Successfully", Data = email });
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Reset-Password")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                string email = User.FindFirst(x => x.Type == "Email").Value;
                var result = iuserBusiness.ResetPassword(email, resetPasswordModel);
                if (result != null)
                {
                    logger.LogInformation("Password Reset Successful");
                    return Ok(new ResponseModel<ResetPasswordModel> { Success = true, Message = "Password Reset Successful", Data = result });
                }
                else
                {
                    logger.LogError("Password Reset Failed");
                    return BadRequest(new ResponseModel<ResetPasswordModel> { Success = false, Message = "Password Reset Failed", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }

        [HttpPost]
        [Route("User-Login")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            try
            {
                var result = iuserBusiness.UserLogin(loginModel);
                if (result != null)
                {
                    HttpContext.Session.SetInt32("userId", result.UserId);
                    logger.LogInformation("Login Successful");
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Login Successful", Data = result });
                }
                else
                {
                    logger.LogError("Login Unsuccessful");
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Login Unsuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Exception Occurred...");
                throw ex;
            }
        }
    }

}
