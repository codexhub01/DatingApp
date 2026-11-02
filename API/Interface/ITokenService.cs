using System;
using API.Entities;

namespace API.Interface;

public interface ITokenService
{
    string CreateToke(AppUser appUser);
}
