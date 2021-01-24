using EfCoreRepository;
using Items;
using System;

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
                    _databaseConnector = new EfCoreDatabaseConnector();
                    ((EfCoreDatabaseConnector)_databaseConnector).Database.EnsureCreated();
                    ((EfCoreDatabaseConnector)_databaseConnector).SaveChanges();
                    break;
                default:
                    _databaseConnector = new EfCoreDatabaseConnector();
                    break;
            }
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
