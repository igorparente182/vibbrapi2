﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using vibbraapi.Domain.Commands;
using vibbraapi.Domain.DTO;
using vibbraapi.Domain.Entities;
using vibbraapi.Domain.Handler;
using vibbraapi.Domain.Repositories;

namespace vibbraapi.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : Controller
    {

        public readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public JsonResult Authenticate([FromBody] AuthCommand command,
            [FromServices] UserHandle handle,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations
            )
        {
            if (command is null) return new JsonResult(NotFound()) { StatusCode = 404 };
            try
            {
                var auth = (GenericCommandResult)handle.Handle(command);

                if (!auth.Success)
                {
                    return new JsonResult(BadRequest())
                    {
                        StatusCode = 400,
                        Value = new GenericCommandResult(false, auth.Message, command.Notifications)
                    };
                }
                else
                {
                    var user = (User)auth.Data;
                    var userDTO = new UserDTO(user.Id, user.Name, user.Email, user.Login, "*******");
                    return Json(new { token = JWTToken(user.Name, signingConfigurations, tokenConfigurations), user = user });
                }


            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest())
                {
                    StatusCode = 400,
                    Value = e.Message
                };
            }
        }

        [HttpGet("user_id:long")]
        [Authorize]
        public JsonResult GetById(long id)
        {
            if (id == 0) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {
                var user = _repository.GetById(id);
                if (user != null)
                {
                    var userDTO = new UserDTO(user.Id, user.Name, user.Email, user.Login, user.Password);
                    return Json(userDTO);
                }
                return Json("Not Found User");
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message) };
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Create([FromBody] CreateUserCommand command,
            [FromServices] UserHandle handle
            )
        {
            if (command is null) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {
                var resultCommand = (GenericCommandResult)handle.Handle(command);
                if (resultCommand.Success)
                {
                    var user = (User)resultCommand.Data;
                    var userDTO = new UserDTO(user.Id, user.Name, user.Email, user.Login, user.Password);
                    return Json(userDTO);
                }
                return Json(resultCommand.Message);
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "Error ", ex.Message) };
            }
        }

        [HttpPut]
        [Authorize]
        public JsonResult Update([FromBody] UpdateUserCommand command,
           [FromServices] UserHandle handle
           )
        {
            if (command is null) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {
                var resultCommand = (GenericCommandResult)handle.Handle(command);
                if (resultCommand.Success)
                {
                    var user = (User)resultCommand.Data;
                    var userDTO = new UserDTO(user.Id, user.Name, user.Email, user.Login, user.Password);
                    return Json(userDTO);
                }
                return Json(resultCommand.Message);
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "Error ", ex.Message) };
            }
        }
        private object JWTToken(string name, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            Claim[] claims = new[]
              {
                new Claim(ClaimTypes.Name, name)

            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Bearer");

            DateTime createDate = DateTime.Now;

            DateTime endDate = createDate + TimeSpan.FromHours(2);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = claimsIdentity,

                NotBefore = createDate,
                Expires = endDate
            });
            return handler.WriteToken(securityToken);

        }

    }


    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }

    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }

}
