using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairPointCreate : MonoBehaviour
{
    /// <summary>配列の一つ目の要素の場所をランダムで見るための変数</summary>
    int _randomNumX;

    /// <summary>配列の二つ目の要素の場所をランダムで見るための変数</summary>
    int _randomNumZ;

    /// <summary>スポーンできたかどうかのフラグ</summary>
    bool _spon;

    /// <summary>移動先・前を確認、変更するためのAreaControllerスクリプトを獲得する変数</summary>
    AreaController areaController;

    public void PointCreate(GameObject door)
    {
        if(door == null)
        {
            Debug.Log("ヌルです");
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
                Debug.Log("反応している");

                if (areaController._onWall == true)
                {
                    continue;
                }
                else
                {
                    //ここに階段の情報を持たせる
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
