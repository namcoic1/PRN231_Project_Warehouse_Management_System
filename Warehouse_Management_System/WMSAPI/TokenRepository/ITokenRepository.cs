using BusinessObjects;
using WMSAPI.DTO;

namespace WMSAPI.TokenRepository
{
    public interface ITokenRepository
    {
        TokenDTO Authenticate(User user);
    }
}
