using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    EnemyModel _enemyModel = null;

    [SerializeField] EnemyView _enemyView = null;

    [SerializeField] int _enemyHp = 1;

    public void Init()
    {
        _enemyModel = new EnemyModel(
            _enemyHp,
            x =>
            {
                _enemyView.ChangeSliderValue(_enemyHp, x);
            },
            _enemyView.gameObject);
    }

    public void Damage(int pPower)
    {
        _enemyModel.Damage(pPower);
    }
}
