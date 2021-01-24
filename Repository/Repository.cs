using EfCoreRepository;
using Items;

namespace Repository
{
    public class Repository : IRepository
    {
        private IDatabaseConnector _databaseConnector;

        public Repository(DataSource dataSource = DataSource.EfCore)
        {
            switch (dataSource)
            {
                case DataSource.EfCore:
                    _databaseConnector = GetEfDatabaseConnector();
                    break;
                default:
                    _databaseConnector = GetEfDatabaseConnector();
                    break;
            }
        }

        private EfCoreDatabaseConnector GetEfDatabaseConnector()
        {
            var databaseConnector = new EfCoreDatabaseConnector();
            databaseConnector.Database.EnsureCreated();
            databaseConnector.SaveChanges();

            return databaseConnector;
        }

        public bool GetUserByName(string name, out IUser user)
        {
            if (_databaseConnector.GetUserByName(name, out user))
            {
                return true;
            }

            return false;
        }
    }
}
