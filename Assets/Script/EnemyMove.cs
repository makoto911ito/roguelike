using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMove
{
    MoveA = 0,
}


public class EnemyMove : MonoBehaviour
{
    Rigidbody _rd;
    /// <summary></summary>
    [SerializeField] int _count;
    /// <summary></summary>
    [SerializeField] bool _change;
    /// <summary>�^�C�v�ɂ���ē�����ς��邻�̃^�C�v���Ǘ�����ϐ�</summary>
    [SerializeField] EMove _eMove = EMove.MoveA;
    /// <summary>�v���C���[�̃I�u�W�F�N�g���擾</summary>
    [SerializeField] GameObject _player;
    /// <summary>�X�|�[�������w���W���擾���邽�߂̕ϐ�</summary>
    int _pointX;
    /// <summary>�X�|�[�������y���W���擾���邽�߂̕ϐ�</summary>
    int _pointZ;

    AreaController areaController;

    // Start is called before the first frame update
    void Start()
    {
        _rd = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RizumuController._eMoveFlag == true)
        {
            //�^�C�v�ɂ���ē�����ς���
            if (_eMove == EMove.MoveA)
            {
                MoveA();
            }
        }
    }

    void MoveA()
    {
        if (_change == false)
        {
            if (_count == 0)
            {
                //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                areaController = MapManager._areas[_pointX + 1, _pointZ].GetComponent<AreaController>();

                //�ړ���̏��ɂ���čs�������߂�
                if (areaController._onWall == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onEnemy == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onPlayer == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                    _pointX = _pointX + 1;
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
            }
            else
            {
                //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                areaController = MapManager._areas[_pointX - 1, _pointZ].GetComponent<AreaController>();

                //�ړ���̏��ɂ���čs�������߂�
                if (areaController._onWall == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onEnemy == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onPlayer == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    this.transform.position = new Vector3(MapManager._areas[_pointX - 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                    _pointX = _pointX - 1;
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
            }

            _count++;

            if (_count == 2)
            {
                _count = 0;
                _change = true;
            }
        }
        else
        {
            if (_count == 0)
            {
                //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                areaController = MapManager._areas[_pointX - 1, _pointZ].GetComponent<AreaController>();

                //�ړ���̏��ɂ���čs�������߂�
                if (areaController._onWall == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onEnemy == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onPlayer == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    this.transform.position = new Vector3(MapManager._areas[_pointX - 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                    _pointX = _pointX - 1;
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
            }
            else
            {
                //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                areaController = MapManager._areas[_pointX + 1, _pointZ].GetComponent<AreaController>();

                //�ړ���̏��ɂ���čs�������߂�
                if (areaController._onWall == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onEnemy == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else if (areaController._onPlayer == true)
                {
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                    _pointX = _pointX + 1;
                    RizumuController._eMoveFlag = false;
                    RizumuController._moveFlag = false;
                }
            }

            _count++;

            if (_count == 2)
            {
                _count = 0;
                _change = false;
            }
        }
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
                    Debug.Log("���݂̔z��ԍ�" + _pointX + " , " + _pointZ);
                }
            }
        }
    }
}
