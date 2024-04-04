using Item.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IStatusService 
{
    Task<IEnumerable<Status>> GetAsync(CancellationToken token = default);
    Task<Status> GetByIdAsync(Guid id, CancellationToken token = default);
}
