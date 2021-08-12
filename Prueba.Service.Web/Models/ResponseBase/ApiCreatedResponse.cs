namespace Prueba.Service.Web.Models.ResponseBase
{
    public class ApiCreatedResponse : ApiResponse
    {
        public ApiCreatedResponse(object data)
        {
            this.Data = data;
        }

    }
}
