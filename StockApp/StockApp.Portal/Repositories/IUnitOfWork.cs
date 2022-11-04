namespace StockApp.Portal.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository User { get; }
        public IRoleRepository Role { get; }
        public IStockRepository Stocks { get; }
    }
}
