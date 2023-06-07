using System.Collections;
using UnityEngine;

public class SlowdownEffect : Effect
{
    protected override void StartEffect() {
        _effectsManager.ApplySlowdownEffect(this);
        StartCoroutine(DurationTimer());
    }

    private IEnumerator DurationTimer() {
        float timer = _effectData.Duration;

        while (timer > 0) {
            yield return null;

            if (!_pause) {
                timer -= Time.deltaTime;
            }
        }
        _effectsManager.RemoveSlowdownEffect(this);
        Destroy(this);
    }
}
