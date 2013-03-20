namespace TileEditor
{
    /// <summary>
    /// Pair of a key and value
    /// </summary>
    /// <typeparam name="TKey">Type of object used as key</typeparam>
    /// <typeparam name="TValue">Type of object stored as value</typeparam>
    /// <author>Colden Cullen</author>
    public class DictionaryPair<TKey, TValue>
    {
        #region Fields, Properties
        /// <summary>
        /// Key for value
        /// </summary>
        internal TKey key;
        /// <summary>
        /// Value stored
        /// </summary>
        internal TValue value;

        /// <summary>
        /// Key for value
        /// </summary>
        public TKey Key
        {
            get { return key; }
            set { key = value; }
        }
        /// <summary>
        /// Value stored
        /// </summary>
        public TValue Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Sets up pair
        /// </summary>
        /// <param name="key">Key for value</param>
        /// <param name="value">Value to store</param>
        public DictionaryPair( TKey key, TValue value )
        {
            this.key = key;
            this.value = value;
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// Puts value in readable form
        /// </summary>
        /// <returns>KEY: VALUE</returns>
        public override string ToString()
        {
            return key + ": " + value;
        }
        #endregion
    }
}
