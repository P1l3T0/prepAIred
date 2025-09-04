using prepAIred.Data;

namespace prepAIred.Services
{
    /// <summary>
    /// Service interface for managing user-related business operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="user">The user entity to create.</param>
        /// <returns>The created user entity.</returns>
        Task<User> CreateUserAsync(User user);

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>The matching user entity if found.</returns>
        Task<CurrentUserDTO> GetUserByEmailAsync(string email);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="name">The username to search for.</param>
        /// <returns>The matching user entity if found.</returns>
        Task<CurrentUserDTO> GetUserByUsernameAsync(string name);

        /// <summary>
        /// Retrieves a user dto by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The matching user dto if found.</returns>
        Task<CurrentUserDTO> GetUserByIdAsync(int userId);

        /// <summary>
        /// Retrieves a user entity by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The matching user entity if found.</returns>
        Task<User> GetUserEntityByIdAsync(int userId);

        /// <summary>
        /// Retrieves the currently authenticated user.
        /// </summary>
        /// <returns>The current user entity.</returns>
        Task<CurrentUserDTO> GetCurrentUserAsync();

        /// <summary>
        /// Validates user registration data.
        /// </summary>
        /// <param name="registerDto">The registration data to validate.</param>
        /// <returns>A task representing the validation operation.</returns>
        Task ValidateUserAsync(RegisterDTO registerDto);

        /// <summary>
        /// Checks if a user with the specified email exists.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        Task<bool> UserExistsAsync(string email);

        /// <summary>
        /// Checks if required registration fields are empty.
        /// </summary>
        /// <param name="registerDto">The registration data to check.</param>
        /// <returns>True if any required fields are empty, false otherwise.</returns>
        bool AreFieldsEmpty(RegisterDTO registerDto);

        /// <summary>
        /// Validates the format of email and password.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if both email and password are valid, false otherwise.</returns>
        bool ValidateEmailAndPassword(string email, string password);

        /// <summary>
        /// Hashes a user's password during registration.
        /// </summary>
        /// <param name="registerDto">The registration data containing the password to hash.</param>
        /// <returns>A tuple containing the hashed password and salt.</returns>
        (byte[] hashedPassword, byte[] saltPassword) HashPassword(RegisterDTO registerDto);

        /// <summary>
        /// Verifies a user's password during login.
        /// </summary>
        /// <param name="currentUser">The user entity to check against.</param>
        /// <param name="loginDto">The login credentials to verify.</param>
        /// <returns>True if the password is correct, false otherwise.</returns>
        bool CheckPassword(CurrentUserDTO currentUser, LoginDTO loginDto);
    }
}
