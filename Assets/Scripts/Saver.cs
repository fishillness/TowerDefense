using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    [Serializable]
    public class Saver<T>
    {
        private static string Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }
        public static void TryLoad(string fileName, ref T data)
        {
            var path = Path(fileName);

            if (File.Exists(path))
            {
                var dataString = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.data;
            }
        }

        public static void Save(string fileName, T data)
        {
            var wrapper = new Saver<T> { data = data };
            var dataString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(Path(fileName), dataString);
        }
        public T data;
    }
}