using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Repository.IRepositories;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service.Service
{
    public class AuthService : BaseService<AuditLog>, IAuthService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthService(IRepository<AuditLog> repository, IClientRepository clientRepository, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            this._clientRepository = clientRepository;
            this._refreshTokenRepository = refreshTokenRepository;
            this._unitOfWork = unitOfWork;
        }
        public Client FindClient(string clientId)
        {
            var client = _clientRepository.GetSingleById(clientId);

            return client;
        }

        public RefreshToken AddRefreshToken(RefreshToken token)
        {
            RefreshToken model = new RefreshToken();
            var existingToken = _refreshTokenRepository.GetMulti(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = RemoveRefreshToken(existingToken);
            }
            else
            {
                model = _refreshTokenRepository.Add(token);
                _unitOfWork.Commit();

            }

            return model;
        }

        public RefreshToken RemoveRefreshToken(string refreshTokenId)
        {
            RefreshToken model = new RefreshToken();
            var refreshToken = _refreshTokenRepository.GetSingleById(refreshTokenId);

            if (refreshToken != null)
            {
                model = _refreshTokenRepository.Delete(refreshToken);
                _unitOfWork.Commit();
            }
            return model;
        }

        public RefreshToken RemoveRefreshToken(RefreshToken refreshToken)
        {
            var model = _refreshTokenRepository.Delete(refreshToken);
            return model;
        }

        public RefreshToken FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = _refreshTokenRepository.GetSingleById(refreshTokenId);
            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _refreshTokenRepository.GetAll().ToList();
        }
    }
}
