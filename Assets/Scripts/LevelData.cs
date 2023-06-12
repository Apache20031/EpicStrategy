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

    public enum TowerType {
        ArcherTower,
        CannonTower,
        MagicTower,
        PoisonTower,
        SlowTower
    }

    [System.Serializable]
    public class AvailableTowers
    {
        [SerializeField] private bool[] _archerTower = { false, false, false};
        [SerializeField] private bool[] _cannonTower = { false, false, false};
        [SerializeField] private bool[] _magicTower = { false, false, false};
        [SerializeField] private bool[] _poisonTower = { false, false, false};
        [SerializeField] private bool[] _slowTower = { false, false, false};

        public bool GetTowerAvailable(TowerType tower, int level) {
            switch (tower) {
                case TowerType.ArcherTower:
                    return _archerTower[level];
                case TowerType.CannonTower:
                    return _cannonTower[level];
                case TowerType.MagicTower:
                    return _magicTower[level];
                case TowerType.PoisonTower:
                    return _poisonTower[level];
                case TowerType.SlowTower:
                    return _slowTower[level];
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
