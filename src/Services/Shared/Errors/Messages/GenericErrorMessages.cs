namespace Shared.Errors.Messages;

public static class GenericErrorMessages<TEntity>
{
    public static ErrorMessage NotFound => new(
        $"{typeof(TEntity).Name}.NotFound",
        $"{typeof(TEntity).Name} not found");
}