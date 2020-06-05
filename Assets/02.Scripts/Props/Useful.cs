using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ExtensionMethods {

    public static class Useful
    {
        public static Color32 blank = new Color32(255, 255, 255, 0);

        private static System.Random rdg = new System.Random();

        public static void Shuffle<T>(this IList<T> list) //Fisher-Yates Shuffle
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rdg.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.RandomElementUsing<T>(rdg);
        }

        public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, System.Random rand)
        {
            int index = rand.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }

        public static IEnumerable<TValue> RandomValues<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            List<TValue> values = Enumerable.ToList(dict.Values);
            int size = dict.Count;
            while (true)
            {
                yield return values[rdg.Next(size)];
            }
        }

        public static void SetActiveness(this bool activeness, params GameObject[] go)
        {
            for (int i = 0; i < go.Length; i++) go[i].gameObject.SetActive(activeness);
        }

        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
        
        private static char[] punktuationMarks = new char[] { '.', ',', '!', '?' };

        public static bool isPunctuationMark(this string speech, int i)
        {
            if (i <= 0 || i >= speech.Length - 1) return false;
            if (punktuationMarks.Contains(speech[i]) && !punktuationMarks.Contains(speech[i + 1]) && !punktuationMarks.Contains(speech[i - 1])) return true;
            else return false;
        }
        
        public static bool IsEmpty<T>(this IEnumerable<T> data)
        {
            return data.All(x => x == null);
        }

        public static bool IsNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }

    }

    public static class Lerp{

        //인자로 duration 받게해서 부른 코루틴에서는 duration만큼 기다리게 하면 될듯

        //private enum LerpWhat
        //{
        //    Position,
        //    Scale,
        //};
        //private static LerpWhat lerpWhat = LerpWhat.Position;
        

        public static void LinearLerp(this float lerpDuration)
        {

        }

        //enum cheezy

        //IEquatable

        //public static Vector3 CheezyLerp(this float lerpDuration, Vector3 origin, Vector3 dest)
        //{
        //    float currentLerpTime = 0f;
        //    while (currentLerpTime >= lerpDuration)
        //    {
        //        float perc = currentLerpTime / lerpDuration;
        //        perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                
        //        origin
        //    }
        //}


    }

}