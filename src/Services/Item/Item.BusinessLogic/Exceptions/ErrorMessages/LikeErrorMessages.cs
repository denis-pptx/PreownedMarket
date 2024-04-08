namespace Item.BusinessLogic.Exceptions.ErrorMessages;

public static class LikeErrorMessages
{
    public static ErrorMessage AlreadyLiked => new(
        "Like.AlreadyLiked",
        "The item has already been liked by this user.");

    public static ErrorMessage NotLiked => new(
        "Like.NotLiked",
        "The item is not liked by this user.");

    public static ErrorMessage LikeYourItem => new(
        "Like.LikeYourItem",
        "It is impossible to like your item.");
}
