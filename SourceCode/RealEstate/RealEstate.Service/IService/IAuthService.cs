using RealEstate.Entities.Entites;
using RealEstate.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.IService
{
    public interface IAuthService : IBaseService<AuditLog>
    {
        Client FindClient(string clientId);
        RefreshToken AddRefreshToken(RefreshToken token);
        RefreshToken RemoveRefreshToken(string refreshTokenId);
        RefreshToken RemoveRefreshToken(RefreshToken refreshToken);
        RefreshToken FindRefreshToken(string refreshTokenId);
        List<RefreshToken> GetAllRefreshTokens();
    }
}
