using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerModel
{
    /// <summary>プレイヤーのHPの変化を管理する変数</summary>
    ReactiveProperty<int> _playerHpProperty;
    int _maxHp = 0;

    /// <summary>プレイヤーの攻撃力</summary>
    public int _pPower = 1;

    public PlayerModel(int maxHp,int pPower, System.Action<int> action, GameObject gameObject)
    {
        _maxHp = maxHp;
        _pPower = pPower;
        _playerHpProperty = new ReactiveProperty<int>(maxHp);
        _playerHpProperty.Subscribe(action).AddTo(gameObject);
    }

    public void Damage(int damage)
    {
        _playerHpProperty.Value -= damage;
    }
}
