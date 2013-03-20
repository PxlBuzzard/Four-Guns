#region Using Statements
using System;
using System.Reflection;
using System.Collections.Generic;
#endregion 

namespace CodeSamples
{
    /// <summary>
    /// A generic-type object pool that can create new objects if empty.
    /// </summary>
    /// <author>Daniel Jost</author>
    public class DynamicObjectPool<T>
    {
        #region Fields
        private Queue<T> objPool;
        private T masterObj;
        #endregion

        #region Constructor
        /// <summary>
        /// Create an expandable pool with a number of starting elements.
        /// </summary>
        /// <param name="startingSize">The size of the pool to start.</param>
        /// <param name="theObject">The object to be used as the master clone object.</param>
        public DynamicObjectPool(int startingSize, T theObject) 
        {
            objPool = new Queue<T>();
            masterObj = theObject;

            //fill up the pool
            for (int i = 0; i <= startingSize - 1; i++)
            {
                objPool.Enqueue(GetNewObject());
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Returns an object, either from the pool, or a new object if the pool is empty.
        /// </summary>
        /// <returns>An object of type T.</returns>
        public T Get()
        {
            return (objPool.Count > 0) ? objPool.Dequeue() : GetNewObject();
        }

        /// <summary>
        /// Adds an object into the pool.
        /// </summary>
        /// <param name="data">The object being returned to the pool.</param>
        public void Return(T data)
        {
            objPool.Enqueue(data);
        }

        /// <summary>
        /// Creates a new object if the pool is empty.
        /// </summary>
        /// <returns>A new object of the type T.</returns>
        private T GetNewObject()
        {
            //create new instance of the master clone object
            T newObj = (T)Activator.CreateInstance<T>();

            //set all properties
            var properties = masterObj.GetType().GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(newObj, property.GetValue(masterObj, null), null);
            }

            //set all fields
            var fields = masterObj.GetType().GetFields();
            foreach (var field in fields)
            {
                field.SetValue(newObj, field.GetValue(masterObj));
            }

            return newObj;
        }
        #endregion
    }
}
