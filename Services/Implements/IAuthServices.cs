using DataAccessLayer.RequestDTO;
using DataAccessLayer.ResponseDTO;

namespace Services.Implements
{
    public interface IAuthServices
    {
        List<AccountUpdateRes> GetAllAccounts();
        UserRes Login(LoginRequest loginRequest);
        AccountUpdateRes Register(RegisterReq req);
        void DeleteAccount(int id);
        void ResetPassword(ResetPassReq req);
    }
}
