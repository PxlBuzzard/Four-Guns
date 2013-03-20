using System;
using System.Collections.Generic;

namespace TileEngine
{
    /// <summary>
    /// Dictionary of values accessed by a key
    /// </summary>
    /// <typeparam name="TKey">Type of object used as key</typeparam>
    /// <typeparam name="TValue">Type of object stored as value</typeparam>
    /// <author>Colden Cullen</author>
    public class Dictionary<TKey, TValue>
    {
        #region Fields, Properties
        /// <summary>
        /// Keys to values
        /// </summary>
        internal List<DictionaryPair<TKey, TValue>>[] objects;
        /// <summary>
        /// Number of items in dictionary
        /// </summary>
        internal int count;

        /// <summary>
        /// Keys to values
        /// </summary>
        public List<TKey> Keys
        {
            get
            {
                List<TKey> returnList = new List<TKey>();
                foreach( List<DictionaryPair<TKey, TValue>> row in objects )
                    if( row != null )
                        foreach( DictionaryPair<TKey, TValue> col in row )
                            returnList.Add( col.Key );
                return returnList;
            }
        }
        /// <summary>
        /// Values stored
        /// </summary>
        public List<TValue> Values
        {
            get
            {
                List<TValue> returnList = new List<TValue>();
                foreach( List<DictionaryPair<TKey, TValue>> row in objects )
                    if( row != null )
                        foreach( DictionaryPair<TKey, TValue> col in row )
                            returnList.Add( col.Value );
                return returnList;
            }
        }
        /// <summary>
        /// Number of items in dictionary
        /// </summary>
        public int Count
        {
            get { return count; }
        }
        #endregion

        #region Indexer, Enumerator
        /// <summary>
        /// Finds the element with given key, return value
        /// </summary>
        /// <param name="key">Key associated with value to return</param>
        /// <returns>Value associated with given key</returns>
        public TValue this[ TKey key ]
        {
            get
            {
                foreach( DictionaryPair<TKey, TValue> dicPair in objects[ hash( key ) ] )
                    if( dicPair.Key.Equals( key ) )
                        return dicPair.Value;

                return default( TValue );
            }
            set
            {
                foreach( DictionaryPair<TKey,TValue> dicPair in objects[ hash( key ) ] )
                    if( dicPair.Key.Equals( key ) )
                    {
                        dicPair.Value = value;
                        return;
                    }

                Add( key, value );
            }
        }

