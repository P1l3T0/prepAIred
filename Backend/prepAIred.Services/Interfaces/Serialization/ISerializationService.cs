namespace prepAIred.Services
{
    public interface ISerializationService
    {
        /// <summary>
        /// Serializes any object to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <returns>A JSON string representation of the object</returns>
        string Serialize<T>(T obj) where T : class;

        /// <summary>
        /// Serializes a collection of objects to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of objects in the collection</typeparam>
        /// <param name="objects">The collection to serialize</param>
        /// <returns>A JSON string representation of the collection</returns>
        string SerializeCollection<T>(IEnumerable<T> objects) where T : class;

        /// <summary>
        /// Deserializes a JSON string to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="json">The JSON string to deserialize</param>
        /// <returns>The deserialized object</returns>
        T? Deserialize<T>(string json) where T : class;

        /// <summary>
        /// Deserializes a JSON string to a collection of objects of type T.
        /// </summary>
        /// <typeparam name="T">The type of objects in the collection</typeparam>
        /// <param name="json">The JSON string to deserialize</param>
        /// <returns>The deserialized collection</returns>
        ICollection<T>? DeserializeCollection<T>(string json) where T : class;
    }
}
