using System.Linq;
using Yourslides.Data.Infrastructure;
using Yourslides.Model.Account;

namespace Yourslides.Data.Repositories {
    public interface IUserRepository : IRepository<User> {
        User GetUserByName(string userName);
    }
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(IDbFactory dbFactory)
            : base(dbFactory) {
        }

        public User GetUserByName(string userName) {
            return base.GetMany(u => u.UserName.Equals(userName)).FirstOrDefault();
        }
    }
}