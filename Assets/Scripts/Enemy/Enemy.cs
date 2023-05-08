using UnityEngine;
using Money;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthSystem _health;
    [SerializeField] private PathFollower _movement;
    [SerializeField] private int _damage = 1; 
    [SerializeField] private int _bounty = 1;

    public HealthSystem Health => _health;
    public PathFollower Movement => _movement;

    private void Awake() {
        _health.Awake();
        _health.Subscribe(OnHealthChanged);
        _movement.SetFollower(gameObject);
        _movement.Awake();
        _movement.Subscribe(OnMovementCopleted);
    }

    private void OnDestroy() {
        _movement.OnDestroy();
    }

    private void Update() {
        _movement.Update();
    }

    [ContextMenu("Destroy")]
    private void Death() {
        MoneyManager.AddMoney(_bounty);
        Destroy(gameObject);
    }

    private void OnHealthChanged(object sender, float health) {
        if (health <= 0) {
            Death();
        }
    }

    private void OnMovementCopleted(object sender) {
        Destroy(gameObject);
    }
}
