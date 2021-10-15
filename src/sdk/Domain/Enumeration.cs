using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace CQRS.Mediatr.Lite.SDK.Domain
{
    /// <summary>
    /// Custom enumeration classes for encapsulating domain behavior
    /// </summary>
    /// <remarks>https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/</remarks>
    public class Enumeration : IComparable
    {
        private readonly int _code;
        private readonly string _name;

        /// <summary>
        /// Code (integer) of the enum
        /// </summary>
        public int Code => _code;

        /// <summary>
        /// Value of the enum
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Blank enum
        /// </summary>
        public Enumeration() { }

        /// <summary>
        /// Constructs enum by code and name
        /// </summary>
        /// <param name="code">Code of the enum</param>
        /// <param name="name">Name of the enum</param>
        public Enumeration(int code, string name)
        {
            _code = code;
            _name = name;
        }

        /// <summary>
        /// Gets all enum of the given type
        /// </summary>
        /// <typeparam name="TEnumeration" cref="Enumeration">Type of the enum</typeparam>
        /// <returns cref="IEnumerable{Enumeration}">List of all available enums of the given type</returns>
        public static IEnumerable<TEnum> GetAll<TEnum>() where TEnum : Enumeration, new()
        {
            Type type = typeof(TEnum);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach(FieldInfo field in fields)
            {
                var instance = new TEnum();
                if (field.GetValue(instance) is TEnum locatedValue)
                    yield return locatedValue;
            }
        }

        /// <summary>
        /// Creates an enum of the given code
        /// </summary>
        /// <typeparam name="TEnumeration" cref="Enumeration">Type of the enum</typeparam>
        /// <param name="code">Code of the enum</param>
        /// <returns cref="Enumeration">Enum with given code</returns>
        public static TEnum FromCode<TEnum>(int code) where TEnum : Enumeration, new()
        {
            return Parse<TEnum, int>(code, nameof(Code), (@enum => @enum.Code == code));
        }

        /// <summary>
        /// Creates an enum of the given name, if name doesn't exist then the default value is returned
        /// </summary>
        /// <typeparam name="TEnumeration" cref="Enumeration">Type of the enum</typeparam>
        /// <param name="name">Name of the enum</param>
        /// <param name="default">Default value incase of invalid name</param>
        /// <returns cref="Enumeration">Enum with given name</returns>
        public static TEnum FromName<TEnum>(string name, TEnum @default = null) where TEnum : Enumeration, new()
        {
            try
            {
                return Parse<TEnum, string>(name, nameof(Name), (@enum => @enum.Name.ToLowerInvariant() == name.ToLowerInvariant()));
            }
            catch (ArgumentException)
            {
                if (@default != null)
                    return @default;
                throw;
            }
        }

        /// <summary>
        /// Name of the enum
        /// </summary>
        /// <returns>Enum name</returns>
        public override string ToString() => _name;

        public int CompareTo(object obj)
        {
            return Name.CompareTo(((Enumeration)obj).Name);
        }

        /// <summary>
        /// Equates 2 enums based on type, code and name
        /// </summary>
        /// <param name="obj">Other enum getting compared</param>
        /// <returns>True if the enums are equal</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherEnum))
                    return false;

            return otherEnum.Code == Code
                && otherEnum.Name.ToLowerInvariant().Equals(Name.ToLowerInvariant());
        }
        
        /// <summary>
        /// Gets the hashcode of the enum code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        /// <summary>
        /// Creates an enum based on field, value and a given predicate
        /// </summary>
        /// <typeparam name="TEnumeration">Type of the enum</typeparam>
        /// <typeparam name="FValue">Type of the field</typeparam>
        /// <param name="value">Value of the field</param>
        /// <param name="field">Field name</param>
        /// <param name="predicate">Predicate for comparing the enum</param>
        /// <returns cref="Enumeration">Parsed enumeration</returns>
        /// <exception cref="ArgumentException">Throws exception when no enum with the given predicate is found</exception>
        protected static TEnum Parse<TEnum, TVal>(TVal value, string field, Func<TEnum, bool> predicate) where TEnum : Enumeration, new()
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Enumeration value cannot be null");

            TEnum @enum = GetAll<TEnum>().FirstOrDefault(predicate);
            if (@enum == null)
                throw new ArgumentException($"Enumeration does not exist. {value} is a valid {field} in {typeof(TEnum)}");
            return @enum;
        }
    }
}
