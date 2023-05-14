using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] public LevelData levelData;

        public static LevelData LevelData => _instance.levelData;

        private static LevelManager _instance;

        private void Awake() {
            if (_instance == null) {
                _instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}

