namespace prepAIred.Exceptions
{
    /// <summary>
    /// Exception thrown when an access token is invalid.
    /// </summary>
    public class InvalidAccessTokenException(string message) : Exception(message);

    /// <summary>
    /// Exception thrown when a refresh token is invalid or expired.
    /// </summary>
    public class InvalidRefreshTokenException(string message) : Exception(message);

    /// <summary>
    /// Exception thrown when authentication fails due to invalid credentials.
    /// </summary>
    public class InvalidCredentialsException(string message) : Exception(message);

    /// <summary>
    /// Exception thrown when a required resource is not found.
    /// </summary>
    public class ResourceNotFoundException(string message) : Exception(message);

    /// <summary>
    /// Exception thrown when a required field is empty.
    /// </summary>
    public class EmptyFieldsException(string message) : Exception(message);

    /// <summary>
    /// Exception thrown when a user attempts to register with an email that already exists.
    /// </summary>
    public class UserAlreadyExistsException(string message) : Exception(message);

    /// <summary>
    /// Exception thrown when the frontend is trying to access a certain endpoint but no user is currently logged in.
    /// </summary>
    public class NoUserLoggedInException(string message) : Exception(message);

    /// <summary>
    /// Exception thrown when an unsupported AI agent is specified.
    /// </summary>
    public class UnsupportedAiAgentException(string message) : Exception(message);
}
