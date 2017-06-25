using RealEstate.Entities.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.IService
{
    public interface IAuthService
    {
        Client FindClient(string clientId);
        RefreshToken AddRefreshToken(RefreshToken token);
        RefreshToken RemoveRefreshToken(string refreshTokenId);
        RefreshToken RemoveRefreshToken(RefreshToken refreshToken);
        RefreshToken FindRefreshToken(string refreshTokenId);
        List<RefreshToken> GetAllRefreshTokens();
    }
}
