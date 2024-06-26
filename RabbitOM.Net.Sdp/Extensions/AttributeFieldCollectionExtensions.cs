﻿using System;
using System.Linq;

namespace RabbitOM.Net.Sdp.Extensions
{
    /// <summary>
    /// Represent the collection for attribute field that a allow duplicate field names
    /// </summary>
    public static class AttributeFieldCollectionExtensions
    {
        /// <summary>
        /// Gets a field using it's name
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <param name="name">the name</param>
        /// <returns>returns an instance</returns>
        public static AttributeField GetByName(this AttributeFieldCollection collection, string name)
        {
            return GetByName(collection, name, true);
        }

        /// <summary>
        /// Gets a field using it's name
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <param name="name">the name</param>
        /// <param name="ignoreCase">set true to ignore the case</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public static AttributeField GetByName(this AttributeFieldCollection collection, string name, bool ignoreCase)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));
            return collection.First(field => AttributeField.Equals(field, name, ignoreCase)) ?? throw new InvalidOperationException("The returns field is null");
        }

        /// <summary>
        /// Finds a field using it's name
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <param name="name">the name</param>
        /// <returns>returns an instance,otherwise null</returns>
        public static AttributeField? FindByName(this AttributeFieldCollection collection, string name)
        {
            return FindByName(collection, name, true);
        }

        /// <summary>
        /// Gets a field using it's name
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <param name="name">the name</param>
        /// <param name="ignoreCase">set true to ignore the case</param>
        /// <returns>returns an instance,otherwise null</returns>
        /// <exception cref="ArgumentNullException"/>
        public static AttributeField? FindByName(this AttributeFieldCollection collection, string name, bool ignoreCase)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));
            return collection.FirstOrDefault(field => AttributeField.Equals(field, name, ignoreCase));
        }

        /// <summary>
        /// Try to get a field using it's name
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <param name="name">the name</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryGetByName(this AttributeFieldCollection collection, string name, out AttributeField result)
        {
            return TryGetByName(collection, name, true, out result);
        }

        /// <summary>
        /// Try to get a field using it's name
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <param name="name">the name</param>
        /// <param name="ignoreCase">set to true to ignore the case</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryGetByName(this AttributeFieldCollection collection, string name, bool ignoreCase, out AttributeField result)
        {
            result = collection?.FirstOrDefault(field => AttributeField.Equals(field, name, ignoreCase));

            return result != null;
        }
    }
}
