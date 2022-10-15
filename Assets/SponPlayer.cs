using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponPlayer : MonoBehaviour
{
    int _randomNumX;
    int _randomNumZ;
    bool _spon;

    [SerializeField] GameObject _player;

    /// <summary>�ړ���E�O���m�F�A�ύX���邽�߂�AreaController�X�N���v�g���l������ϐ�</summary>
    AreaController areaController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spon()
    {
        while(_spon == false)
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
                    Debug.Log("�v���C���[�͐������ꂽ");
                    Instantiate(_player, new Vector3(MapManager._areas[_randomNumX, _randomNumZ].transform.position.x, 2, MapManager._areas[_randomNumX, _randomNumZ].transform.position.z), Quaternion.identity);
                    _spon = true;
                }
            }
        }
    }

}
