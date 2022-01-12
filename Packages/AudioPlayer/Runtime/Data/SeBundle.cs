using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [CreateAssetMenu(fileName = "SeBundle", menuName = "AudioSettings/SeBundle")]
    public class SeBundle : ScriptableObject, ISeBundle
    {
        [SerializeField]
        private List<SeData> _seDataList;

        public SeData GetData(int index)
        {
            if (index < 0 || index >= _seDataList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            return _seDataList[index];
        }

        public SeData GetData(string key)
        {
            return _seDataList.First(data => data.Key == key);
        }
    }
}
