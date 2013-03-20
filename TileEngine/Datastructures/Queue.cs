using System;
using System.Collections.Generic;

namespace TileEngine
{
    /// <summary>
    /// Interface between users and QueueInternal, which is the actual value structure
    /// </summary>
    /// <typeparam name="T">Type of value stored</typeparam>
    /// <author>Colden Cullen</author>
    public class Queue<T>
    {
        #region Fields
        /// <summary>
        /// QueueInternal object that acts as actual queue
        /// </summary>
        QueueInternal<T> qi;

        /// <summary>
        /// Returns the total number of elements in the queue
        /// </summary>
        public int Count
        {
            get { return qi.Count; }
        }
        #endregion

        #region Indexer, Enumerator
        /// <summary>
        /// Indexer for List</summary>
        /// <param name="index">
        /// Index of List item to get or set</param>
        /// <returns>
        /// get: Data at index set: nothing</returns>
        public T this[ int index ]
        {
            get { return qi[ index ]; }
            set { qi[ index ] = value; }
        }

        /// <summary>
        /// Enumerator for for loops</summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return qi.GetEnumerator();
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Queue constructor
        /// </summary>
        public Queue()
        {
            qi = new QueueInternal<T>();
        }
        #endregion

        #region Enqueue
        /// <summary>
        /// Adds an element to the back of the queue
        /// </summary>
        /// <param name="value">
        /// The element to be added</param>
        public void Enqueue( T data )
        {
            qi.Enqueue( data );
        }
        #endregion

        #region Dequeue
        /// <summary>
        /// Removes the front element from the queue
        /// </summary>
        /// <returns>The front element</returns>
        public virtual T Dequeue()
        {
            return qi.Dequeue();
        }
        #endregion
    }
}
