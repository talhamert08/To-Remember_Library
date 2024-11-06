using Core.Utilities.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ResultDataSet<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

    }

    public static class ReDataConvert<T> where T : class, new()
    {
        public static IDataResult<T> GetDataResult(string resultSet)
        {
            ResultDataSet<T> resultDataSet = JsonConvert.DeserializeObject<ResultDataSet<T>>(resultSet);

            if (resultDataSet.Success == true)
            {
                return new SuccessDataResult<T>(data: resultDataSet.Data, message: resultDataSet.Message);
            }
            else
            {
                return new ErrorDataResult<T>(data: resultDataSet.Data, message: resultDataSet.Message);
            }

        }


    }

}
