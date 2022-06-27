using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceApi.Data.Repository
{
    public abstract class BaseRepository<T>
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly ILogger<T> _logger;

        public BaseRepository(AppDbContext appDbContext, ILogger<T> logger)
        {
            this._appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task SaveChangesAsycn()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
    }
}
