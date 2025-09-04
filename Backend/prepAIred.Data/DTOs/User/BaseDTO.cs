namespace prepAIred.Data
{
    public class BaseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        /// <value>The user's database ID.</value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the date when the user account was created.
        /// </summary>
        /// <value>The timestamp of account creation.</value>
        public DateTime DateCreated { get; set; }
    }
}
