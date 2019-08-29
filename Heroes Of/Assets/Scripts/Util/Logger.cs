using UnityEngine;
using System.Collections.Generic;
using System;

public class Logger : Singleton<Logger> {

    public void PrintObject(object obj) {
        var fields = Reflection.GetFields(obj);
        string message = "";

        Debug.Log(fields.Count);

        foreach (var field in fields) {
            // message += '#';
            message += String.Format("{0} = {1}\n", field.Key, field.Value ?? "null");
        }

        Debug.Log(message);
    }

}
