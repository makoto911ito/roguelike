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
    int _keepFrontSide = 0;

    /// <summary>中心（ｚ座標）</summary>
    int _center = 0;

    /// <summary>各エリアの左端</summary>
    int _keepSide = 0;

    /// <summary>エリアの大きさ</summary>
    int _randomNum = 0;

    int _count = 0;

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
                Debug.Log(_keepMinAreaSize + "前回の最大の幅");
                Debug.Log(keepMaxArea + "　今回の最大の幅");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "　最後の中心点");
            }
            else // 間の区画
            {
                keepMaxArea += _areaSize;
                Debug.Log(_keepMinAreaSize + "前回の最大の幅");
                Debug.Log(keepMaxArea + "　今回の最大の幅");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "　今回の中心点");
            }

            _randomNum = Random.Range(_mapMin, _mapMax);　//部屋の大きさを決めている

            _AreaPointz = Random.Range(1, _z - 1) - _randomNum;　// Z座標をランダムで決める


            //エリアを作るfor文
            for (int x = _randomPosX - _randomNum; x < _randomPosX + _randomNum; x++)
            {
                for (int z = _AreaPointz - _randomNum; z < _AreaPointz + _randomNum; z++)
                {
                    if (x > 0 || z > 0 && x > _x || z > _z) // 一番端は外枠になるため
                    {
                        //マップ自体をくっつけないように最大幅から一マス離したところに生成させる
                        if (x >= _keepMinAreaSize + 2)
                        {
                            if (x <= keepMaxArea - 2)
                            {
                                Instantiate(_obj[(int)obj.walk], new Vector3(x, 0, z), Quaternion.identity);
                                _keepBackSide = x;
                                _count++;
                            }

                            if (_count == 1)
                            {
                                _keepFrontSide = x;
                            }
                        }
                    }
                }
            }

            _count = 0;
            Debug.Log(_keepBackSide + "エリア最後尾の位置");

            //生成したエリアの最後尾のマスから今回の最大幅まで道をつなげる
            if(i != _areaNum - 1)
            {
                for (int miti = _keepBackSide; miti < keepMaxArea; miti++)
                {
                    Debug.Log("動いた");
                    Instantiate(_obj[(int)obj.walk], new Vector3(miti, 0, _AreaPointz), Quaternion.identity);
                }
            }

            //前回の最大幅から今回のエリアの一番最初に生成されたの列にところまで道を作る
            if (i > 0)
            {
                for (int aisle = _keepMinAreaSize; aisle < _keepFrontSide; aisle++)
                {
                    Instantiate(_obj[(int)obj.walk], new Vector3(aisle, 0, _AreaPointz), Quaternion.identity);
                }

                if(_AreaPointz < _center)
                {
                    for(int rodo = _AreaPointz; rodo <= _center; rodo++)
                    {
                        Instantiate(_obj[(int)obj.walk], new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                    }
                }
                else
                {
                    for(int rodo = _center; rodo <= _AreaPointz; rodo++)
                    {
                        Instantiate(_obj[(int)obj.walk], new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                    }
                }

            }

            _center = _AreaPointz; // 今回のZ座標を保存

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
