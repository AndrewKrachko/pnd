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

    public interface IAuthenticationService
    {
        bool AuthoriseUser(string userName, string password, out string token);
        bool AuthenticateUser(string userName, string token);
    }
}
