using System;
using System.Collections.Generic;

namespace FourGuns
{
    /// <summary>
    /// Queue that pulls items based on priority
    /// </summary>
    /// <typeparam name="TValue">Type of objects stored</typeparam>
    /// <typeparam name="TPriority">Type of objects used to compare</typeparam>
    /// <author>Colden Cullen</author>
    public class PriorityQueue<TValue, TPriority>
        where TPriority : IComparable<TPriority>
    {
        #region Fields, Properties
        /// <summary>
        /// List that stores value
        /// </summary>
        internal List<PQElement<TValue, TPriority>> objects;
        /// <summary>
        /// Number of items in queue
        /// </summary>
        internal int count;

        /// <summary>
        /// Number of items in queue
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        /// <summary>
        /// Values stored
        /// </summary>
        public List<TValue> Values
        {
            get
            {
                List<TValue> returnList = new List<TValue>();
                foreach (PQElement<TValue, TPriority> p in objects)
                    returnList.Add(p.Value);
                return returnList;
            }
        }
        /// <summary>
        /// Keys to values
        /// </summary>
        public List<TPriority> Priorities
        {
            get
            {
                List<TPriority> returnList = new List<TPriority>();
                foreach (PQElement<TValue, TPriority> p in objects)
                    returnList.Add(p.Priority);
                return returnList;
            }
        }
        #endregion

        #region Enumerator
        /// <summary>
        /// Makes foreach-able
        /// </summary>
        /// <returns>Enumerator object with value</returns>
        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (PQElement<TValue, TPriority> temp in objects)
                yield return temp.Value;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates new instance of priority queue
        /// </summary>
        public PriorityQueue()
        {
            objects = new List<PQElement<TValue, TPriority>>();
            count = 0;
        }
        #endregion

        #region Enqueue
        /// <summary>
        /// Adds new element to list
        /// </summary>
        /// <param name="value">Value of element</param>
        /// <param name="priority">Priority of element</param>
        public void Enqueue(TValue value, TPriority priority)
        {
            // Add object to list
            objects.Add(new PQElement<TValue, TPriority>(value, priority));

            // Increment count
            count++;
        }
        #endregion

        #region Dequeue
        /// <summary>
        /// Gets the first element in list
        /// </summary>
        /// <returns>Value of first element</returns>
        public TValue Dequeue()
        {
            // Get head value to return
            PQElement<TValue, TPriority> toReturn = objects[0];
            // Index of object to return
            int returnIndex = 0;

            for (int ii = 0; ii < count; ii++)
            {
                if (objects[ii].CompareTo(toReturn) < 0)
                {
                    toReturn = objects[ii];
                    returnIndex = ii;
                }
            }

            // Remove value
            objects.RemoveAt(returnIndex);

            // Increment count
            count--;

            // Return first head
            return toReturn.Value;
        }
        #endregion

        #region Peek
        /// <summary>
        /// Gets the first element in list
        /// </summary>
        /// <returns>Value of first element</returns>
        public TValue Peek()
        {
            // Get head value to return
            PQElement<TValue, TPriority> toReturn = objects[0];
            // Index of object to return
            int returnIndex = 0;

            for (int ii = 0; ii < count; ii++)
            {
                if (objects[ii].CompareTo(toReturn) < 0)
                {
                    toReturn = objects[ii];
                    returnIndex = ii;
                }
            }

            // Return first head
            return toReturn.Value;
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// Puts list in printable form
        /// </summary>
        /// <returns>string of values</returns>
        public override string ToString()
        {
            return objects.ToString();
        }

        /// <summary>
        /// Checks the priority queue and updates the priority of the desired item
        /// </summary>
        /// <param name="whoToUpdate">Which element to update</param>
        /// <param name="valueToChange">The desired new priority</param>
        /// <author>Sean Brennan</author>
        public void Update(TValue whoToUpdate, TPriority valueToChange)
        {
            foreach (PQElement<TValue, TPriority> itemCheck in objects)
            {
                if (itemCheck.Value.Equals(whoToUpdate))
                {
                    itemCheck.Priority = valueToChange;
                }
            }
        }

        /// <summary>
        /// Checks the PQ to see if the element is already in here
        /// </summary>
        /// <param name="isInQueue">Value to check if it is in the queue</param>
        /// <author>Sean Brennan</author>
        public bool exists(TValue isInQueue)
        {
            foreach (PQElement<TValue, TPriority> itemCheck in objects)
            {
                if (itemCheck.Value.Equals(isInQueue))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}

//using System;
//using System.Collections.Generic;

//namespace FourGuns
//{
//    /// <summary>
//    /// Queue that pulls items based on priority
//    /// </summary>
//    /// <typeparam name="TValue">Type of objects stored</typeparam>
//    /// <typeparam name="TPriority">Type of objects used to compare</typeparam>
//    /// <author>Colden Cullen</author>
//    public class PriorityQueue<TValue, TPriority>
//        //where TValue : IComparable<TValue>
//        where TPriority : IComparable< TPriority>
//    {
//        #region Fields, Properties
//        /// <summary>
//        /// List that stores value
//        /// </summary>
//        internal List<PQElement<TValue, TPriority>> objects;
//        /// <summary>
//        /// Number of items in queue
//        /// </summary>
//        internal int count;

//        /// <summary>
//        /// Number of items in queue
//        /// </summary>
//        public int Count
//        {
//            get { return count; }
//            set { count = value; }
//        }
//        /// <summary>
//        /// Values stored
//        /// </summary>
//        public List<TValue> Values
//        {
//            get
//            {
//                List<TValue> returnList = new List<TValue>();
//                foreach( PQElement<TValue, TPriority> p in objects )
//                    returnList.Add( p.Value );
//                return returnList;
//            }
//        }
//        /// <summary>
//        /// Keys to values
//        /// </summary>
//        public List<TPriority> Priorities
//        {
//            get
//            {
//                List<TPriority> returnList = new List<TPriority>();
//                foreach( PQElement<TValue, TPriority> p in objects )
//                    returnList.Add( p.Priority );
//                return returnList;
//            }
//        }
//        #endregion

//        #region Enumerator
//        /// <summary>
//        /// Makes foreach-able
//        /// </summary>
//        /// <returns>Enumerator object with value</returns>
//        public IEnumerator<TValue> GetEnumerator()
//        {
//            foreach( PQElement<TValue, TPriority> temp in objects )
//                yield return temp.Value;
//        }
//        #endregion

//        #region Constructor
//        /// <summary>
//        /// Creates new instance of priority queue
//        /// </summary>
//        public PriorityQueue()
//        {
//            objects = new List<PQElement<TValue, TPriority>>();
//            count = 0;
//        }
//        #endregion

//        #region Enqueue
//        /// <summary>
//        /// Adds new element to list
//        /// </summary>
//        /// <param name="value">Value of element</param>
//        /// <param name="priority">Priority of element</param>
//        public void Enqueue( TValue value, TPriority priority )
//        {
//            // Add object to list
//            objects.Add( new PQElement<TValue, TPriority>( value, priority ) );

//            // Increment count
//            count++;
//        }
//        #endregion

//        #region Dequeue
//        /// <summary>
//        /// Gets the first element in list
//        /// </summary>
//        /// <returns>Value of first element</returns>
//        public TValue Dequeue()
//        {
//            // Get head value to return
//            PQElement<TValue, TPriority> toReturn = objects[ 0 ];
//            // Index of object to return
//            int returnIndex = 0;

//            for( int ii = 0; ii < count; ii++ )
//            {
//                if( objects[ ii ].CompareTo( toReturn ) < 0 )
//                {
//                    toReturn = objects[ ii ];
//                    returnIndex = ii;
//                }
//            }

//            // Remove value
//            objects.RemoveAt( returnIndex );

//            // Increment count
//            count--;

//            // Return first head
//            return toReturn.Value;
//        }

//        /// <summary>
//        /// Gets the first element in list, and doesn't delete it
//        /// </summary>
//        /// <returns>Value of first element</returns>
//        /// <author>Sean Brennan</author>
//        public TValue Peek()
//        {
//            // Get head value to return
//            PQElement<TValue, TPriority> toReturn = objects[0];
//            // Index of object to return
//            int returnIndex = 0;

//            for (int ii = 0; ii < count; ii++)
//            {
//                if (objects[ii].CompareTo(toReturn) < 0)
//                {
//                    toReturn = objects[ii];
//                    returnIndex = ii;
//                }
//            }

//            // Return first head
//            return toReturn.Value;
//        }
//        #endregion

//        #region Other Methods
//        /// <summary>
//        /// Puts list in printable form
//        /// </summary>
//        /// <returns>string of values</returns>
//        public override string ToString()
//        {
//            return objects.ToString();
//        }

//        /// <summary>
//        /// Checks the priority queue and updates the priority of the desired item
//        /// </summary>
//        /// <param name="whoToUpdate">Which element to update</param>
//        /// <param name="valueToChange">The desired new priority</param>
//        /// <author>Sean Brennan</author>
//        public void Update(TValue whoToUpdate, TPriority valueToChange)
//        {
//            foreach (PQElement<TValue, TPriority> itemCheck in objects)
//            {
//                if (itemCheck.Value.Equals(whoToUpdate))
//                {
//                    itemCheck.Priority = valueToChange;
//                }
//            }
//        }

//        /// <summary>
//        /// Checks the PQ to see if the element is already in here
//        /// </summary>
//        /// <param name="isInQueue">Value to check if it is in the queue</param>
//        /// <author>Sean Brennan</author>
//        public bool exists(TValue isInQueue)
//        {
//            foreach (PQElement<TValue, TPriority> itemCheck in objects)
//            {
//                if (itemCheck.Value.Equals(isInQueue))
//                {
//                    return true;
//                }
//            }
//            return false;
//        }
//        #endregion
//    }
//}