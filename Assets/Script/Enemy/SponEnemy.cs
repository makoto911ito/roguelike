using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponEnemy : MonoBehaviour
{
    /// <summary>�z��̈�ڂ̗v�f�̏ꏊ�������_���Ō��邽�߂̕ϐ�</summary>
    int _randomNumX;

    /// <summary>�z��̓�ڂ̗v�f�̏ꏊ�������_���Ō��邽�߂̕ϐ�</summary>
    int _randomNumZ;

    /// <summary>�G�̃I�u�W�F�N�g��z��ɕۑ����邽�߂̕ϐ�</summary>
    [SerializeField] GameObject[] _enemys;

    /// <summary>�X�|�[��������G�̍��v��</summary>
    [SerializeField] int _sponEnemyNum;

    /// <summary>�X�|�[��������G�L�����������_���őI�Ԃ��߂̕ϐ�</summary>
    int _randoEnemyNum;

    /// <summary>�G���A�����擾���邽�߂�AreaController�X�N���v�g���l������ϐ�</summary>
    AreaController areaController;

    /// <summary>�Q�[���}�l�[�W���[���擾���邽�߂̕ϐ�</summary>
    [SerializeField] EnemyList _enemyList = null;

    public void Spon()
    {
        for (var i = 0; i < _sponEnemyNum; i++)
        {
            _randoEnemyNum = Random.Range(0, _enemys.Length);
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
                    i -= 1;
                    continue;
                }
                else
                {
                    Debug.Log("�G�L�������������ꂽ");
                    GameObject _enemy = Instantiate(_enemys[_randoEnemyNum], new Vector3(MapManager._areas[_randomNumX, _randomNumZ].transform.position.x, 1.5f, MapManager._areas[_randomNumX, _randomNumZ].transform.position.z), Quaternion.identity);
                    _enemyList.Enemy(_enemy);

                }
            }
        }

    }

}