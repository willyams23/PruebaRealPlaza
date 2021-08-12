namespace Prueba.Service.Api.Models.ResponseBase
{
    public class ApiCreatedResponse : ApiResponse
    {
        public ApiCreatedResponse(object data)
        {
            this.Data = data;
        }

    }
}
