using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyMoveBaseA : MoveBase
{
    /// <summary></summary>
    int _count = 0;
    /// <summary></summary>
    bool _change = false;

    bool _canMove = false;

    private readonly EnemyMove _enemyMove;
    private readonly PlayerPresenter _playerPresenter;

    public EnemyMoveBaseA(EnemyMove enemyMove, PlayerPresenter playerPresenter)
    {
        _enemyMove = enemyMove;
        _playerPresenter = playerPresenter;
    }

    public override void Move()
    {
        if(_canMove == false)
        {
            _canMove = true;
        }
        else
        {
            //Debug.Log(_enemyMove._pointX);
            //Debug.Log(_enemyMove._pointZ);

            var px = _enemyMove._pointX;
            var pz = _enemyMove._pointZ;

            //Debug.Log("X���̒l " + px);
            //Debug.Log("Z���̒l " + pz);



            if (_count > 0)
            {
                _count--;
                _change = true;
            }
            else if (_count < 0)
            {
                _count++;
                _change = false;
            }
            else
            {
                if (_change == true)
                {
                    _count--;
                }
                else
                {
                    _count++;
                }

            }

            if (_count > 0 || _count == 0 && _change == true)//���]��false����count��0��������or���]���Ă��Ċ���count��0����Ȃ�����
            {
                //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                var areaController = MapManager._areas[px + 1, pz].GetComponent<AreaController>();

                //�ړ���̏��ɂ���čs�������߂�
                if (areaController._onWall == true)
                {
                    //���΂Ɉړ����邩������Ȃ�
                }
                else if (areaController._onEnemy == true) { }
                else if (areaController._onPlayer == true)
                {
                    //�U��������
                    _playerPresenter.EnemyAttack(1);
                }
                else
                {
                    areaController._onEnemy = true;
                    areaController = MapManager._areas[px, pz].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    _enemyMove.transform.position = new Vector3(MapManager._areas[px + 1, pz].transform.position.x, _enemyMove.transform.position.y, _enemyMove.transform.position.z);
                    px = px + 1;
                }
            }
            else if (_count < 0 || _count == 0 && _change == false) //���]��false����count��0����Ȃ�������or���]���Ă��Ċ���count��0��������
            {
                if (px == 0)
                {
                    Debug.Log("���̐�͉����Ȃ��̂œ����Ȃ�");
                }
                else
                {
                    //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                    var areaController = MapManager._areas[px - 1, pz].GetComponent<AreaController>();

                    //�ړ���̏��ɂ���čs�������߂�
                    if (areaController._onWall == true)
                    {

                    }
                    else if (areaController._onEnemy == true) { }
                    else if (areaController._onPlayer == true)
                    {
                        //�v���C���[�ɍU������
                        _playerPresenter.EnemyAttack(1);
                    }
                    else
                    {
                        areaController._onEnemy = true;
                        areaController = MapManager._areas[px, pz].GetComponent<AreaController>();
                        areaController._onEnemy = false;
                        _enemyMove.transform.position = new Vector3(MapManager._areas[px - 1, pz].transform.position.x, _enemyMove.transform.position.y, _enemyMove.transform.position.z);
                        px = px - 1;
                    }
                }
            }

            _canMove = false;
        }

    }
}
