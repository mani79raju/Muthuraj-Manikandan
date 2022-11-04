namespace StockApp.Portal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; }

        public IRoleRepository Role { get; }

        public IStockRepository Stocks { get; }

        public UnitOfWork(IUserRepository user, IRoleRepository role, IStockRepository stock)
        {
            User = user;
            Role = role; 
            Stocks = stock;
        }
    }
}
