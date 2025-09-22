using System.ComponentModel.DataAnnotations;

namespace prepAIred.Data
{
    /// <summary>
    /// Base abstract class for all models in the system.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        [Key]
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Converts the model to its corresponding DTO type.
        /// </summary>
        /// <typeparam name="T">The type of DTO to convert to</typeparam>
        /// <returns>A DTO representation of the model</returns>
        public virtual T ToDto<T>() where T : class
        {
            throw new NotImplementedException("ToDto must be implemented by derived classes");
        }
    }
}
