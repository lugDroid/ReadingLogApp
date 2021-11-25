using ReadingLog.Core;

namespace ReadingLog.Data
{
    public interface IUserRepository
    {
        User GetByUsernameAndPassword(string username, string password);
    }
}