using System;

namespace Items
{
    public interface IRepository
    {
         bool GetUserByName(string name, out IUser user);
    }

    public interface IDatabaseConnector
    {
        public bool GetUserByName(string name, out IUser user);
    }

    public interface IUser
    {
        string Name { get; set; }
        string Password { get; set; }
    }
}
