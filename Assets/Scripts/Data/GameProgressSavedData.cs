using System.ComponentModel;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameProgressSavedData",menuName = "Aquatic/SavingData/Game Progress Saved Data")]
    public class GameProgressSavedData : ScriptableObject
    {
        public int levelValue;
        public int playerMoney;
    }
}
