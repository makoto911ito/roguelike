using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponEnemy : MonoBehaviour
{
    /// <summary>�z��̈�ڂ̗v�f�̏ꏊ�������_���Ō��邽�߂̕ϐ�</summary>
    int _randomNumX;

    /// <summary>�z��̓�ڂ̗v�f�̏ꏊ�������_���Ō��邽�߂̕ϐ�</summary>
    int _randomNumZ;

    /// <summary>�X�|�[���ł������ǂ����̃t���O</summary>
    bool _spon;

    /// <summary>�v���C���[�̃I�u�W�F�N�g���擾</summary>
    [SerializeField] GameObject[] _enemy;

    /// <summary>�X�|�[��������G�̍��v��</summary>
    [SerializeField] int _sponEnemyNum;

    /// <summary>�X�|�[��������G�L�����������_���őI�Ԃ��߂̕ϐ�</summary>
    int _randoEnemyNum;

    /// <summary>�ړ���E�O���m�F�A�ύX���邽�߂�AreaController�X�N���v�g���l������ϐ�</summary>
    AreaController areaController;

    public void Spon()
    {
        for (var i = 0; i < _sponEnemyNum; i++)
        {
            _randoEnemyNum = Random.Range(0, _enemy.Length);
            _randomNumX = Random.Range(0, MapManager._x);
            _randomNumZ = Random.Range(0, MapManager._z);

            if (MapManager._areas[_randomNumX, _randomNumZ] == null)
            {
                i -= 1;
                continue;
            }
            else if (MapManager._areas[_randomNumX, _randomNumZ] != null)
            {
                areaController = MapManager._areas[_randomNumX, _randomNumZ].GetComponent<AreaController>();
                Debug.Log("�������Ă���");

                if (areaController._onEnemy == true || areaController._onWall == true || areaController._onPlayer == true)
                {
                    Debug.Log("�v���C���[�͐�������Ȃ������B");
                    i -= 1;
                    continue;
                }
                else
                {
                    Debug.Log("�G�L�������������ꂽ");
                    Instantiate(_enemy[_randoEnemyNum], new Vector3(MapManager._areas[_randomNumX, _randomNumZ].transform.position.x, 1.5f, MapManager._areas[_randomNumX, _randomNumZ].transform.position.z), Quaternion.identity);

                }
            }
        }

    }

}
