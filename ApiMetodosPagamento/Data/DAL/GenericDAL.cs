namespace ApiMetodosPagamento.Data.DAL
{
    public class GenericDAL<T> where T : class
    {
        protected readonly ApiMetodosPagamentoContext _context;

        public GenericDAL(ApiMetodosPagamentoContext context)
        {
            this._context = context;
        }

        public List<T> GetAll()
        {
            return this._context.Set<T>().ToList();
        }

        public T? GetBy(Func<T, bool> exp)
        {
            return this._context.Set<T>().Where(exp).FirstOrDefault();
        }

        public void Add(T obj)
        {
            this._context.Set<T>().Add(obj);
            this._context.SaveChanges();
        }

        public void Update(T obj)
        {
            this._context.Set<T>().Update(obj);
            this._context.SaveChanges();
        }

        public void Delete(T obj)
        {
            this._context.Remove(obj);
            this._context.SaveChanges();
        }

    }
}
