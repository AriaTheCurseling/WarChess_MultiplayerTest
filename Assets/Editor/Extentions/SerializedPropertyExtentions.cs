using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Editor.Extentions
{
    public static class SerializedPropertyExtentions
    {
        public class PathObject
        {
            public PathObject Parent { get; private set; }
            public object Obj { get; private set; }

            public PathObject(PathObject parent, object obj)
            {
                Parent = parent;
                Obj = obj;
            }
        }

        static public object Get(this SerializedProperty prop)
        {
            string path = prop.propertyPath.Replace(".Array.data[", "[");

            object obj = prop.serializedObject.targetObject;

            string[] elements = path.Split('.');

            foreach (string element in elements)
            {
                if (element.Contains("["))
                {
                    string elementName = element.Substring(0, element.IndexOf("["));
                    int index = Convert.ToInt32(element.Substring(element.IndexOf("[") + 1, element.Length - 1));
                    obj = GetValue(obj, elementName, index);
                }
                else
                    obj = GetValue(obj, element);
            }

            return obj;
        }

        static public PathObject GetWithParentInfo(this SerializedProperty prop)
        {
            string path = prop.propertyPath.Replace(".Array.data[", "[");

            object obj = prop.serializedObject.targetObject;
            PathObject pathObj = new PathObject(null, obj);

            string[] elements = path.Split('.');

            foreach (string element in elements)
            {
                if (element.Contains("["))
                {
                    string elementName = element.Substring(0, element.IndexOf("["));
                    int index = Convert.ToInt32(element.Substring(element.IndexOf("[") + 1, element.Length - 1));
                    obj = GetValue(obj, elementName, index);
                }
                else
                    obj = GetValue(obj, element);

                pathObj = new PathObject(pathObj, obj);
            }

            return pathObj;
        }


        public static object GetValue(object source, string name)
        {
            Type type = source.GetType();

            FieldInfo f = type.GetField(name, (BindingFlags)(-1));

            if (f is object)
                return f.GetValue(source);

            PropertyInfo p = type.GetProperty(name, (BindingFlags)(-1));

            if (p is object)
                return p.GetValue(source);

            return null;
        }

        public static object GetValue(object source, string name, int index)
        {
            var enumerable = GetValue(source, name) as IEnumerable;
            var enm = enumerable.GetEnumerator();
            while (index-- >= 0)
                enm.MoveNext();
            return enm.Current;
        }
    }
}
