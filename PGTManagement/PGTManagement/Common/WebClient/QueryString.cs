using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace PGTManagement.Common.WebClient
{
    public static class QueryString
    {
        public static string ToQueryString(this object model)
        {
            return model.ToNameValueCollection().ToQueryString();
        }

        public static string ToQueryString(this Dictionary<string, string> collection)
        {
            if (collection != null)
            {
                var keyPars = collection.Select(keypar => String.Format("{0}={1}",
                                                            WebUtility.UrlEncode(keypar.Key),
                                                            WebUtility.UrlEncode(keypar.Value)));
                var parameters = String.Join("&", keyPars);
                var queryString = String.Format("?{0}", parameters);
                return queryString;
            }

            return string.Empty;
        }

        public static string ToQueryString(this NameValueCollection collection)
        {
            if (collection != null)
            {
                var keyPars = collection.AllKeys.SelectMany(key => collection.GetValues(key)
                    .Select(value => String.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value))))
                    .ToArray();
                var parameters = String.Join("&", keyPars);
                var queryString = String.Format("?{0}", parameters);
                return queryString;
            }

            return string.Empty;
        }

        public static string ToQueryString(this NameValueCollection collection, string separator)
        {
            if (collection != null)
            {
                var keyPars = collection.AllKeys.SelectMany(key => collection.GetValues(key)
                    .Select(value => String.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value))))
                    .ToArray();
                var parameters = String.Join(separator, keyPars);
                var queryString = String.Format(separator + "{0}", parameters);
                return queryString;
            }

            return string.Empty;
        }

        public static NameValueCollection ToNameValueCollection(this Dictionary<string, string> properties)
        {
            return properties.ToNameValueCollection(_ => _.Key, _ => _.Value);
        }

        public static NameValueCollection ToNameValueCollection(this object model)
        {
            if (model != null)
            {
                var properties = model.ToDictionary();

                return properties.ToNameValueCollection(_ => _.Key, _ => _.Value);
            }

            return null;
        }

        private static NameValueCollection ToNameValueCollection<TSource, TKey, TElement>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            var collection = new NameValueCollection(EqualityComparer<TKey>.Default);
            foreach (var item in source.Where(item => !Equals(elementSelector(item), null)))
            {
                string element;
                if (elementSelector(item) is DateTime || elementSelector(item) is DateTime?)
                {
                    element = Convert.ToDateTime(elementSelector(item)).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else if (elementSelector(item) is decimal || elementSelector(item) is decimal?)
                {
                    element = Convert.ToDecimal(elementSelector(item)).ToString("0.00", CultureInfo.InvariantCulture);
                }
                else
                {
                    element = elementSelector(item).ToString();
                }

                collection.Add(keySelector(item).ToString(), element);
            }
            return collection;
        }

        public static object GetDefaultValue(Type t)
        {
            if (t.GetTypeInfo().IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }

        private static Dictionary<string, object> ToDictionary(this object model)
        {
            var properties = model.GetType()
                                         .GetProperties()
                                         .Where(x => x.CanRead)
                                         .Where(x => x.GetValue(model, null) != null)
                                         .ToDictionary(x => x.Name, x => x.GetValue(model, null));

            var propertyNames = properties.Where(x => !(x.Value is string) && (x.Value is IEnumerable || x.Value.GetType().GetTypeInfo().IsClass))
                                          .Select(x => x.Key)
                                          .ToList();

            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.GetTypeInfo().IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType() ?? valueType;
                if (valueElemType.GetTypeInfo().IsPrimitive ||
                    valueElemType == typeof(string) ||
                    valueElemType.GetGenericArguments().Any(t => t.GetTypeInfo().IsValueType && t.GetTypeInfo().IsPrimitive))
                {
                    var enumerable = properties[key] as IEnumerable;
                    foreach (var item in enumerable.Cast<object>().Select((value, i) => new { i, value }))
                    {
                        properties.Add(key + "[" + item.i + "]", item.value);
                    }
                    properties.Remove(key);
                }
                else if (!valueElemType.GetTypeInfo().IsPrimitive &&
                         valueElemType.Namespace != null &&
                         valueElemType.Namespace.Contains("CarSharing"))
                {
                    var enumerable = properties[key] as IEnumerable<object>;
                    if (enumerable != null)
                    {
                        foreach (var subProp in enumerable.Select((value, i) => new { i, value }))
                        {
                            var dictionarySubProp = subProp.value.ToDictionary();
                            foreach (var dictionary in dictionarySubProp)
                            {
                                properties.Add(key + "[" + subProp.i + "]." + dictionary.Key, dictionary.Value);
                            }
                        }
                        properties.Remove(key);
                    }
                    else
                    {
                        var dictionarySubProp = properties[key].ToDictionary();
                        foreach (var dictionary in dictionarySubProp)
                        {
                            properties.Add(key + "." + dictionary.Key, dictionary.Value);
                        }
                        properties.Remove(key);
                    }
                }
            }

            return properties;
        }
    }
}
