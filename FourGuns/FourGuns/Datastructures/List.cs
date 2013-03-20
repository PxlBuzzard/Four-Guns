using System;
using System.Collections;
using System.Collections.Generic;

namespace FourGuns
{
    /// <summary>
    /// Linked list of objects
    /// </summary>
    /// <typeparam name="T">Type of  object stored in list</typeparam>
    /// <author>Colden Cullen</author>
    /// <contributor>Sean Brennan</contributor>
    public class List<T> : IEnumerable<T>, IEnumerable
    {
        #region Fields, Properties
        /// <summary>
        /// First item in list</summary>
        internal Node<T> head;
        /// <summary>
        /// Last item in list</summary>
        internal Node<T> tail;
        /// <summary>
        /// Number of items in list</summary>
        internal int count;

        /// <summary>
        /// Number of Nodes in list</summary>
        public int Count { get { return count; } }
        #endregion

        #region Indexer
        /// <summary>
        /// Indexer for List</summary>
        /// <param name="index">
        /// Index of List item to get or set</param>
        /// <returns>
        /// get: Data at index set: nothing</returns>
        public T this[ int index ]
        {
            get
            {
                Node<T> temp = head;

                try
                {
                    for( int ii = 0; ii < index; ii++ )
                    {
                        temp = temp.Next;
                    }
                    // Return value at index
                    return temp.Value;
                }
                catch( Exception )
                {
                    // If at any point, temp == null, return default
                    return default( T );
                }
            }

            set
            {
                Node<T> temp = head;

                try
                {
                    for( int ii = 0; ii < index; ii++ )
                    {
                        temp = temp.Next;
                    }
                }
                catch( Exception )
                {
                    throw new IndexOutOfRangeException( "index is not in List" );
                }

                if( head == null )
                {
                    Add( value );
                }
                else
                {
                    temp.Value = value;
                }
            }
        }
        #endregion

        #region Enumerator
        /// <summary>
        /// Enumerator for for loops</summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> temp = head;

            for( int ii = 0; ii < count; ii++ )
            {
                yield return temp.Value;

                temp = temp.Next;
            }
        }

        /// <summary>
        /// Enumerator for for loops</summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor, sets default values</summary>
        public List()
        {
            count = 0;
            head = null;
            tail = null;
        }
        #endregion

        #region Add
        /// <summary>
        /// Adds item to end of list</summary>
        /// <param name="value">
        /// Data to addX to list</param>
        public virtual void Add( T data )
        {
            Node<T> toBeAdded = new Node<T>( data );        // Node to be put into array

            if( head == null )
            {
                //Console.WriteLine( "First Add" );
                head = toBeAdded;
                tail = head;
            }
            else if( head.Next == null )
            {
                head.Next = toBeAdded;
                tail = head.Next;
            }
            else
            {
                //Console.WriteLine( "Not First Add" );
                tail.Next = toBeAdded;
                tail = tail.Next;
            }

            count++;
        }

        /// <summary>
        /// Adds Item to front of List</summary>
        /// <param name="value">
        /// Item to addX to front of List</param>
        public virtual void AddFront( T data )
        {
            Node<T> toBeAdded = new Node<T>( data, head );
            Node<T> temp = head;

            head = toBeAdded;
            count++;
        }

        /// <summary>
        /// Inserts an item at the specified 0-based index
        /// </summary>
        /// <contributor>Zachary Behrmann</contributor>
        /// <param name="index">The index you wish to insert it at</param>
        /// <param name="value">The item to insert</param>
        public void AddAt( T data, int index )
        {
            Node<T> current = head;
            if( index == 0 )
            {
                head = new Node<T>( data, current );
                for( int i = 0; i < count; i++ )
                {
                    if( current.Next != null )
                    {
                        current = current.Next;
                    }
                }
                count++;
            }
            else if( index == count )
            {
                tail.Next = new Node<T>( data );
                count++;
            }
            else if( index > count || index < 0 )
            {
                throw new Exception( "index is outside the bounds of the list" );
            }
            else
            {
                for( int i = 0; i < index; i++ )
                {
                    current = current.Next;
                }

                Node<T> temp = current.Next;
                current.Next = new Node<T>( data, temp );
                count++;
            }
        }

