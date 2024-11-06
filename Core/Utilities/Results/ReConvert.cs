using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ResultSet
    {
        public bool Success { get; set; }
        public string Message { get; set; }

    }

    public static class ReConvert
    {

        public static IResult GetResult(string resultSet)
        {
            ResultSet result = JsonConvert.DeserializeObject<ResultSet>(resultSet);


            if (result.Success == true)
            {
                return new SuccessResult(message: result.Message);
            }
            else
            {
                return new ErrorResult(message: result.Message);
            }

        }
    }
}
