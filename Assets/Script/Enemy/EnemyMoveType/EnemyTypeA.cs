using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyTypeA : X
{
    /// <summary></summary>
    private int _count;
    /// <summary></summary>
    private bool _change;

    private readonly EnemyMove _enemyMove;
    private readonly PlayerPresenter _playerPresenter;

    public EnemyTypeA(EnemyMove enemyMove, PlayerPresenter playerPresenter)
    {
        _enemyMove = enemyMove;
        _playerPresenter = playerPresenter;
    }

    public override void Move()
    {
        var px = _enemyMove._pointX;
        var pz = _enemyMove._pointZ;
        if (_change == false && _count == 0 || _change == true && _count != 0)//���]��false����count��0��������or���]���Ă��Ċ���count��0����Ȃ�����
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
                _playerPresenter.Damage(1);
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
        else if (_change == false && _count != 0 || _change == true && _count == 0) //���]��false����count��0����Ȃ�������or���]���Ă��Ċ���count��0��������
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
                _playerPresenter.Damage(1);
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

        _count++;

        if (_count == 2)
        {
            _count = 0;
            if (_change == false)
            {
                _change = true;
            }
            else
            {
                _change = false;
            }
        }

    }
}
