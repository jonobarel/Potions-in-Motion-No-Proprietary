using UnityEngine;

namespace ZeroPrep.Utils
{
    public class Helpers
    {
        public static void Shuffle<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int randomIndex = Random.Range(0, array.Length - 1);
                T temp = array[randomIndex];
                array[randomIndex] = array[i];
                array[i] = temp;
            }
        }

        public static string ArrayToString<T>(T[] array)
        {
            string ret = "[";
            for (int i = 0; i < array.Length; i++)
            {
                ret += array[i].ToString();
                if (i < array.Length - 1)
                {
                    ret += ", ";
                }
            }

            ret += "]";
            return ret;
        }
    }
}