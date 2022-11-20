using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    /// <summary>�v���C���[�̃f�[�^�Ɋւ��ẴN���X</summary>
    PlayerModel _playerModel = null;
    /// <summary>�v���C���[�̕\���Ɋւ��ẴN���X</summary>
    [SerializeField] PlayerView _playerView = null;

    [SerializeField] EnemyList _enemyList = null;

    /// <summary>�v���C���[��HP</summary>
    [SerializeField] int _playerHp = 1;
    /// <summary>�v���C���[�̍U����</summary>
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
