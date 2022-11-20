using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMove
{
    MoveA = 0,
}


public class EnemyMove : MonoBehaviour
{
    /// <summary></summary>
    [SerializeField] int _count;
    /// <summary></summary>
    [SerializeField] bool _change;
    /// <summary>�^�C�v�ɂ���ē�����ς��邻�̃^�C�v���Ǘ�����ϐ�</summary>
    [SerializeField] int _eMove = 0;
    /// <summary>�v���C���[�̃I�u�W�F�N�g���擾</summary>
    [SerializeField] GameObject _player;
    /// <summary>�X�|�[�������w���W���擾���邽�߂̕ϐ�</summary>
    public int _pointX;
    /// <summary>�X�|�[�������y���W���擾���邽�߂̕ϐ�</summary>
    public int _pointZ;

    AreaController areaController;

    public void MoveEnemy()
    {
        //�^�C�v�ɂ���ē�����ς���
        if (_eMove == (int)EMove.MoveA)
        {
            MoveA();
        }
    }

    void MoveA()
    {
        if (_change == false && _count == 0 || _change == true && _count != 0)//���]��false����count��0��������or���]���Ă��Ċ���count��0����Ȃ�����
        {
            //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
            areaController = MapManager._areas[_pointX + 1, _pointZ].GetComponent<AreaController>();

            //�ړ���̏��ɂ���čs�������߂�
            if (areaController._onWall == true)
            {
                //���΂Ɉړ����邩������Ȃ�
            }
            else if (areaController._onEnemy == true) { }
            else if (areaController._onPlayer == true)
            {
                //�U��������
            }
            else
            {
                areaController._onEnemy = true;
                areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                areaController._onEnemy = false;
                this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                _pointX = _pointX + 1;
            }
        }
        else if (_change == false && _count != 0 || _change == true && _count == 0) //���]��false����count��0����Ȃ�������or���]���Ă��Ċ���count��0��������
        {
            //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
            areaController = MapManager._areas[_pointX - 1, _pointZ].GetComponent<AreaController>();

            //�ړ���̏��ɂ���čs�������߂�
            if (areaController._onWall == true)
            {

            }
            else if (areaController._onEnemy == true) { }
            else if (areaController._onPlayer == true)
            {
                //�v���C���[�ɍU������
            }
            else
            {
                areaController._onEnemy = true;
                areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                areaController._onEnemy = false;
                this.transform.position = new Vector3(MapManager._areas[_pointX - 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                _pointX = _pointX - 1;
            }
        }

        _count++;

        if (_count == 2)
        {
            _count = 0;
            if(_change == false)
            {
                _change = true;
            }
            else
            {
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