        /// <summary>
        /// Add all elements from given list to this list
        /// </summary>
        /// <param name="listToAdd">List to pull data from</param>
        public void AddRange( List<T> listToAdd )
        {
            AddRange( listToAdd, 0, listToAdd.Count );
        }

        public void AddRange(IEnumerable listToAdd)
        {
            foreach (T toAdd in listToAdd)
                this.Add(toAdd);
        }

        /// <summary>
        /// Add elements from given list to this list
        /// </summary>
        /// <param name="listToAdd">List to pull data from</param>
        /// <param name="startIndex">Index in list to start at</param>
        /// <param name="endIndex">Index in list to end at</param>
        public void AddRange( List<T> listToAdd, int startIndex, int endIndex )
        {
            for( int ii = startIndex; ii < endIndex; ii++ )
                this.Add( listToAdd[ ii ] );
        }
        #endregion

        #region Remove
        /// <summary>
        /// <author>Eric Christenson</author>
        /// <contributer>Daniel Jost (made compatible with the rest of the List class)</contributer>
        /// remove first node with value
        /// </summary>
        /// <param name="value">object of type T to addX</param>
        public virtual void Remove( T data )
        {
            Node<T> current = head;
            Node<T> prev = current;

            while( current != null )// && current.Next != null)
            {
                // The following 2 lines are what I changed to make it work
                // EqualityComparer uses the default .Equals of object, comparing the hash code 
                // of the two objects which will be unique for each instance of an object
                if( EqualityComparer<T>.Default.Equals( data, current.Value ) )
                //if (current.Data.Equals(value))
                {
                    if( current == head ) // if current is head, it has no previous, need to do something different
                    {
                        if( head == tail )
                        {
                            tail = null;
                        }

                        head = head.Next;
                    }
                    else if( current == tail )
                    {
                        tail = prev;
                        prev.Next = null;
                    }
                    else
                    {
                        prev.Next = current.Next;
                    }

                    count--;
                    break;
                }
                else
                {
                    prev = current;
                    current = current.Next;
                }
            }
        }

