using Entities;
using Entities.Models;
using Repositories.Contracts;
using Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private ISortHelper<Account> _sortHelper;

        public AccountRepository(RepositoryContext repositoryContext, ISortHelper<Account> sortHelper)
            : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList();
        }
    }
}
