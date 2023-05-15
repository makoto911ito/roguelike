using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesutPlayerSpawner : MonoBehaviour
{
        /// <summary>配列の一つ目の要素の場所をランダムで見るための変数</summary>
    int _randomNumX;

    /// <summary>配列の二つ目の要素の場所をランダムで見るための変数</summary>
    int _randomNumZ;

    /// <summary>スポーンできたかどうかのフラグ</summary>
    bool _spon;

    /// <summary>プレイヤーのオブジェクトを取得</summary>
    [SerializeField] GameObject _player;

    /// <summary>移動先・前を確認、変更するためのAreaControllerスクリプトを獲得する変数</summary>
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
                Debug.Log("反応している");

                if (areaController._onEnemy == true || areaController._onWall == true)
                {
                    Debug.Log("プレイヤーは生成されなかった。");
                    continue;
                }
                else
                {
                    if (GameObject.Find("Player") == false)
                    {
                        Debug.Log("プレイヤーは生成された");
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
