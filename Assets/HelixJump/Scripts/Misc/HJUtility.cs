using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class HJUtility
{
    public static Color ConvertHexaToColor(string hexaColorWithoutHash)
    {
        Color color = Color.white;
        string hexaColor = "#" + hexaColorWithoutHash;
        if (ColorUtility.TryParseHtmlString(hexaColor, out color))
        {

        }
        return color;
    }

    public static List<int> GetRandomNumber(int from, int to, int numberOfElement)
    {
        var random = new System.Random();
        HashSet<int> numbers = new HashSet<int>();
        while (numbers.Count < numberOfElement)
        {
            numbers.Add(random.Next(from, to));
        }
        return numbers.ToList();
    }

    public static List<int> GetRandomNumber(List<int> numberList,int numberofElement)
    {
        var random = new System.Random();
        return numberList.OrderBy(x => random.Next()).Take(numberofElement).ToList();
    }


}
