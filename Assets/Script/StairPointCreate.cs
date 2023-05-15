using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairPointCreate : MonoBehaviour
{
    /// <summary>�z��̈�ڂ̗v�f�̏ꏊ�������_���Ō��邽�߂̕ϐ�</summary>
    int _randomNumX;

    /// <summary>�z��̓�ڂ̗v�f�̏ꏊ�������_���Ō��邽�߂̕ϐ�</summary>
    int _randomNumZ;

    /// <summary>�X�|�[���ł������ǂ����̃t���O</summary>
    bool _spon;

    /// <summary>�ړ���E�O���m�F�A�ύX���邽�߂�AreaController�X�N���v�g���l������ϐ�</summary>
    AreaController areaController;

    public void PointCreate(GameObject door)
    {
        if(door == null)
        {
            Debug.Log("�k���ł�");
        }

        _spon = false;

        while (_spon == false)
        {
            _randomNumX = Random.Range(0, MapManager._x);
            _randomNumZ = Random.Range(0, MapManager._z);

            if (MapManager._areas[_randomNumX, _randomNumZ] == null)
            {
                continue;
            }
            else if (MapManager._areas[_randomNumX, _randomNumZ] != null)
            {
                areaController = MapManager._areas[_randomNumX, _randomNumZ].GetComponent<AreaController>();
                Debug.Log("�������Ă���");

                if (areaController._onWall == true)
                {
                    continue;
                }
                else
                {
                    //�����ɊK�i�̏�����������
                    areaController.Stair(door);
                    _spon = true;
                }
            }
        }
    }

    public void OpenDoor(GameObject Stair)
    {
        areaController.OpenStair(Stair);
    }
}
