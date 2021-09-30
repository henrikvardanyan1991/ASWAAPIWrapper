using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ASWAWrapper.Common.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Any())
            {
                return attributes.Select(x => x.Description).FirstOrDefault();
            }

            else
            {
                return value.ToString();
            }
        }

        public static List<Tuple<int, string, string>> GetEnumNamesAndDescrptions<T>()
        {
            List<Tuple<int, string, string>> enumDescriptions = new List<Tuple<int, string, string>>();

            Type enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("T is not System.Enum");
            }

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var fi = e.GetType().GetField(e.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                string description = string.Empty;
                if (attributes != null && attributes.Any())
                {
                    description = !string.IsNullOrEmpty(attributes.Select(x => x.Description).FirstOrDefault()) ? attributes.Select(x => x.Description).FirstOrDefault() : e.ToString();
                }
                else
                {
                    description = e.ToString();
                }

                enumDescriptions.Add(new Tuple<int, string, string>((int)e, e.ToString(), description));
            }

            enumDescriptions.OrderBy(x => x.Item2);

            return enumDescriptions;

        }


        public static List<Tuple<int, string>> GetEnumDescrptions<T>()
        {
            List<Tuple<int, string>> enumDescriptions = new List<Tuple<int, string>>();

            Type enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("T is not System.Enum");
            }

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var fi = e.GetType().GetField(e.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                string description = string.Empty;
                if (attributes != null && attributes.Any())
                {
                    description = !string.IsNullOrEmpty(attributes.Select(x => x.Description).FirstOrDefault()) ? attributes.Select(x => x.Description).FirstOrDefault() : e.ToString();
                }
                else
                {
                    description = e.ToString();
                }

                enumDescriptions.Add(new Tuple<int, string>((int)e, description));
            }

            enumDescriptions.OrderBy(x => x.Item2);

            return enumDescriptions;

        }

        public static List<Tuple<int, string>> GetEnumNameAndValue<T>()
        {
            List<Tuple<int, string>> enumNameAndValue = new List<Tuple<int, string>>();

            Type enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("T is not System.Enum");
            }

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var fi = e.GetType().GetField(e.ToString());
                string name = e.ToString();
                enumNameAndValue.Add(new Tuple<int, string>((int)e, name));
            }

            enumNameAndValue.OrderBy(x => x.Item1);
            return enumNameAndValue;

        }
    }
}
