using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponEnemy : MonoBehaviour
{
    /// <summary>配列の一つ目の要素の場所をランダムで見るための変数</summary>
    int _randomNumX;

    /// <summary>配列の二つ目の要素の場所をランダムで見るための変数</summary>
    int _randomNumZ;

    /// <summary>敵のオブジェクトを配列に保存するための変数</summary>
    [SerializeField] GameObject[] _enemys;

    /// <summary>スポーンさせる敵の合計数</summary>
    [SerializeField] int _sponEnemyNum;

    /// <summary>スポーンさせる敵キャラをランダムで選ぶための変数</summary>
    int _randoEnemyNum;

    /// <summary>エリア情報を取得するためのAreaControllerスクリプトを獲得する変数</summary>
    AreaController areaController;

    /// <summary>ゲームマネージャーを取得するための変数</summary>
    [SerializeField] EnemyManager _enemyManager = null;

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
                Debug.Log("反応している");

                if (areaController._onEnemy == true || areaController._onWall == true || areaController._onPlayer == true)
                {
                    i -= 1;
                    continue;
                }
                else
                {
                    Debug.Log("敵キャラが生成された");
                    GameObject _enemy = Instantiate(_enemys[_randoEnemyNum], new Vector3(MapManager._areas[_randomNumX, _randomNumZ].transform.position.x, 1.5f, MapManager._areas[_randomNumX, _randomNumZ].transform.position.z), Quaternion.identity);
                    _enemyManager.Enemy(_enemy);

                }
            }
        }

    }

}
