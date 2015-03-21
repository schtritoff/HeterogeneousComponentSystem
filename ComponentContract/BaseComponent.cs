namespace ComponentContract
{
    public abstract class BaseComponent
    {
        /// <summary>
        /// Software Component Name
        /// </summary>
        public string ComponentName { get; protected set; }
        
        /// <summary>
        /// Software Component Author
        /// </summary>
        public string ComponentAuthor { get; protected set; }

        /// <summary>
        /// Software Component Description
        /// </summary>
        public string ComponentDescription { get; protected set; }
    }
}