        /// <summary>
        /// Enumerator for for loops</summary>
        /// <returns>Data in enumerable from</returns>
        public IEnumerator<DictionaryPair<TKey, TValue>> GetEnumerator()
        {
            foreach( List<DictionaryPair<TKey, TValue>> row in objects )
                foreach( DictionaryPair<TKey, TValue> col in row )
                    yield return col;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates new instance of dictionary
        /// </summary>
        public Dictionary()
        {
            objects = new List<DictionaryPair<TKey, TValue>>[ 26 ];
            count = 0;
        }
        #endregion

        #region Add
        /// <summary>
        /// Adds item to dictionary
        /// </summary>
        /// <param name="key">How to access value</param>
        /// <param name="value">Value to access</param>
        public void Add( TKey key, TValue value )
        {
            // If key is not already taken
            if( !AddContains( key ) )
            {
                int hashKey = hash( key );

                // Creates list if it doesn't exist yet
                if( objects[ hashKey ] == null )
                    objects[ hashKey ] = new List<DictionaryPair<TKey, TValue>>();

                objects[ hashKey ].Add( new DictionaryPair<TKey, TValue>( key, value ) );

                count++;
            }
            // Else, throw exception
            else throw new Exception( "Key already exists in dictionary." );
        }
        #endregion

        #region Remove
        /// <summary>
        /// Removes element from lists
        /// </summary>
        /// <param name="key">Key at which to remove index from</param>
        /// <returns>Success</returns>
        public bool Remove( TKey key )
        {
            // Hash of the key given
            int hashKey = hash( key );

            // For each index in dictionary
            for( int ii = 0; ii < objects[ hashKey ].Count; ii++ )
                // If the key is equal to given key
                if( objects[ hashKey ][ ii ].Key.Equals( key ) )
                {
                    // Remove elements from both lists, decreases count, returns true
                    objects[ hashKey ].RemoveAt( ii );
                    count--;
                    return true;
                }

            // Returns false if element is not found
            return false;
        }

        /// <summary>
        /// Removes first element found from lists
        /// </summary>
        /// <param name="value">Value to check for</param>
        /// <returns>Success</returns>
        public bool Remove( TValue value )
        {
            // For each index in dictionary
            for( int xx = 0; xx < objects.Length; xx++ )
                for( int yy = 0; yy < objects[ xx ].Count; yy++ )
                    // If the value is equal to given value
                    if( objects[ xx ][ yy ].Value.Equals( value ) )
                    {
                        // Remove elements from both lists, decreases count, returns true
                        objects[ xx ].RemoveAt( yy );
                        count--;
                        return true;
                    }

            // Returns false if element is not found
            return false;
        }

        /// <summary>
        /// Resets everything
        /// </summary>
        public void Clear()
        {
            objects = new List<DictionaryPair<TKey, TValue>>[ 26 ];
            count = 0;
        }
        #endregion

        #region Contains
        /// <summary>
        /// Whether or not key is taken
        /// </summary>
        /// <param name="key">Key to check for</param>
        /// <returns>Whether or not key exists</returns>
        public bool Contains( TKey key )
        {
            return Keys.Contains( key );
        }

        /// <summary>
        /// Whether or not key is taken
        /// </summary>
        /// <param name="key">Key to check for</param>
        /// <returns>Whether or not key exists</returns>
        public bool Contains( TValue value )
        {
            return Values.Contains( value );
        }

        /// <summary>
        /// Whether or not key is taken, meant to be used by add method
        /// </summary>
        /// <param name="key">Key to check for</param>
        /// <returns>Whether or not key exists</returns>
        internal bool AddContains( TKey key )
        {
            try
            {
                if( !this[ key ].Equals( default( TValue ) ) )
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Hash
        /// <summary>
        /// Hashes the key to an integer
        /// </summary>
        /// <param name="key">Key to hash</param>
        /// <returns>Key in integer form</returns>
        private int hash( TKey key )
        {
            // Get first letter
            char firstLetter = key.ToString()[ 0 ];

            // If firstLetter is a letter
            if( ( firstLetter >= 'A' && firstLetter <= 'Z' ) || ( firstLetter >= 'a' && firstLetter <= 'z' ) )
            {
                // Get capital version of letter
                firstLetter = ( firstLetter >= 'a' && firstLetter <= 'z' ) ? (char)( key.ToString()[ 0 ] - ( 'a' - 'A' ) ) : key.ToString()[ 0 ];

                return (char)( firstLetter - 'A' );
            }

            return ' ';
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// Whether or not dictionary is empty
        /// </summary>
        /// <returns>Whether or not dictionary is empty</returns>
        public bool IsEmpty()
        {
            return count == 0;
        }

        /// <summary>
        /// Returns dictionary as a list
        /// </summary>
        /// <returns>List form of Dictionary</returns>
        public List<DictionaryPair<TKey, TValue>> ToList()
        {
            // List to return
            List<DictionaryPair<TKey, TValue>> returnList = new List<DictionaryPair<TKey, TValue>>(); ;

            foreach( List<DictionaryPair<TKey, TValue>> row in objects )
                foreach( DictionaryPair<TKey, TValue> col in row )
                    returnList.Add( col );

            return returnList;
        }

        /// <summary>
        /// Tries to get the value associated with the specific key. @Jim Arnold
        /// </summary>
        /// <param name="key">The key associated with a value.</param>
        /// <param name="value">The value to recieve the key's associated value.</param>
        /// <returns>Returns true if an element associated with that key exists, otherwise returns false.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (Contains(key))
            {
                value = this[key];
                return true;
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Puts values in printable form
        /// </summary>
        /// <returns>Dictionary information</returns>
        public override string ToString()
        {
            // String to return
            string returnString = objects[ 0 ].ToString();

            // For each item, add to string if it exists
            for( int ii = 1; ii < count; ii++ )
            {
                try
                {
                    if( objects[ ii ][ 0 ] != null )
                        returnString += "\n" + objects[ ii ];
                }
                catch( Exception ) { }
            }

            // Return the string
            return returnString;
        }
        #endregion
    }
}