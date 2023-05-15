using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TesutMap : MonoBehaviour
{
    /// <summary>横幅</summary>
    [SerializeField] static public int _x = 50;
    /// <summary>縦幅</summary>
    [SerializeField] static public int _z = 50;
    /// <summary>エリアの数</summary>
    [SerializeField] int _areaNum = 4;

    static public GameObject[,] _areas;

    //部屋の大きさの決めるための範囲
    /// <summary>エリア大きさの最小値</summary>
    [SerializeField] int _mapMin = 3;
    /// <summary>エリア大きさの最大値</summary>
    [SerializeField] int _mapMax = 7;

    //床や壁などのオブジェクト
    public GameObject[] _obj;

    //分割したエリアで精製されるマップの中心（z座標）
    int _areaUnderPointZ = 0;

    /// <summary>分割したエリアの上側で生成されるマップの中心（z座標） </summary>
    int _areaUpPointZ = 0;

    /// <summary>分割するエリアの大きさの最大値</summary>
    int _areaSize = 0;

    //分けたエリアの内でランダムにx座標を取得
    /// <summary>各区分の中心となるx座標</summary>
    int _randomPosX;

    /// <summary>今のx座標の最大値</summary>
    int keepMaxArea = 1;
    //
    /// <summary>前回のx座標の最大値</summary>
    int _keepMinAreaSize = 1;

    /// <summary>生成されたエリアの一番後ろのマスのｘ座標</summary>
    int _keepBackSide = 0;
    /// <summary>生成されたエリアの一番最初のマスのｘ座標</summary>
    int _keepFrontSide = 0;

    /// <summary>生成したエリアの中心（ｚ座標）</summary>
    int _center = 0;

    /// <summary>エリアの大きさ</summary>
    int _randomNum = 0;

    /// <summary>一番最初に生成されたマスのｘ座標をとるためのカウント</summary>
    int _count = 0;

    /// <summary>横方向の区切り位置</summary>
    int _zLine = 0;

    /// <summary>どちらの方向に生成するか判定するための変数</summary>
    int _randomJudgeNum = 0;

    /// <summary>指定エリアを横方向に区切るための範囲の最低値</summary>
    [SerializeField] int _minZLine = 10;

    /// <summary>プレイヤーを生成するためのスクリプトを参照するための変数</summary>
    [SerializeField] TesutPlayerSpawner _sponPlayer;

    /// <summary>敵を生成するためのスクリプトを参照するための変数</summary>
    [SerializeField] SponEnemy _sponEnemy;

    AreaController areaController;

    [SerializeField] public GameObject _mapController;

    private void Start()
    {
        BossMapCreate(_mapController);
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(1);
        //プレイヤーをスポーンさせる
        _sponPlayer.Spon();
        //敵キャラをスポーンさせる
        _sponEnemy.Spon();
        //ボス敵をスポーンさせる
        _sponEnemy.BossSpon();
    }



    static public int _bossMapX = 40;
    static public int _bossMapZ = 40;

    public void BossMapCreate(GameObject _mapController)
    {
        _areas = new GameObject[_bossMapX + 1, _bossMapZ + 1];

        for (var x = 1; x < 16; x++)
        {
            for (var z = 1; z < _bossMapZ; z++)
            {
                if (x >= 12)
                {
                    if (z > 17 && z < 23)
                    {
                        _areas[x, z] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(x, 0, z), Quaternion.identity);
                        _areas[x, z].transform.parent = _mapController.transform;
                    }
                }
                else
                {
                    _areas[x, z] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(x, 0, z), Quaternion.identity);
                    _areas[x, z].transform.parent = _mapController.transform;
                }

            }
        }

        for (var x = 16; x < _bossMapX; x++)
        {
            for (var z = 1; z < _bossMapZ; z++)
            {
                _areas[x, z] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(x, 0, z), Quaternion.identity);
                _areas[x, z].transform.parent = _mapController.transform;
            }
        }

        StartCoroutine("Spawner");
    }
}
