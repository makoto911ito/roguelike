using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum obj
{
    walk = 0,
    obj2 = 1,
    obj3 = 2,
}

public class MapManager : MonoBehaviour
{
    /// <summary>横幅</summary>
    [SerializeField] private int _x = 50;
    /// <summary>縦幅</summary>
    [SerializeField] private int _z = 50;
    /// <summary>エリアの数</summary>
    [SerializeField] int _areaNum = 4;

    //部屋の大きさの決めるための範囲
    /// <summary>エリア大きさの最小値</summary>
    [SerializeField] int _mapMin = 3;
    /// <summary>エリア大きさの最大値</summary>
    [SerializeField] int _mapMax = 7;

    //床や壁などのオブジェクト
    public GameObject[] _obj;

    //分割したエリアで精製されるマップの中心（z座標）
    int _AreaPointz = 0;

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

    int _keepBackSide = 0;

    /// <summary>中心（ｚ座標）</summary>
    int _center = 0;

    /// <summary>各エリアの左端</summary>
    int _keepSide = 0;

    /// <summary>エリアの大きさ</summary>
    int _randomNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        _areaSize = _x / _areaNum; //分割する大きさを決める

        for (int i = 0; i < _areaNum; i++)　//マップの生成
        {
            _keepMinAreaSize = keepMaxArea; //前回の最大幅を保存する

            if (i == 0) // 最初の区画
            {
                keepMaxArea = _areaSize;
                _randomPosX = Random.Range(1, keepMaxArea);
                //Debug.Log(_randomPosX + " 最初の中心点");
            }
            else if (i == _areaNum - 1) //最後の区画の場合
            {
                keepMaxArea = _x - 1;
                //Debug.Log(keepMaxArea + "　今回の最大の幅");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "　最後の中心点");
            }
            else // 間の区画
            {
                keepMaxArea += _areaSize;
                //Debug.Log(keepMaxArea + "　今回の最大の幅");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "　今回の中心点");
            }

            _randomNum = Random.Range(_mapMin, _mapMax);　//部屋の大きさを決めている

            _AreaPointz = Random.Range(1, _z - 1) - _randomNum;　// Z座標をランダムで決める

            //Debug.Log(count + " 区切った回数");

            _keepSide = _randomPosX - _randomNum; // 各エリアの左端を計算


            //エリアを作るfor文
            for (int x = _randomPosX - _randomNum; x < _randomPosX + _randomNum; x++)
            {
                for (int z = _AreaPointz - _randomNum; z < _AreaPointz + _randomNum; z++)
                {

                    if (x > 0 || z > 0 && x > _x || z > _z) // 一番端は外枠になるため
                    {
                        //マップ自体をくっつけないように最大幅から一マス離したところに生成させる
                        if (x > _keepMinAreaSize + 1)
                        {
                            if (x < keepMaxArea - 1)
                            {
                                Instantiate(_obj[(int)obj.walk], new Vector3(x, 0, z), Quaternion.identity);

                                if (x == keepMaxArea - 2)
                                {
                                    _keepBackSide = x;
                                    Debug.Log(_keepBackSide);
                                }
                            }
                        }
                    }
                }
            }

            _center = _AreaPointz; // 今回のZ座標を保存

            for (int aisle = _keepMinAreaSize; aisle < _keepSide; aisle++)
            {
                Instantiate(_obj[(int)obj.walk], new Vector3(aisle, 0, _center), Quaternion.identity);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
