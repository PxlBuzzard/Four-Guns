using System;

namespace TileEditor
{
    /// <summary>
    /// FIFO value structure
    /// </summary>
    /// <typeparam name="T">Type of value stored</typeparam>
    /// <author>Colden Cullen</author>
    internal class QueueInternal<T> : List<T>
    {
        #region Constructor
        /// <summary>
        /// Queue constructor
        /// </summary>
        public QueueInternal()
        {
            count = 0;
            head = null;
            tail = null;
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
            Node<T> toBeAdded = new Node<T>( data );

            if( head == null )
            {
                head = toBeAdded;
                tail = toBeAdded;
            }
            else
            {
                tail.Next = toBeAdded;
                tail = toBeAdded;
            }

            count++;
        }
        #endregion

        #region Dequeue
        /// <summary>
        /// Removes the front element from the queue
        /// </summary>
        /// <returns>The front element</returns>
        public T Dequeue()
        {
            Node<T> temp;

            if( head == null )
            {
                throw new Exception( "Queue is empty!" );
            }
            else
            {
                temp = head;
                head = head.Next;
            }

            count--;

            return temp.Value;
        }
        #endregion
    }
}