        /// <summary>
        /// Removes value at given index</summary>
        /// <param name="index">
        /// Index to remove from</param>
        /// <returns>
        /// Successfulness of removal</returns>
        public virtual bool RemoveAt( int index )
        {
            // If index given is higher than count, return false
            if( index >= count )
                return false;

            // Get node previous to one before to be removed
            Node<T> remPrev = head;
            Node<T> rem = head;
            for( int ii = 0; ii < index - 1; ii++ )
            {
                remPrev = remPrev.Next;
                rem = rem.Next;
            }

            // Make rem go one further than remPrev
            if( index != 0 )
                rem = rem.Next;

            // Remove node
            if( rem == head )
            {
                if( head == tail )
                {
                    head = tail = null;
                    count--;
                    return true;
                }

                head = head.Next;
            }
            else if( rem == tail )
            {
                tail = remPrev;
            }
            else
            {
                remPrev.Next = remPrev.Next.Next;
            }

            count--;

            // Return true
            return true;
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// Checks to see if element is contained
        /// </summary>
        /// <param name="data">Data to search for</param>
        /// <returns>Whether or not data is contained</returns>
        public bool Contains( T data )
        {
            return IndexOf( data ) != -1;
        }

        /// <summary>
        /// Finds the index of a given data
        /// </summary>
        /// <param name="target">Data to find</param>
        /// <returns>Index of data</returns>
        /// <contributor>Sean Brennan</contributor>
        public int IndexOf( T target )
        {
            Node<T> current = head;

            for( int ii = 0; ii < count; ii++ )
            {
                if( current.Value.Equals( target ) )
                    return ii;
                else
                    current = current.Next;
            }

            // Return -1 if not found
            return -1;
        }

        /// <summary>
        /// Remove everything from list
        /// </summary>
        public void Clear()
        {
            Node<T> previous = head;    // Last node checked
            Node<T> current = head;    // Current node being checked


            if( current != null )
            {
                current = null;
                tail = null;
            }
            else if( current != null && current.Next != null )
            {
                while( current.Next != null )
                {
                    current = current.Next;
                    previous = null;
                    previous = current;
                }
            }

            head = null;
            count = 0;
        }

        /// <summary>
        /// Returns whether or not the list is empty</summary>
        /// <returns>
        /// Emptyness of list</returns>
        public Boolean IsEmpty()
        {
            return head == null;
        }

        /// <summary>
        /// Converts the list to an array of type T. @Jim Arnold
        /// </summary>
        /// <returns>The new array</returns>
        public T[] ToArray()
        {
            if (IsEmpty())
                return null;

            T[] temp = new T[this.count];
            for (int i = 0; i < this.count; i++)
            {
                temp[i] = this[i];
            }
            return temp;
        }

        /// <summary>
        /// Returns all items in List</summary>
        /// <returns>
        /// A string of all items</returns>
        public override string ToString()
        {
            if( head != null )
            {
                string returnString = this[ 0 ].ToString();

                for( int ii = 1; ii < this.Count; ii++ )
                    returnString += "\n" + this[ ii ].ToString();

                return returnString;
            }
            else return "";
        }
        #endregion
    }
}
#region oldcode
//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace FourGuns
//{
//    /// <summary>
//    /// Linked list of objects
//    /// </summary>
//    /// <typeparam name="T">Type of object stored in list</typeparam>
//    /// <author>Colden Cullen</author>
//    public class List<T> : IEnumerable<T>
//    {
//        #region Fields, Properties
//        /// <summary>
//        /// First item in list</summary>
//        internal Node<T> head;
//        /// <summary>
//        /// Last item in list</summary>
//        internal Node<T> tail;
//        /// <summary>
//        /// Number of items in list</summary>
//        internal int count;

//        /// <summary>
//        /// Number of Nodes in list</summary>
//        public int Count { get { return count; } }
//        #endregion

//        #region Indexer, Enumerator
//        /// <summary>
//        /// Indexer for List</summary>
//        /// <param name="index">
//        /// Index of List item to get or set</param>
//        /// <returns>
//        /// get: Data at index set: nothing</returns>
//        public T this[ int index ]
//        {
//            get
//            {
//                Node<T> temp = head;

//                try
//                {
//                    for( int ii = 0; ii < index; ii++ )
//                    {
//                        temp = temp.Next;
//                    }
//                    // Return value at index
//                    return temp.Value;
//                }
//                catch( Exception )
//                {
//                    // If at any point, temp == null, return default
//                    return default( T );
//                }
//            }

//            set
//            {
//                Node<T> temp = head;

//                try
//                {
//                    for( int ii = 0; ii < index; ii++ )
//                    {
//                        temp = temp.Next;
//                    }
//                }
//                catch( Exception )
//                {
//                    throw new IndexOutOfRangeException( "index is not in List" );
//                }

//                if( head == null )
//                {
//                    Add( value );
//                }
//                else
//                {
//                    temp.Value = value;
//                }
//            }
//        }

//        /// <summary>
//        /// Gets the index of the element in the list to check
//        /// </summary>
//        /// <param name="itemToCheck">The item to check where it is in the list</param>
//        /// <returns>The index of the item to check</returns>
//        /// <author>Sean Brennan</author>
//        public int IndexOf(T itemToCheck)
//        {
//            Node<T> current = head;

//            for (int i = 0; i < Count; i++)
//            {
//                if (current.Value.Equals(itemToCheck))
//                    return i;
//                else
//                    current = current.Next;
//            }

//            return -1;
//            /*for (int i = 0; i < Count; i++)
//            {
//                if (itemToCheck.Equals(this[i]))
//                {
//                    // If this is the item to be checked for, return its index
//                    return i;
//                }
//            }
//            // If the item was not found in the list, return a number greater than the size of the list
//            return Count + 1;*/
//        }

