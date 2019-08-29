using System.Collections.Generic;
using System.Reflection;
using System;


public class Reflection {

    public static Dictionary<string, object> GetFields(object obj) {
        var fields = new Dictionary<string, object>();

        if (obj == null) {
            return fields;
        }

        Type objType = obj.GetType();
        foreach (FieldInfo field in objType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)) {
            fields.Add(field.Name, field.GetValue(obj));
        }

        return fields;
    }

}
