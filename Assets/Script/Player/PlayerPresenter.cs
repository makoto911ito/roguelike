using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    /// <summary>�v���C���[�̃f�[�^�Ɋւ��ẴN���X</summary>
    PlayerModel _playerModel = null;

    /// <summary>�v���C���[�̕\���Ɋւ��ẴN���X</summary>
    [SerializeField] PlayerView _playerView = null;

    //���݂̃}�b�v��ɂ���G�̏����܂Ƃ߂����X�g�̏���
    //[SerializeField] EnemyList _enemyList = null;

    /// <summary>�v���C���[��HP</summary>
    [SerializeField] int _playerHp = 1;

    GameManager _gameManager = null;

    int _myPosX = 0;
    int _myPosZ = 0;


    public void SetLife(GameObject life, GameManager gm)
    {
        _gameManager = gm;
        _playerView.SetSlider(life);
        StartCoroutine(StatInit());
    }

    IEnumerator StatInit()
    {
        yield return new WaitForSeconds(1);
        Init();
    }

    public void Init()
    {

        _playerModel = new PlayerModel(
            _playerHp,
            x =>
            {
                _playerView.ChangeSliderValue(_playerHp, x);
                if(x <= 0)
                {
                    _gameManager.GameOvare(_myPosX,_myPosZ);
                    Destroy(this.gameObject);
                }
            },
            _playerView.gameObject);


    }

    public void SaveMyPosition(int posX, int posZ)
    {
        _myPosX = posX;
        _myPosZ = posZ;

        Debug.Log(_myPosX + " / " + _myPosZ);
    }

    //public void Attack(int posx, int posz)
    //{
    //    _enemyList.CheckEnemy(posx, posz, _playerModel._pPower);
    //}

    public void EnemyAttack(int ePower)
    {
        if (_playerModel == null)
        {
            Debug.Log("PlayerModel��null�ł�");
        }

        Debug.Log("�G����_���[�W��H����Ă���");
        _playerModel.Damage(ePower);

    }
}
