using UnityEngine;
using Money;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthSystem _health;
    [SerializeField] private PathFollower _movement;
    [SerializeField] private int _damage = 1; 
    [SerializeField] private int _reward = 1;

    public HealthSystem Health => _health;
    public PathFollower Movement => _movement;

    private void Awake() {
        _health.Awake();
        _health.Subscribe(OnHealthChanged);
        _movement.SetFollower(gameObject);
        _movement.Awake();
        _movement.Subscribe(OnMovementCompleted);
    }

    private void OnDestroy() {
        _movement.OnDestroy();
    }

    private void Update() {
        _movement.Update();
    }

    private void Death() {
        MoneyManager.AddMoney(_reward);
        Destroy(gameObject);
    }

    private void PathComplete() {
        Destroy(gameObject);
    }

    private void OnHealthChanged(object sender, float health) {
        if (health <= 0) {
            Death();
        }
    }

    private void OnMovementCompleted(object sender) {
        PathComplete();
    }
}
