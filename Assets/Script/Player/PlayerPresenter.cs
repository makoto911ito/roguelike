using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    /// <summary>プレイヤーのデータに関してのクラス</summary>
    PlayerModel _playerModel = null;
    /// <summary>プレイヤーの表示に関してのクラス</summary>
    [SerializeField] PlayerView _playerView = null;

    [SerializeField] EnemyList _enemyList = null;

    /// <summary>プレイヤーのHP</summary>
    [SerializeField] int _playerHp = 1;
    /// <summary>プレイヤーの攻撃力</summary>
    [SerializeField] int _pPower = 1;

    public void Init()
    {
        _playerModel = new PlayerModel(
            _playerHp,
            _pPower,
            x =>
            {
                _playerView.ChangeSliderValue(_playerHp, x);
            },
            gameObject);
    }



    public void Attack(int posx, int posz)
    {
        _enemyList.CheckEnemy(posx, posz,_playerModel._pPower);
    }
}
