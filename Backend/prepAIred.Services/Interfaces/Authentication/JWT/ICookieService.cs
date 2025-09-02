namespace prepAIred.Services
{
    /// <summary>
    /// Service interface for managing HTTP cookies in the application.
    /// </summary>
    public interface ICookieService
    {
        /// <summary>
        /// Creates a new HTTP cookie with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the cookie.</param>
        /// <param name="value">The value to store in the cookie.</param>
        void CreateCookie(string name, string value);

        /// <summary>
        /// Deletes an existing HTTP cookie by its name.
        /// </summary>
        /// <param name="name">The name of the cookie to delete.</param>
        void DeleteCookie(string name);
    }
}
