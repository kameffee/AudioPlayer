using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kameffee.AudioPlayer
{
    [CreateAssetMenu(menuName = "AudioSettings/BgmData", fileName = "BgmData")]
    public class BgmBundle : ScriptableObject, IBgmBundle
    {
        [SerializeField]
        private List<BgmData> _bgmDataList;

        public BgmData GetData(int index)
        {
            if (index < 0 || index >= _bgmDataList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _bgmDataList[index];
        }

        public BgmData GetData(string fileName)
        {
            return _bgmDataList.First(data => data.AudioClip.name == fileName);
        }
    }
}