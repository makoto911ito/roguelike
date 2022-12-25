using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeB : MonoBehaviour
{
    /// <summary>�Ԋu�̃J�E���g</summary>
    [SerializeField] int _count;
    /// <summary>�����̐؂�ւ�</summary>
    [SerializeField] bool _change;
    /// <summary>�X�|�[�������w���W���擾���邽�߂̕ϐ�</summary>
    public int _pointX;
    /// <summary>�X�|�[�������y���W���擾���邽�߂̕ϐ�</summary>
    public int _pointZ;

    int _playePointX;
    int _playePointZ;

    [SerializeField] EnemyMove _eMove;

    /// <summary>�X�e�[�W�̏����Ǘ����Ă���X�N���v�g���Q�Ƃ��邽�߂̕ϐ�</summary>
    AreaController areaController;

    public void MoveB()
    {
        int pointx = _pointX - 2;
        int pointz = _pointZ - 2;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (pointx + i == _pointX && pointz + j == _pointZ)
                {
                    return;
                }
                else
                {
                    areaController = MapManager._areas[pointx + i, pointz + j].GetComponent<AreaController>();
                    if (areaController._onPlayer == true)
                    {
                        _playePointX = pointx;
                        _playePointZ = pointz;
                    }
                }
            }
        }

        
        int pointXdistance;
        int pointZdistance;

        //false����-1�Etrue����+1
        bool x;
        bool y;

        //�ǂ����̎��̂ق����v���C���[�ɋ߂������肷�邽�߂ɍ������߂Ă���
        if (_playePointX < 0 && _pointX < 0 || _playePointX > 0 && _pointX > 0)
        {
            if(_playePointX < _pointX)
            {
                x = false;
            }
            else
            {
                x = true;
            }
            pointXdistance = _playePointZ - _pointX;
            if (_playePointZ < 0 && _pointZ < 0 || _playePointZ > 0 && _pointZ > 0)
            {
                pointZdistance = _playePointZ - _pointZ;

            }
            else
            {
                if (_playePointZ > 0)
                {
                    pointZdistance = _playePointZ - (_pointZ *= -1);
                }
                else
                {
                    pointZdistance = (_playePointZ *= -1) - _pointZ;
                }
            }
        }
        else
        {
            if(_playePointX > 0)
            {
                pointXdistance = _playePointX - (_pointX *= -1);
                if (_playePointZ < 0 && _pointZ < 0 || _playePointZ > 0 && _pointZ > 0)
                {
                    pointZdistance = _playePointZ - _pointZ;
                }
                else
                {
                    if (_playePointZ > 0)
                    {
                        pointZdistance = _playePointZ - (_pointZ *= -1);
                    }
                    else
                    {
                        pointZdistance = (_playePointZ *= -1) - _pointZ;
                    }
                }
            }
            else
            {
                pointXdistance = (_playePointX *= -1) - _pointX;
                if (_playePointZ < 0 && _pointZ < 0 || _playePointZ > 0 && _pointZ > 0)
                {
                    pointZdistance = _playePointZ - _pointZ;
                }
                else
                {
                    if (_playePointZ > 0)
                    {
                        pointZdistance = _playePointZ - (_pointZ *= -1);
                    }
                    else
                    {
                        pointZdistance = (_playePointZ *= -1) - _pointZ;
                    }
                }
            }
        }

        if (pointXdistance < 0)
        {
            pointXdistance *= -1;
        }

        if (pointZdistance < 0)
        {
            pointZdistance *= -1;
        }

        if (pointXdistance < pointZdistance)
        {
            //if()
            //{

            //}
            areaController = MapManager._areas[_pointX,_pointZ].GetComponent<AreaController>();
            //�ړ���̏��ɂ���čs�������߂�
            if (areaController._onWall == true)
            {
                //���΂Ɉړ����邩������Ȃ�
            }
            else if (areaController._onEnemy == true) { }
            else if (areaController._onPlayer == true)
            {
                //�U��������
                _eMove.Attack();
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
        else
        {
            areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
            //�ړ���̏��ɂ���čs�������߂�
            if (areaController._onWall == true)
            {
                //���΂Ɉړ����邩������Ȃ�
            }
            else if (areaController._onEnemy == true) { }
            else if (areaController._onPlayer == true)
            {
                //�U��������
                _eMove.Attack();
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
    }
}
