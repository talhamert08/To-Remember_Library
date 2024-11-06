using Core.CustomExceptions;
using Core.Utilities.Results;
using Core;
using Entity.Concrete;
using Newtonsoft.Json;
using System.Text;
using Web_UI.API_SERVİCE;
using Core.Entities;
using Web_UI.Models.Utilities.RequestResults;

namespace Web_UI.API_Access_Manager
{
    public class ApiManager
    {
        private readonly ApiAccessManager _apiManager;

        public ApiManager(ApiAccessManager apiManager)
        {
            _apiManager = apiManager;
        }

        public async Task<IRequestDataResult<T>> Post<T>(T article, string url) where T : class, new()
        {
            try
            {

                var jsonContent = JsonConvert.SerializeObject(article);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _apiManager._client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new ErrorRequestDataResult<T>(data: null,message: null,requestStatusCode: (int)response.StatusCode);
                }

                string responseData = await response.Content.ReadAsStringAsync();
                var res = ReDataConvert<T>.GetDataResult(responseData);

                if (res.Success)
                {
                    return new SuccessRequestDataResult<T>(data: res.Data, message: res.Message, requestStatusCode: (int)response.StatusCode);
                }
                else
                {
                    return new ErrorRequestDataResult<T>(data: res.Data, message: res.Message, requestStatusCode: (int)response.StatusCode);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }
        }
        public async Task<IRequestDataResult<List<T>>> GetList<T>(string url) where T : class, new()
        {
            try
            {
                var response = await _apiManager._client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new ErrorRequestDataResult<List<T>>(data: null, message: null, requestStatusCode: (int)response.StatusCode);
                }

                string responseData = await response.Content.ReadAsStringAsync();
                var res = ReDataConvert<List<T>>.GetDataResult(responseData);

                if (res.Success)
                {
                    return new SuccessRequestDataResult<List<T>>(data: res.Data, message: res.Message, requestStatusCode: (int)response.StatusCode);
                }
                else
                {
                    return new ErrorRequestDataResult<List<T>>(data: res.Data, message: res.Message, requestStatusCode: (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }
        }

        public async Task<IRequestDataResult<T>> GetSingle<T>(string url) where T : class, new()
        {
            try
            {
                var response = await _apiManager._client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new ErrorRequestDataResult<T>(data: null, message: null, requestStatusCode: (int)response.StatusCode);
                }

                string responseData = await response.Content.ReadAsStringAsync();
                var res = ReDataConvert<T>.GetDataResult(responseData);

                if (res.Success)
                {
                    return new SuccessRequestDataResult<T>(data: res.Data, message: res.Message, requestStatusCode: (int)response.StatusCode);
                }
                else
                {
                    return new ErrorRequestDataResult<T>(data: res.Data, message: res.Message, requestStatusCode: (int)response.StatusCode);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }
        }
    }
}
