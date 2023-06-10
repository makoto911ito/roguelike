using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを生成するためのスクリプト
/// </summary>
public class SponPlayer : MonoBehaviour
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

    //プレイヤーがスポーンしたときから曲を再生させたいからゲームマネージャーを取得
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
