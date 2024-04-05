namespace Identity.Application.Exceptions.ErrorMessages;

public static class RoleErrorMessages
{
    public static ErrorMessage NotFound => new(
        "Role.NotFound",
        "Role not found");

    public static ErrorMessage UpdateYourself => new(
        "User.UpdateYourself",
        "It is not possible to update the role for yourself");
}
