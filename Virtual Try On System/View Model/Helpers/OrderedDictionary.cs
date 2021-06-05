using System;
using System.Collections.Generic;

namespace Virtual_Try_On_System.View_Model.Helpers
{
    public class OrderedDictionary<TKey, TValue>
    {
        #region Private Fields

        private Dictionary<TKey, TValue> _dictionary;

      
        // The keys added to the dictionary
        
        private List<TKey> _dictionaryKeys;

        #endregion Private Fields
        #region Public  /// <summary>
        // Gets the <see cref="TValue"/> with the specified key.
       
        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set
            {
                _dictionary[key] = value;
                _dictionaryKeys.Remove(key);
                _dictionaryKeys.Add(key);
            }
        }
       
        // Gets the number of items.
        
        public int Count { get { return _dictionaryKeys.Count; } }
       
        // Gets the last added value.
        
       
        // Gets the last key.
       
        public TKey LastKey { get { return _dictionaryKeys[_dictionaryKeys.Count - 1]; } }
       
        // Gets the values from the dictionary
        
        public Dictionary<TKey, TValue>.ValueCollection Values { get { return _dictionary.Values; } }
        #endregion Public Properties
        #region .ctor
      
        // Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
       
        public OrderedDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
            _dictionaryKeys = new List<TKey>();
        }
        
        // Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        
        public OrderedDictionary(OrderedDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary._dictionary);
            _dictionaryKeys = new List<TKey>(dictionary._dictionaryKeys);
        }
        #endregion .ctor
        #region Public Methods
       
        // Removes the specified key from the dictionary.
       
        public void Remove(TKey key)
        {
            _dictionaryKeys.Remove(key);
            try
            {
                _dictionary.Remove(key);
            }
            catch (Exception)
            {
                Console.WriteLine("No such key in the dictionary.");
            }
        }
        
        // Determines whether the specified key contains key.
        
        public bool ContainsKey(TKey key)
        {
            return _dictionaryKeys.Contains(key);
        }
        #endregion Public Methods
    }
}
