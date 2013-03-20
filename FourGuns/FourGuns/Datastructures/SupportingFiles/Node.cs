namespace FourGuns
{
    /// <summary>
    /// Node for linked ADTs
    /// </summary>
    /// <typeparam name="T">Type of value stored</typeparam>
    /// <author>Colden Cullen</author>
    internal class Node<T>
    {
        #region Fields, Properties
        /// <summary>
        /// The node's value</summary>
        internal T value;
        /// <summary>
        /// A reference to the next node</summary>
        internal Node<T> next;

        /// <summary>
        /// Value stored at current Node</summary>
        public T Value { get { return value; } set { this.value = value; } }
        /// <summary>
        /// Next node in list</summary>
        public Node<T> Next { get { return next; } set { next = value; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor</summary>
        public Node() { }

        /// <summary>
        /// Constructs a node with a value element, and null for the next node</summary>
        /// <param name="value">
        /// The value element</param>
        /// <param name="index">
        /// Index of element added</param>
        public Node(T data) : this(data, null) { }

        /// <summary>
        /// Constructs a node with a value element and a next node</summary>
        /// <param name="value">
        /// The value element</param>
        /// <param name="index">
        /// Index of element added</param>
        /// <param name="next">
        /// The next node</param>
        public Node(T data, Node<T> next)
        {
            this.value = data;
            this.next = next;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returnts current node's value and all nodes after it</summary>
        /// <returns>
        /// Current node's value and all nodes after it</returns>
        public override string ToString()
        {
            return value.ToString();
        }
        #endregion
    }
}

//namespace FourGuns
//{
//    /// <summary>
//    /// Node for linked ADTs
//    /// </summary>
//    /// <typeparam name="T">Type of value stored</typeparam>
//    /// <author>Colden Cullen</author>
//    internal class Node<T>
//    {
//        #region Fields, Properties
//        /// <summary>
//        /// The node's value</summary>
//        internal T value;
//        /// <summary>
//        /// A reference to the next node</summary>
//        internal Node<T> next;

//        /// <summary>
//        /// Value stored at current Node</summary>
//        public T Value { get { return value; } set { this.value = value; } }
//        /// <summary>
//        /// Next node in list</summary>
//        public Node<T> Next { get { return next; } set { next = value; } }
//        #endregion

//        #region Constructors
//        /// <summary>
//        /// Default constructor</summary>
//        public Node() { }

//        /// <summary>
//        /// Constructs a node with a value element, and null for the next node</summary>
//        /// <param name="value">
//        /// The value element</param>
//        /// <param name="index">
//        /// Index of element added</param>
//        public Node( T data ) : this( data, null ) { }

//        /// <summary>
//        /// Constructs a node with a value element and a next node</summary>
//        /// <param name="value">
//        /// The value element</param>
//        /// <param name="index">
//        /// Index of element added</param>
//        /// <param name="next">
//        /// The next node</param>
//        public Node( T data, Node<T> next )
//        {
//            this.value = data;
//            this.next = next;
//        }
//        #endregion

//        #region Methods
//        /// <summary>
//        /// Returnts current node's value and all nodes after it</summary>
//        /// <returns>
//        /// Current node's value and all nodes after it</returns>
//        public override string ToString()
//        {
//            return value.ToString();
//        }
//        #endregion
//    }
//}