//        /// <summary>
//        /// Enumerator for for loops</summary>
//        /// <returns></returns>
//        /// <author>Sean Brennan</author>
//        public IEnumerator<T> GetEnumerator()
//        {
//            Node<T> temp = head;

//            for( int ii = 0; ii < count; ii++ )
//            {
//                yield return temp.Value;

//                temp = temp.Next;
//            }
//        }

//        /// <summary>
//        /// Implements the IEnumerable class
//        /// </summary>
//        /// <returns></returns>
//        /// <author>Sean Brennan</author>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

//        #endregion

//        #region Constructor
//        /// <summary>
//        /// Default Constructor, sets default values</summary>
//        public List()
//        {
//            count = 0;
//            head = null;
//            tail = null;
//        }
//        #endregion

//        #region Add
//        /// <summary>
//        /// Adds item to end of list</summary>
//        /// <param name="value">
//        /// Data to addX to list</param>
//        public virtual void Add( T data )
//        {
//            Node<T> toBeAdded = new Node<T>( data );        // Node to be put into array

//            if( head == null )
//            {
//                //Console.WriteLine( "First Add" );
//                head = toBeAdded;
//                tail = head;
//            }
//            else if( head.Next == null )
//            {
//                head.Next = toBeAdded;
//                tail = head.Next;
//            }
//            else
//            {
//                //Console.WriteLine( "Not First Add" );
//                tail.Next = toBeAdded;
//                tail = tail.Next;
//            }

//            count++;
//        }

//        /// <summary>
//        /// Adds Item to front of List</summary>
//        /// <param name="value">
//        /// Item to addX to front of List</param>
//        public virtual void AddFront( T data )
//        {
//            Node<T> toBeAdded = new Node<T>( data, head );
//            Node<T> temp = head;

//            head = toBeAdded;
//            count++;
//        }

//        /// <summary>
//        /// Inserts an item at the specified 0-based index
//        /// </summary>
//        /// <contributor>Zachary Behrmann</contributor>
//        /// <param name="index">The index you wish to insert it at</param>
//        /// <param name="value">The item to insert</param>
//        public void AddAt( T data, int index )
//        {
//            Node<T> current = head;
//            if( index == 0 )
//            {
//                head = new Node<T>( data, current );
//                for( int i = 0; i < count; i++ )
//                {
//                    if( current.Next != null )
//                    {
//                        current = current.Next;
//                    }
//                }
//                count++;
//            }
//            else if( index == count )
//            {
//                tail.Next = new Node<T>( data );
//                count++;
//            }
//            else if( index > count || index < 0 )
//            {
//                throw new Exception( "index is outside the bounds of the list" );
//            }
//            else
//            {
//                for( int i = 0; i < index; i++ )
//                {
//                    current = current.Next;
//                }

//                Node<T> temp = current.Next;
//                current.Next = new Node<T>( data, temp );
//                count++;
//            }
//        }

//        /// <summary>
//        /// Add another list to this one
//        /// </summary>
//        /// <param name="temp">The list to add</param>
//        /// <author>Sean Brennan</author>
//        public void AddRange(IEnumerable temp)
//        {
//            foreach (T toAdd in temp)
//            {
//                Add(toAdd);
//            }
//        }
//        #endregion

//        #region Remove
//        /// <summary>
//        /// <author>Eric Christenson</author>
//        /// <contributer>Daniel Jost (made compatible with the rest of the List class)</contributer>
//        /// remove first node with value
//        /// </summary>
//        /// <param name="value">object of type T to addX</param>
//        public virtual void Remove( T data )
//        {
//            Node<T> current = head;
//            Node<T> prev = current;

