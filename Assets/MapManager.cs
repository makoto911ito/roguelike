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
    /// <summary>����</summary>
    [SerializeField] private int _x = 50;
    /// <summary>����</summary>
    [SerializeField] private int _z = 50;
    /// <summary>�G���A�̐�</summary>
    [SerializeField] int _areaNum = 4;

    //�����̑傫���̌��߂邽�߂͈̔�
    [SerializeField] int _mapMin = 3;
    [SerializeField] int _mapMax = 7;

    public GameObject[] _obj;

    //���������G���A�Ő��������}�b�v��x,z���W
    int _areaPointx = 0;
    int _areaPointz = 0;

    /// <summary>��������G���A�̑傫���̍ő�l</summary>
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
                Debug.Log(keepMInArea + "keepMInArea�ł�");
                Debug.Log(_randomPosX + "�ł�");
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
                Debug.Log(keepMInArea + "keepMInArea�ł���");
                Debug.Log(_randomPosX + "�ł���");
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
