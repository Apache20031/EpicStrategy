using System.Collections;
using UnityEngine;
using Events;
using UnityEngine.UI;
using Money;

public class ConstructionPlace : MonoBehaviour, IRaycastable
{
    [SerializeField] private Vector3 _spawnPosition = Vector3.zero;
    [Space(15)]
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _defaultMenu;
    [SerializeField] private GameObject _cancelMenu;

    private GameObject _menu;
    private GameObject _tower;
    private bool _pause = false;
    private bool _construction = false;
    private Coroutine coroutine;

    public void StartConstruction(GameObject tower, float duration, int price) {
        if (_construction) {
            return;
        }
        _tower?.SetActive(false);
        _construction = true;
        coroutine = StartCoroutine(ConstructionTimer(tower, duration));
        if (_slider) {
            _slider.gameObject.SetActive(true);
            _slider.maxValue = duration;
            _slider.value = 0;
        }
    }

    public void CancelConstruction() {
        _tower?.SetActive(true);
        _construction = false;
        StopCoroutine(coroutine);
        if (_slider) {
            _slider.gameObject.SetActive(false);
        }
    }

    public void SellTower(int sellPrice) {
        if (_tower) {
            MoneyManager.AddMoney(sellPrice);
            Destroy(_tower);
        }
    }

    public void OnRaycastHit() {
        SetMenuActive(true);
    }

    public void OnRaycastMissed() {
        SetMenuActive(false);
    }

    private void Awake() {
        Observer.Subscribe<PauseEvent>(Pause);
    }

    private void OnDestroy() {
        Observer.Unsubscribe<PauseEvent>(Pause);
    }

    private void BuildTower(GameObject tower) {
        if (_tower) {
            Destroy(_tower);
        }
        _tower = Instantiate(tower, gameObject.transform);
        _tower.transform.localPosition = _spawnPosition;
        _construction = false;
        if (_slider) {
            _slider.gameObject.SetActive(false);
        }
    }

    private void SetMenuActive(bool active) {
        _menu?.SetActive(active);
    }

    private IEnumerator ConstructionTimer(GameObject tower, float duration) {
        float timer = duration;

        while (timer > 0f) {
            yield return null;

            if (!_pause) {
                timer -= Time.deltaTime;
                _slider.value = duration - timer;
            }
        }

        BuildTower(tower);
    }

    private void Pause(object sender, PauseEvent eventData) {
        _pause = eventData.pause;
    }
}
