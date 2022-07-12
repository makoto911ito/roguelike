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
    /// <summary>立幅</summary>
    [SerializeField] private int _z = 50;
    /// <summary>エリアの数</summary>
    [SerializeField] int _areaNum = 4;

    //部屋の大きさの決めるための範囲
    [SerializeField] int _mapMin = 3;
    [SerializeField] int _mapMax = 7;

    public GameObject[] _obj;

    //分割したエリアで精製されるマップのx,z座標
    int _areaPointx = 0;
    int _areaPointz = 0;

    /// <summary>分割するエリアの大きさの最大値</summary>
    int _minArea = 0;

    int _randomPosX;
    int _randomPosz;
    int keepMInArea = 0;

    // Start is called before the first frame update
    void Start()
    {
        _minArea = _x / _areaNum;
        //Debug.Log(_minArea);

        //for(int i=0; i<_areaNum; i++)
        //{
        //    int _randomNum = Random.Range(_mapMin, _mapMax);

        //    _randomPosX = (Random.Range(0, _x)) - _randomNum;
        //    _randomPosz = (Random.Range(0, _z)) - _randomNum;

        //    for (int j = _randomPosX;j < _randomPosX + _randomNum;j++)
        //    {
        //        for (int k = _randomPosz; k < _randomPosz + _randomNum; k++)
        //        {
        //             Instantiate(_obj[(int)obj.walk], new Vector3(j, 0, k), Quaternion.identity);
        //        }
        //    }
        //}

        for (int i = 0; i < _areaNum; i++)
        {

            if (i == 0)
            {
                keepMInArea = _minArea;
                _randomPosX = Random.Range(1, keepMInArea);
                Debug.Log(_randomPosX);
            }
            else if (i == _areaNum - 1)
            {
                int keepRandomPosX = _randomPosX;
                keepMInArea = _x - 1;
                _randomPosX = Random.Range(keepRandomPosX + 1, keepMInArea);
                Debug.Log(keepMInArea + "keepMInAreaです");
                Debug.Log(_randomPosX + "です");
            }
            else
            {
                int keepRandomPosX = _randomPosX;
                if (keepRandomPosX !>= 12)
                {
                    keepMInArea += _minArea;
                }
                else
                {
                    keepMInArea += _minArea - keepRandomPosX;
                }
                _randomPosX = Random.Range(keepRandomPosX + 1, keepMInArea);
                Debug.Log(keepMInArea + "keepMInAreaですよ");
                Debug.Log(_randomPosX + "ですよ");
            }

            int _randomNum = Random.Range(_mapMin, _mapMax);

            _areaPointz = Random.Range(1, _z - 1) - _randomNum;


            for (int x = _randomPosX - _randomNum; x < _randomPosX + _randomNum; x++)
            {
                for (int z = _randomPosz - _randomNum; z < _randomPosz + _randomNum; z++)
                {
                    Instantiate(_obj[(int)obj.walk], new Vector3(x, 0, z), Quaternion.identity);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
