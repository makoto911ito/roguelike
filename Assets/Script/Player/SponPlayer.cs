using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�𐶐����邽�߂̃X�N���v�g
/// </summary>
public class SponPlayer : MonoBehaviour
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

    //�v���C���[���X�|�[�������Ƃ�����Ȃ��Đ�������������Q�[���}�l�[�W���[���擾
    [SerializeField] GameManager _gameManager;

    public void Spon()
    {
        _spon = false;

        Debug.Log(MapManager._areas.GetLength(0) + " " + MapManager._areas.GetLength(0));

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
                        var _playerObj = Instantiate(_player, new Vector3(MapManager._areas[_randomNumX, _randomNumZ].transform.position.x, 1.5f, MapManager._areas[_randomNumX, _randomNumZ].transform.position.z), Quaternion.identity);
                        _playerObj.name = "Player";
                        _spon = true;
                    }
                    else
                    {
                        var _player = GameObject.Find("Player");
                        _player.transform.position = new Vector3(MapManager._areas[_randomNumX, _randomNumZ].transform.position.x, 1.5f, MapManager._areas[_randomNumX, _randomNumZ].transform.position.z);
                        _spon = true;
                    }
                    _gameManager.LoadMap(false);
                    _gameManager.GoSound();
                }
            }
        }
    }

}
