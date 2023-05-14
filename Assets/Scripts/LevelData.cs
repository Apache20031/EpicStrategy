using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private AvailableTowers _availableTowers;

        public AvailableTowers AvailableTowers => _availableTowers;
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
}
