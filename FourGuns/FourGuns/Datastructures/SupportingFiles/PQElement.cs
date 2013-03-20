using System;
using System.Collections.Generic;

namespace FourGuns
{
    /// <summary>
    /// Element that 
    /// </summary>
    /// <typeparam name="TValue">Type of object stored in element</typeparam>
    /// <typeparam name="TPriority">Type of object used to compare to other elements</typeparam>
    internal class PQElement<TValue, TPriority> : IComparable<PQElement<TValue, TPriority>>
        where TPriority : IComparable<TPriority>
    {
        #region Fields, Properties
        /// <summary>
        /// Data stored by element
        /// </summary>
        internal TValue value;
        /// <summary>
        /// Priority of element
        /// </summary>
        internal TPriority priority;

        /// <summary>
        /// Data stored by element
        /// </summary>
        public TValue Value
        {
            get { return value; }
            set { this.value = value; }
        }
        /// <summary>
        /// Priority of element
        /// </summary>
        public TPriority Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates an element with given value and priority
        /// </summary>
        /// <param name="value">Value for element</param>
        /// <param name="priority">Priority of element</param>
        public PQElement(TValue value, TPriority priority)
        {
            this.value = value;
            this.priority = priority;
        }
        #endregion

        #region Other Methdods
        /// <summary>
        /// Compares 2 elements based on priority
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>Difference</returns>
        public int CompareTo(PQElement<TValue, TPriority> other)
        {
            return this.priority.CompareTo(other.priority);
        }

        /// <summary>
        /// Returns element in printable form
        /// </summary>
        /// <returns>String with element info</returns>
        public override string ToString()
        {
            return "(" + value + "," + priority + ")";
        }
        #endregion
    }
}

//using System;
//using System.Collections.Generic;

//namespace FourGuns
//{
//    /// <summary>
//    /// Element that 
//    /// </summary>
//    /// <typeparam name="TValue">Type of object stored in element</typeparam>
//    /// <typeparam name="TPriority">Type of object used to compare to other elements</typeparam>
//    internal class PQElement<TValue, TPriority> : IComparable<PQElement<TValue, TPriority>>
//        //where TValue : IComparable<TValue>
//        where TPriority : IComparable<TPriority>
//    {
//        #region Fields, Properties
//        /// <summary>
//        /// Data stored by element
//        /// </summary>
//        internal TValue value;
//        /// <summary>
//        /// Priority of element
//        /// </summary>
//        internal TPriority priority;

//        /// <summary>
//        /// Data stored by element
//        /// </summary>
//        public TValue Value
//        {
//            get { return value; }
//            set { this.value = value; }
//        }
//        /// <summary>
//        /// Priority of element
//        /// </summary>
//        public TPriority Priority
//        {
//            get { return priority; }
//            set { priority = value; }
//        }
//        #endregion

//        #region Constructor
//        /// <summary>
//        /// Creates an element with given value and priority
//        /// </summary>
//        /// <param name="value">Value for element</param>
//        /// <param name="priority">Priority of element</param>
//        public PQElement( TValue value, TPriority priority )
//        {
//            this.value = value;
//            this.priority = priority;
//        }
//        #endregion

//        #region Other Methdods
//        /// <summary>
//        /// Compares 2 elements based on priority
//        /// </summary>
//        /// <param name="other">Object to compare to</param>
//        /// <returns>Difference</returns>
//        public int CompareTo( PQElement<TValue, TPriority> other )
//        {
//            return this.priority.CompareTo( other.priority );
//        }

//        /// <summary>
//        /// Returns element in printable form
//        /// </summary>
//        /// <returns>String with element info</returns>
//        public override string ToString()
//        {
//            return "(" + value + "," + priority + ")";
//        }
//        #endregion
//    }
//}