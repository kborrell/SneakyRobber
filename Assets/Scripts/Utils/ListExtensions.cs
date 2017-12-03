using System;
using System.Collections.Generic;
using System.Linq;

public static class IListExtensions
{
    private static Random random = new Random();
    private static int preferred = 0;

    public static void SetPreferred<T>(this IList<T> list, int index)
    {
        if ((index >= 0) && (index < list.Count))
        {
            preferred = index;
        }
    }

    public static T Random<T>(this IList<T> list)
    {
        if (list.Count == 0)
            throw new IndexOutOfRangeException("Cannot take random element from an empty list");
        return list[random.Next(list.Count)];
    }

    public static T RandomWeighted<T>(this IList<T> list, Func<T, int> weightKey)
    {
        int totalWeight = list.Sum(x => weightKey(x));
        if(totalWeight == 0)
        {
            return list.Random();
        }

        int randomWeightedIndex = random.Next(totalWeight) + 1;
        int itemWeightedIndex = 0;
        foreach (T item in list)
        {
            itemWeightedIndex += weightKey(item);
            if (randomWeightedIndex <= itemWeightedIndex)
                return item;
        }
        throw new ArgumentException("Collection count and weights must be greater than 0");
    }

    public static T NextOrFirst<T>(this IList<T> list, T current)
    {
        int index = list.IndexOf(current);
        if (index == -1)
            throw new IndexOutOfRangeException("Can't found the object " + current + " inside list: " + list);
        return list.NextOrFirst(index);
    }

    public static T PreviousOrLast<T>(this IList<T> list, T current)
    {
        int index = list.IndexOf(current);
        if (index == -1)
            throw new IndexOutOfRangeException("Can't found the object " + current + " inside list: " + list);
        return list.PreviousOrLast(index);
    }

    public static T NextOrFirst<T>(this IList<T> list, int index)
    {
        int listCount = list.Count;
        if (index < 0 || index > (listCount - 1))
            throw new IndexOutOfRangeException("Index is outside list range: " + list);
        return list[(index + 1) % listCount];
    }

    public static T PreviousOrLast<T>(this IList<T> list, int index)
    {
        int listCount = list.Count;
        if (index < 0 || index > (listCount - 1))
            throw new IndexOutOfRangeException("Index is outside list range: " + list);
        return list[(index + listCount - 1) % listCount];
    }

    public static T FirstOrDefault<T>(this IList<T> list)
    {
        return list.Count == 0 ? default(T) : list[0];
    }

    public static T First<T>(this IList<T> list)
    {
        return list[0];
    }

    public static T Preferred<T>(this IList<T> list)
    {
        if (list.Count == 0)
        {
            return default(T);
        }
        else
        {
            if (preferred >= list.Count)
            {
                preferred = 0;
            }
            return list[preferred];
        }
    }

    public static T LastOrDefault<T>(this IList<T> list)
    {
        return list.Count == 0 ? default(T) : list[list.Count - 1];
    }

    public static T Last<T>(this IList<T> list)
    {
        return list[list.Count - 1];
    }
}
