using System;

namespace TileEditor
{
    /// <summary>
    /// LIFO value structure
    /// </summary>
    /// <typeparam name="T">Type of value stored</typeparam>
    /// <author>Colden Cullen</author>
    public class Stack<T> : List<T>
    {
        #region Fields
        /// <summary>
        /// Max size for the stack</summary>
        internal int maxSize;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor</summary>
        public Stack() : this( 0 ) { }

        /// <summary>
        /// Constructor for max size</summary>
        /// <param name="maxSize">
        /// Biggest stack can be</param>
        public Stack( int maxSize )
        {
            this.maxSize = maxSize;
            head = null;
        }
        #endregion

        #region Add Remove
        /// <summary>
        /// Adds an element to the stack</summary>
        /// <param name="value">
        /// The element to addX</param>
        public new void Add( T data )
        {
            Node<T> add = new Node<T>( data );

            add.Next = head;
            head = add;

            if( maxSize != 0 )
                if( count == maxSize )
                {
                    this.RemoveAt( maxSize - 1 );
                    count--;
                }


            count++;
        }

        /// <summary>
        /// Quietly removes the top element on the stack</summary>
        /// <exception>
        /// Throws an UnderflowException if the stack is empty</exception>
        public void Pop()
        {
            if( head != null )
            {
                head = head.Next;
                count--;
            }
            else throw new Exception( "Stack is empty!" );
        }

        /// <summary>
        /// Returns the top element on the stack without removing it</summary>
        /// <returns>
        /// The top element</returns>
        /// <exception>
        /// Throws an Exception if the stack is empty</exception>
        public T Peek()
        {
            if( head != null )
            {
                return head.Value;
            }
            else throw new Exception( "Stack is empty!" );
        }
        #endregion

    }
}
