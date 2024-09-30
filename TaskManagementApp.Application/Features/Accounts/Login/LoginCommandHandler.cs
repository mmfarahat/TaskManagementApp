using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Domain.Entities;
using AppTask = TaskManagementApp.Domain.Entities.AppTask;

namespace TaskManagementApp.Application.Features.Accounts.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        IMapper _mapper;
        IUserService _userService;
        public LoginCommandHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }


        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new LoginCommandResponse();
            var validationResult = await new LoginCommandValidator().ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }
            else
            {
                var tokenResponse = await _userService.LogUserIn(request.Email, request.Password);
                if (tokenResponse != null)
                {
                    response.Success = true;
                    response.TokenResponse.Token = tokenResponse.Token;
                    response.TokenResponse.Expiration = tokenResponse.Expiration;
                }
                else
                {
                    response.Success = false;
                    response.ValidationErrors.Add("Invalid login credentials");
                }
            }
            return response;
        }
    }
}
