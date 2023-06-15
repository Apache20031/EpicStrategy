using Level;
using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private const int REMAINING_ENEMY_COUNT_FOR_NEXT_WAVE = 10;

    [SerializeField] private EnemiesData _enemiesData;
    [SerializeField] private List<PathData> _paths;

    private List<Coroutine> _spawners = new List<Coroutine>();
    private List<GameObject> _enemies = new List<GameObject>();
    private bool _wavesStarted = false;
    private bool _pause = false;
    private bool _nextWaveAvailable = true;
    private int _waveIndex = -1;
    private float _nextWaveTimer;

    private void Awake() {
        Observer.Subscribe<PauseEvent>(Pause);
    }

    private void OnDestroy() {
        Observer.Unsubscribe<PauseEvent>(Pause);
        StopAllCoroutines();
    }

    private void SpawnEnemy(string enemyName, int pathIndex) {

    }

    private void StartNextWave() {
        if (!_wavesStarted) {
            StartCoroutine(NextWaveDelegator());
        }
        _waveIndex++;
        _nextWaveTimer = LevelManager.LevelData.WavesData.Waves[_waveIndex].NextWaveDelay;
        _nextWaveAvailable = false;
        foreach (WavePathData wavePathData in LevelManager.LevelData.WavesData.Waves[_waveIndex].Paths) {
            _spawners.Add(StartCoroutine(Spawner(wavePathData)));
        }
    }

    private IEnumerator NextWaveDelegator() {
        while (true) {
            yield return null;

            if (_spawners.Count > 0 || _enemies.Count > REMAINING_ENEMY_COUNT_FOR_NEXT_WAVE) {
                continue;
            }
            if (_waveIndex == LevelManager.LevelData.WavesData.WavesCount - 1 && _enemies.Count == 0) {
                yield break;
            }
            if (!_pause) {
                _nextWaveTimer -= Time.deltaTime;

                if (_nextWaveTimer <= 0) {
                    StartNextWave();
                }
            }
        }
    }
    private IEnumerator Spawner(WavePathData wavePathData) {
        float timer = 0;
        float count;
        foreach (EnemySpawnData enemySpawnData in wavePathData.Enemies) {
            count = enemySpawnData.Count;
            while (count > 0) {
                yield return null;

                if (!_pause) {
                    timer -= Time.deltaTime;

                    if (timer <= 0) {
                        SpawnEnemy(enemySpawnData.EnemyName, wavePathData.Index);
                        timer = enemySpawnData.Interval;
                        count--;
                    }
                }
            }
        }
    }

    private void Pause(object sender, PauseEvent eventData) {
        _pause = eventData.pause;
    }

    [System.Serializable]
    private class PathData {
        [SerializeField] private int _index;
        [SerializeField] private EnemyPath _enemyPath;

        public int Index => _index;
        public EnemyPath EnemyPath => _enemyPath;
    }
}
