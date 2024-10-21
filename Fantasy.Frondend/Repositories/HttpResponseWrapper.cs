namespace Fantasy.Frondend.Repositories
{
    public class HttpResponseWrapper<T>
    {
        private T? response;
        private bool v;
        private HttpResponseMessage responseHttp;

        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }
        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessaggeAsync()
        {
            if (!Error)
            {
                return null;
            }
            var statusCode = HttpResponseMessage.StatusCode;
            if (statusCode == System.Net.HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }
            if (statusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            if (statusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return "Tienes que estar logueado para ejecutar esa operación";
            }
            if (statusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return "No tienes permisos para ejecutar esta acción";
            }
            return "Ha ocurriddo un error inesperado";
        }
    }
}