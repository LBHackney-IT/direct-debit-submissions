namespace BaseListener.Infrastructure
{
    public interface IHttpApiContext<TModel> where TModel : class
    {
        public HttpBaseApi<TModel> Resolve { get; }
    }
}
