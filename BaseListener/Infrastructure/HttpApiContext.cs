namespace BaseListener.Infrastructure
{
    public class HttpApiContext<TModel> : IHttpApiContext<TModel> where TModel : class
    {
        private HttpBaseApi<TModel> _baseApi;

        public HttpApiContext(HttpBaseApi<TModel> baseApi)
        {
            this._baseApi = baseApi;
        }

        public HttpBaseApi<TModel> Resolve => this._baseApi;
    }
}
