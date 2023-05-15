using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMove
{
    A,
    B,
}

abstract class MoveBase
{
    public abstract void Move();
}

public class EnemyMove : MonoBehaviour
{
    /// <summary></summary>
    [SerializeField] int _count;
    /// <summary></summary>
    [SerializeField] bool _change;
    /// <summary>�X�|�[�������w���W���擾���邽�߂̕ϐ�</summary>
    public int _pointX;
    /// <summary>�X�|�[�������y���W���擾���邽�߂̕ϐ�</summary>
    public int _pointZ;

    /// <summary>�v���C���[�d�l�̃v���[���^�[���擾���邽�߂̕ϐ�</summary>
    PlayerPresenter _playerPresenter;

    /// <summary>�G�d�l�̃v���[���^�[���Q�Ƃ��邽�߂̕ϐ�</summary>
    [SerializeField] EnemyPresenter _enemyPresenter = null;

    public bool _canMove = false;

    GameManager _gameManager;

    //�����ōs���̕ω����Ǘ�����
    private MoveBase _moveType;
    public EMove EMove
    {
        get { return _eMove; }
        set
        {
            switch (value)
            {
                case EMove.A:
                    _moveType = new EnemyTypeA(this, _playerPresenter);
                    break;
                case EMove.B:
                    _moveType = new EnemyMoveBaseB(this, _playerPresenter);
                    break;
            }
            _eMove = value;
        }
    }
    /// <summary>�^�C�v�ɂ���ē�����ς��邻�̃^�C�v���Ǘ�����ϐ�</summary>
    [SerializeField] EMove _eMove = EMove.A;

    private void Start()
    {
        var gm = GameObject.Find("GameManager");
        _gameManager = GetComponent<GameManager>();

        //�v���C���[�̏����擾
        var gameObject = GameObject.Find("Player");
        if (gameObject == null)
        {
            Debug.Log("�v���C���[���擾�ł��܂���ł���");
        }

        _playerPresenter = gameObject.GetComponent<PlayerPresenter>();

        _enemyPresenter.GetLisut();
        _enemyPresenter.Init();

        EMove = _eMove;
    }

    public void MoveEnemy()
    {
        //�G�𓮂���
        _moveType.Move();
    }

    public void Attack()
    {
        //�U��������
        _playerPresenter.Damage(1);
    }


    public void DeleteEnemy()
    {
        var areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
        areaController._onEnemy = false;

        if(gameObject.tag == "Boss")
        {
            _gameManager.DetBoosEnemy();
        }

        if(gameObject.tag == "GameBoss")
        {
            Debug.Log("�Q�[���N���A");
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int x = 0; x < MapManager._x; x++)
        {
            for (int z = 0; z < MapManager._z; z++)
            {
                if (MapManager._areas[x, z] == collision.gameObject)
                {
                    //���݂̈ʒu�𒲂ׂ�
                    _pointX = x;
                    _pointZ = z;
                    //Debug.Log("���݂̔z��ԍ�" + _pointX + " , " + _pointZ);
                    _canMove = true;
                }
            }
        }
    }
}
