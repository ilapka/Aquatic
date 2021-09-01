using System.ComponentModel;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameProgressData",menuName = "Aquatic/SavingData/GameProgressData")]
    public class GameProgressData : ScriptableObject
    {
        public int levelValue;
    }
}
