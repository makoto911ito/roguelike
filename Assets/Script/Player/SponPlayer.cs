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
                Debug.Log("反応している");

                if (areaController._onEnemy == true || areaController._onWall == true)
                {
                    Debug.Log("プレイヤーは生成されなかった。");
                    continue;
                }
                else
                {
                    Debug.Log("プレイヤーは生成された");
                    Instantiate(_player, new Vector3(MapManager._areas[_randomNumX, _randomNumZ].transform.position.x, 1.5f, MapManager._areas[_randomNumX, _randomNumZ].transform.position.z), Quaternion.identity);
                    _spon = true;
                }
            }
        }
    }

}
