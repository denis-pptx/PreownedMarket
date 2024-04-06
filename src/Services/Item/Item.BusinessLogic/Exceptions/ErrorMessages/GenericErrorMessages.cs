using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item.BusinessLogic.Exceptions.ErrorMessages;

public static class GenericErrorMessages<TEntity>
{
    public static ErrorMessage NotFound => new(
        $"{typeof(TEntity).Name}.NotFound",
        $"{typeof(TEntity).Name} not found");
}
