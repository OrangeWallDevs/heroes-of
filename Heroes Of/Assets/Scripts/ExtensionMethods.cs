using System;

public static class ExtensionMethods {
    public static T CloneObject<T>(this object source) {  
        T result = Activator.CreateInstance<T> ();

        return result;  
    }
}
