using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utilities {

	public static List<T> ConvertToList<T>(T[] arrayToConvert)
    {
        List<T> list = new List<T>();
        
        foreach(T value in arrayToConvert)
        {
            list.Add(value);
        }

        return list; 
    }
}
