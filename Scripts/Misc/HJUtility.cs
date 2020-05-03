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

    public static List<int> GetRandomNumber(List<int> numberList, int numberofElement)
    {
        List<int> selectedIndecies = new List<int>();
        var random = new System.Random();
        for (int i = 0; i < numberofElement; i++)
        {
            if (numberList.Count > 0)
            {
                Shuffle(numberList);
                int index = UnityEngine.Random.Range(0, numberList.Count);
                selectedIndecies.Add(numberList[index]);
                numberList.RemoveAt(index);
            }
            else
                break;
        }
        return selectedIndecies;

    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        var random = new System.Random();
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


}
