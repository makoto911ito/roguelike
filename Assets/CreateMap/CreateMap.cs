using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    //マップ全体の大きさを決める
    [SerializeField] static public int _x = 50;//マップ全体の横幅
    [SerializeField] static public int _z = 50;//マップ全体の縦幅
    [SerializeField] int _areaNum = 4;//作るエリアの数

    //部屋の大きさの決めるための範囲
    [SerializeField] int _mapSizeMin = 3;//生成する一番小さい部屋のサイズ
    [SerializeField] int _mapSizeMax = 7;//生成する一番でかい部屋のサイズ

    [SerializeField] GameObject _obj;//生成するオブジェクト

    int keepMaxArea = 1;//今のx座標の最大値
    int _keepMinAreaSize = 1;//前回のx座標の最大値

    int _areaSize = 0; //マップを区切るサイズ
    int _randomPosX = 0;//区切った区画で生成する部屋の横幅の中心点
    int _randomPosZ = 0;//区切った区画で生成する部屋の縦幅の中心点
    int _randomMapSize = 0;//生成する部屋の大きさ
    int _count = 0;// 何回目のオブジェクト生成か管理する変数
    int _keepBackSide = 0;//生成した部屋の一番最後にあるオブジェクトのx座標を管理する変数
    int _keepFrontSide = 0;//生成した部屋の一番最初にあるオブジェクトのx座標を管理する変数
    int _keepPosZ = 0;//生成した部屋のZ座標を管理する変数

    // Start is called before the first frame update
    void Start()
    {
        _areaSize = _x / _areaNum; //分割する大きさを決める

        for (int i = 0; i < _areaNum; i++) //マップの生成
        {
            _keepMinAreaSize = keepMaxArea; //前回の最大幅を保存する
            if (i == 0) // 最初の区画
            {
                keepMaxArea = _areaSize;
                _randomPosX = Random.Range(1, keepMaxArea);//区切った幅の中から横幅の中心を決める
            }
            else if (i == _areaNum - 1) //最後の区画の場合
            {
                keepMaxArea = _x - 1;
                Debug.Log(_keepMinAreaSize + "前回の最大の幅");
                Debug.Log(keepMaxArea + "　今回の最大の幅");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
            }
            else // 間の区画
            {
                keepMaxArea += _areaSize;
                Debug.Log(_keepMinAreaSize + "前回の最大の幅");
                Debug.Log(keepMaxArea + "　今回の最大の幅");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
            }

            _randomPosZ = Random.Range(1, _z);//縦幅の中心を決めている

            _randomMapSize = Random.Range(_mapSizeMin, _mapSizeMax); //部屋の大きさを決めている

            for (int x = _randomPosX - _randomMapSize; x < _randomPosX + _randomMapSize; x++)
            {
                for (int z = _randomPosZ - _randomMapSize; z < _randomPosZ + _randomMapSize; z++)
                {
                    if (x > 0 && x < keepMaxArea - 1)
                    {
                        if (x > _keepMinAreaSize + 1)
                        {
                            if (z > 0 && z < _z)
                            {
                                Instantiate(_obj, new Vector3(x, 0, z), Quaternion.identity);
                                _keepBackSide = x;
                                _count++;
                            }
                        }
                    }

                    if (_count == 1)
                    {
                        _keepFrontSide = x;
                    }
                }
            }
            _count = 0;

            //生成したエリアの最後尾のマスから今回の最大幅まで道をつなげる
            if (i != _areaNum - 1)
            {
                for (int miti = _keepBackSide + 1; miti <= keepMaxArea; miti++)
                {
                    Instantiate(_obj, new Vector3(miti, 0, _randomPosZ), Quaternion.identity);
                }
            }

            if (i > 0)
            {
                Debug.Log("ufoiteru");
                Debug.Log(_keepFrontSide);
                for (int aisle = _keepFrontSide; aisle >= _keepMinAreaSize; aisle--)
                {
                    Instantiate(_obj, new Vector3(aisle, 0, _randomPosZ), Quaternion.identity);
                }


                if (_randomPosZ > _keepPosZ)
                {
                    //道をつなげるためのコード
                    for (int rodo = _keepPosZ + 1; rodo < _randomPosZ; rodo++)
                    {
                        Instantiate(_obj, new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                    }
                }
                else
                {
                    //道をつなげるためのコード
                    for (int rodo = _randomPosZ + 1; rodo < _keepPosZ; rodo++)
                    {
                        Instantiate(_obj, new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                    }
                }

            }

            //今回のZ座標を保存
            _keepPosZ = _randomPosZ;
        }
    }
}
