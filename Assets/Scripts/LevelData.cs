using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private AvailableTowers _availableTowers;
        [SerializeField] private AvailableResources _availableResources;

        public AvailableTowers AvailableTowers => _availableTowers;
        public AvailableResources AvailableResources => _availableResources;
    }

    [System.Serializable]
    public class AvailableTowers
    {
        [SerializeField] private bool _archerTower1 = false;
        [SerializeField] private bool _archerTower2 = false;
        [SerializeField] private bool _archerTower3 = false;

        public bool GetTowerAvailable(int ID) {
            switch (ID) {
                case 0:
                    return _archerTower1;
                case 1:
                    return _archerTower2;
                case 2:
                    return _archerTower3;
                default:
                    return false;
            }
        }
    }

    [System.Serializable]
    public class AvailableResources {
        [SerializeField] private int _startMoney = 10;

        public int StartMoney => _startMoney;
    }
}
