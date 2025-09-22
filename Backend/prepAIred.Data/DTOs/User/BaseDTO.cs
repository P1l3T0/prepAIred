namespace prepAIred.Data
{
    /// <summary>
    /// Base abstract class for all dtos in the system.
    /// </summary>
    public abstract class BaseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the date when the user account was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}