//            while( current != null )// && current.Next != null)
//            {
//                // The following 2 lines are what I changed to make it work
//                // EqualityComparer uses the default .Equals of object, comparing the hash code 
//                // of the two objects which will be unique for each instance of an object
//                if( EqualityComparer<T>.Default.Equals( data, current.Value ) )
//                //if (current.Data.Equals(value))
//                {
//                    if( current == head ) // if current is head, it has no previous, need to do something different
//                    {
//                        if( head == tail )
//                        {
//                            tail = null;
//                        }

//                        head = head.Next;
//                    }
//                    else if( current == tail )
//                    {
//                        tail = prev;
//                        prev.Next = null;
//                    }
//                    else
//                    {
//                        prev.Next = current.Next;
//                    }

//                    current = null;
//                    count--;
//                    break;
//                }
//                else
//                {
//                    prev = current;
//                    current = current.Next;
//                }
//            }
//        }

//        /// <summary>
//        /// Removes value at given index</summary>
//        /// <param name="index">
//        /// Index to remove from</param>
//        /// <returns>
//        /// Successfulness of removal</returns>
//        public virtual bool RemoveAt( int index )
//        {
//            // If index given is higher than count, return false
//            if( index >= count )
//                return false;

//            // Get node previous to one before to be removed
//            Node<T> remPrev = head;
//            Node<T> rem = head;
//            for( int ii = 0; ii < index - 1; ii++ )
//            {
//                remPrev = remPrev.Next;
//                rem = rem.Next;
//            }

//            // Make rem go one further than remPrev
//            if (index != 0)
//                rem = rem.Next;
            

//            // Remove node
//            if( rem == head )
//            {
//                if( head == tail )
//                {
//                    tail = null;
//                }

//                head = head.Next;
//            }
//            else
//            {
//                remPrev.Next = remPrev.Next.Next;
//            }

//            count--;

//            // Return true
//            return true;
//        }
//        #endregion

//        #region Other Methods
//        /// <summary>
//        /// USE AT YOUR OWN RISK
//        /// Checks whether the list contains the specified item
//        /// </summary>
//        /// <contributor>Zachary Behrmann</contributor>
//        /// <param name="value">The item to check against the list</param>
//        /// <returns>A boolean value indicating whether item is in the list</returns>
//        public bool Contains( T data )
//        {
//            Node<T> current = head;
//            while( !current.Value.Equals( data ) )
//            {
//                if( current.Next != null )
//                {
//                    current = current.Next;
//                }
//                else
//                {
//                    return false;
//                }
//            }

//            return true;
//        }

//        /// <summary>
//        /// Remove everything from list
//        /// </summary>
//        public void Clear()
//        {
//            Node<T> previous = head;    // Last node checked
//            Node<T> current = head;    // Current node being checked


//            if( current != null )
//            {
//                current = null;
//                tail = null;
//            }
//            else if( current != null && current.Next != null )
//            {
//                while( current.Next != null )
//                {
//                    current = current.Next;
//                    previous = null;
//                    previous = current;
//                }
//            }

//            head = null;
//            count = 0;
//        }

//        /// <summary>
//        /// Returns whether or not the list is empty</summary>
//        /// <returns>
//        /// Emptyness of list</returns>
//        public Boolean IsEmpty()
//        {
//            return head == null;
//        }

//        /// <summary>
//        /// Converts the list to an array of type T. @Jim Arnold
//        /// </summary>
//        /// <returns>The new array</returns>
//        public T[] ToArray()
//        {
//            if (IsEmpty())
//                return null;

//            T[] temp = new T[this.count];
//            for (int i = 0; i < this.count; i++)
//            {
//                temp[i] = this[i];
//            }
//            return temp;
//        }

//        /// <summary>
//        /// Returns all items in List</summary>
//        /// <returns>
//        /// A string of all items</returns>
//        public override string ToString()
//        {
//            if( head != null )
//            {
//                string returnString = this[ 0 ].ToString();

//                for( int ii = 1; ii < this.Count; ii++ )
//                    returnString += "\n" + this[ ii ].ToString();

//                return returnString;
//            }
//            else return "";
//        }
//        #endregion
//    }
//}

#endregion

