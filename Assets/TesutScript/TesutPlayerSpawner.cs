using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesutPlayerSpawner : MonoBehaviour
{
        /// <summary>�z��̈�ڂ̗v�f�̏ꏊ�������_���Ō��邽�߂̕ϐ�</summary>
    int _randomNumX;

    /// <summary>�z��̓�ڂ̗v�f�̏ꏊ�������_���Ō��邽�߂̕ϐ�</summary>
    int _randomNumZ;

    /// <summary>�X�|�[���ł������ǂ����̃t���O</summary>
    bool _spon;

    /// <summary>�v���C���[�̃I�u�W�F�N�g���擾</summary>
    [SerializeField] GameObject _player;

    /// <summary>�ړ���E�O���m�F�A�ύX���邽�߂�AreaController�X�N���v�g���l������ϐ�</summary>
    AreaController areaController;

    public void Spon()
    {
        _spon = false;

        while (_spon == false)
        {
            _randomNumX = Random.Range(0, TesutMap._x);
            _randomNumZ = Random.Range(0, TesutMap._z);

            if (TesutMap._areas[_randomNumX, _randomNumZ] == null)
            {
                continue;
            }
            else if (TesutMap._areas[_randomNumX, _randomNumZ] != null)
            {
                areaController = TesutMap._areas[_randomNumX, _randomNumZ].GetComponent<AreaController>();
                Debug.Log("�������Ă���");

                if (areaController._onEnemy == true || areaController._onWall == true)
                {
                    Debug.Log("�v���C���[�͐�������Ȃ������B");
                    continue;
                }
                else
                {
                    if (GameObject.Find("Player") == false)
                    {
                        Debug.Log("�v���C���[�͐������ꂽ");
                        var _playerObj = Instantiate(_player, new Vector3(TesutMap._areas[_randomNumX, _randomNumZ].transform.position.x, 1.5f, TesutMap._areas[_randomNumX, _randomNumZ].transform.position.z), Quaternion.identity);
                        _playerObj.name = "Player";
                        _spon = true;
                    }
                    else
                    {
                        var _player = GameObject.Find("Player");
                        _player.transform.position = new Vector3(TesutMap._areas[_randomNumX, _randomNumZ].transform.position.x, 1.5f, TesutMap._areas[_randomNumX, _randomNumZ].transform.position.z);
                        _spon = true;
                    }

                }
            }
        }
    }
}
