using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public Enemy Enemy => _enemy;

    private List<SlowdownEffect> _slowdownEffects = new List<SlowdownEffect>();
    private List<ArmorReductionEffect> _armorReductionEffects = new List<ArmorReductionEffect>();

    public void AddEffect(EffectData effectData) {
        Effect effect = null;
        switch (effectData.Type) {
            case EffectType.InstantDamage:
                effect = gameObject.AddComponent<InstantDamage>();
                break;
            case EffectType.IntervalDamage:
                effect = gameObject.AddComponent<IntervalDamage>();
                break;
            case EffectType.DelayDamage:
                effect = gameObject.AddComponent<DelayDamage>();
                break;
            case EffectType.Slowdown:
                effect = gameObject.AddComponent<SlowdownEffect>();
                break;
            case EffectType.ArmorReduction:
                effect = gameObject.AddComponent<ArmorReductionEffect>();
                break;
        }
        effect.SetData(effectData, this);
    }

    public void ApplySlowdownEffect(SlowdownEffect slowdownEffect) {
        _slowdownEffects.Add(slowdownEffect);
        RecalculateSlowdownEffect();
    }

    public void RemoveSlowdownEffect(SlowdownEffect slowdownEffect) {
        _slowdownEffects.Remove(slowdownEffect);
        RecalculateSlowdownEffect();
    }

    public void ApplyArmorReductionEffect(ArmorReductionEffect armorReductionEffect) {
        _armorReductionEffects.Add(armorReductionEffect);
        RecalculateArmorReductionEffect();
    }

    public void RemoveArmorReductionEffect(ArmorReductionEffect armorReductionEffect) { 
        _armorReductionEffects.Remove(armorReductionEffect);
        RecalculateArmorReductionEffect();
    }

    private void RecalculateSlowdownEffect() {
        float slowdown = 0;
        if (_slowdownEffects.Any()) {
            slowdown = _slowdownEffects.Max(effect => effect.EffectData.Strength);
        }
        _enemy.Movement.SetSlowdown(slowdown);
    }

    private void RecalculateArmorReductionEffect() {
        float armorReduction = 0;
        if (_armorReductionEffects.Any()) {
            armorReduction = _armorReductionEffects.Max(effect => effect.EffectData.Strength);
        }
        _enemy.Health.SetArmorReduction(armorReduction);
    }
}
