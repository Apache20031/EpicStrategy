using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public HealthSystem health;
    [SerializeField] public PathFollower movement;

    private void Awake() {
        movement.SetFollower(gameObject);
        health.Awake();
        health.Subscribe(OnHealthChanged);
        movement.Awake();
        movement.Subscribe(OnMovementCopleted);
    }

    private void OnDestroy() {
        movement.OnDestroy();
    }

    private void Update() {
        movement.Update();
    }

    private void Death() {
        Destroy(gameObject);
    }

    private void OnHealthChanged(object sender, float health) {
        if (health <= 0) {
            Death();
        }
    }

    private void OnMovementCopleted(object sender) {
        Death();
    }
}
