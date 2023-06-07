using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private TowerMenu _menu;

    [SerializeField] private float _reload = 1;

    [SerializeField] private List<EffectData> _effects;
}