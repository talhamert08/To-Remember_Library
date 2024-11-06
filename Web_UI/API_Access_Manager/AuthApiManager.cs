using Core.CustomExceptions;
using Core;
using Entity.Concrete;
using Core.Entities.Login;
using Web_UI.API_SERVİCE;
using Core.Utilities.Results;

namespace Web_UI.API_Access_Manager
{
    public class AuthApiManager
    {
        private readonly ApiManager _manager;
        private readonly ApiAccessManager _apiManager;

        public AuthApiManager(ApiManager manager, ApiAccessManager apiAccess)
        {
            _manager = manager;
            _apiManager = apiAccess;
        }

        public async Task<IDataResult<string>> Login(UserLoginDto user)
        {
            try
            {
                var response = await _apiManager._client.PostAsJsonAsync("security/createToken", user);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadFromJsonAsync<string>();

                    return new SuccessDataResult<string>(data:responseBody);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new ErrorDataResult<string>(message: BusinessExceptionMessage.Unauthorized);
                }
                else
                {
                    return new ErrorDataResult<string>(message: BusinessExceptionMessage.General);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
