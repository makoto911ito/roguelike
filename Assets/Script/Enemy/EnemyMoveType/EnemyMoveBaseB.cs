using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyMoveBaseB : MoveBase
{
    private readonly EnemyMove _enemyMove;
    private readonly PlayerPresenter _playerPresenter;

    bool _frg = false;

    /// <summary>�X�e�[�W�̏����Ǘ����Ă���X�N���v�g���Q�Ƃ��邽�߂̕ϐ�</summary>
    AreaController areaController;

    public EnemyMoveBaseB(EnemyMove enemyMove, PlayerPresenter playerPresenter)
    {
        _enemyMove = enemyMove;
        _playerPresenter = playerPresenter;
    }

    public override void Move()
    {
        Debug.Log("�G�̏����l" + "x " + _enemyMove._pointX + " z " + _enemyMove._pointZ);
        if(_enemyMove._canMove == true)
        {
            for (int i = _enemyMove._pointX - 5; i <= _enemyMove._pointX + 5; i++)
            {
                if (MapManager._areas.GetLength(0) <= i || 0 > i)
                {
                    continue;
                }
                else
                {
                    for (int j = _enemyMove._pointZ - 5; j <= _enemyMove._pointZ + 5; j++)
                    {
                        if (MapManager._areas.GetLength(1) <= j || 0 > j)
                        {
                            continue;
                        }
                        else
                        {

                            Debug.Log("x " + i + " z " + j);
                            var areaController = MapManager._areas[i, j].GetComponent<AreaController>();

                            if (areaController._onPlayer == true)
                            {
                                Debug.Log("�v���C���[��������");
                                CheackDistance(i, j);

                                break;
                            }
                        }
                    }
                }

                Debug.Log("�v���C���[��������Ȃ�����");
            }
        }
    }

    private void CheackDistance(int _PlayerX, int _PlayerZ)
    {
        var _cheackNumX = 0;
        var _cheackNumZ = 0;

        if (_PlayerX < 0 && _enemyMove._pointX < 0 || _PlayerZ > 0 && _enemyMove._pointX > 0)
        {
            int _distanceX = _PlayerX - _enemyMove._pointX;
            int _distanceZ = _PlayerZ - _enemyMove._pointX;

            if (_distanceX < 0)//�}�C�i�X�̂܂܂��Ɣ�r�����炢�̂Ń}�C�i�X���O��
            {
                _cheackNumX = _distanceX * -1;
            }
            else
            {
                _cheackNumX = _distanceX;
            }

            if (_distanceZ < 0)//�}�C�i�X�̂܂܂��Ɣ�r�����炢�̂Ń}�C�i�X���O��
            {
                _cheackNumZ = _distanceZ * -1;
            }
            else
            {
                _cheackNumZ = _distanceZ;
            }

            if (_cheackNumX == 0)
            {
                if (_PlayerZ - _enemyMove._pointZ < 0)
                {
                    _frg = false;
                }
                else
                {
                    _frg = true;
                }
                //X���W�̋������O�������̂�Z�̕��ɍs���R�[�h�ɔ��
                GoMove(_frg, "z");
            }
            else if (_cheackNumZ == 0)
            {
                if (_PlayerX - _enemyMove._pointX < 0)
                {
                    _frg = false;
                }
                else
                {
                    _frg = true;
                }

                //Z���W�̋������O�������̂�X�̕��ɍs���R�[�h�ɔ��
                GoMove(_frg, "x");
            }
            else
            {
                if (_cheackNumX > _cheackNumZ)
                {
                    if(_PlayerX - _enemyMove._pointX < 0)
                    {
                        _frg = false;
                    }
                    else
                    {
                        _frg = true;
                    }

                    //X�̕������������̂ŋl�߂�悤�ɓ���
                    GoMove(_frg, "x");
                }
                else
                {
                    if (_PlayerZ - _enemyMove._pointZ < 0)
                    {
                        _frg = false;
                    }
                    else
                    {
                        _frg = true;
                    }
                    //Z�̂ق������������̂ŋl�߂�悤�ɓ���
                    GoMove(_frg, "z");
                }
            }
        }
    }

    private void GoMove(bool _distance, string _muki)
    {
        if (_muki == "x")
        {
            if (_frg == true)
            {
                areaController = MapManager._areas[_enemyMove._pointX + 1, _enemyMove._pointZ].GetComponent<AreaController>();
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
                    areaController = MapManager._areas[_enemyMove._pointX, _enemyMove._pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    _enemyMove.transform.position = new Vector3(MapManager._areas[_enemyMove._pointX + 1, _enemyMove._pointZ].transform.position.x, _enemyMove.transform.position.y, _enemyMove.transform.position.z);
                    _enemyMove._pointX = _enemyMove._pointX + 1;
                }
            }
            else
            {
                areaController = MapManager._areas[_enemyMove._pointX - 1, _enemyMove._pointZ].GetComponent<AreaController>();
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
                    areaController = MapManager._areas[_enemyMove._pointX, _enemyMove._pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    _enemyMove.transform.position = new Vector3(MapManager._areas[_enemyMove._pointX - 1, _enemyMove._pointZ].transform.position.x, _enemyMove.transform.position.y, _enemyMove.transform.position.z);
                    _enemyMove._pointX = _enemyMove._pointX - 1;
                }
            }
        }
        else
        {
            if (_frg == true)
            {
                areaController = MapManager._areas[_enemyMove._pointX, _enemyMove._pointZ + 1].GetComponent<AreaController>();
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
                    areaController = MapManager._areas[_enemyMove._pointX, _enemyMove._pointZ].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    _enemyMove.transform.position = new Vector3(_enemyMove.transform.position.x, _enemyMove.transform.position.y, MapManager._areas[_enemyMove._pointX, _enemyMove._pointZ + 1].transform.position.z);
                    _enemyMove._pointZ = _enemyMove._pointZ + 1;
                }
            }
            else
            {
                areaController = MapManager._areas[_enemyMove._pointX, _enemyMove._pointZ - 1].GetComponent<AreaController>();
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
                    areaController = MapManager._areas[_enemyMove._pointX, _enemyMove._pointX].GetComponent<AreaController>();
                    areaController._onEnemy = false;
                    _enemyMove.transform.position = new Vector3(_enemyMove.transform.position.x, _enemyMove.transform.position.y, MapManager._areas[_enemyMove._pointX, _enemyMove._pointZ - 1].transform.position.z);
                    _enemyMove._pointZ = _enemyMove._pointZ - 1;
                }
            }
        }
    }
}
