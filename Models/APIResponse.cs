using System.Net;

namespace apiSqlserver.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessage = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessage { get; set; }
        public object Resultado { get; set; }

        public string Alertmsg { get; set; }

    }
}
