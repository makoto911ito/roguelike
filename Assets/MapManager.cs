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

    //���������G���A�Ő��������}�b�v�̒��S�iz���W�j
    int _AreaPointz = 0;

    /// <summary>��������G���A�̑傫���̍ő�l</summary>
    int _areaSize = 0;

    int _randomPosX;
    int keepMaxArea = 1;

    int _keepMinAreaSize = 1;

    int _keepBackSide = 0;

    int _center = 0;

    int _keepSide = 0;

    // Start is called before the first frame update
    void Start()
    {
        _areaSize = _x / _areaNum; //��������傫�������߂�

        for (int i = 0; i < _areaNum; i++)
        {
            _keepMinAreaSize = keepMaxArea; //�O��̍ő啝��ۑ�����

            if (i == 0) // �ŏ��̋��
            {
                keepMaxArea = _areaSize;
                _randomPosX = Random.Range(1, keepMaxArea);
                //Debug.Log(_randomPosX + " �ŏ��̒��S�_");
            }
            else if (i == _areaNum - 1) //�Ō�̋��̏ꍇ
            {
                keepMaxArea = _x - 1;
                //Debug.Log(keepMaxArea + "�@����̍ő�̕�");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "�@�Ō�̒��S�_");
            }
            else // �Ԃ̋��
            {
                keepMaxArea += _areaSize;
                //Debug.Log(keepMaxArea + "�@����̍ő�̕�");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "�@����̒��S�_");
            }

            int _randomNum = Random.Range(_mapMin, _mapMax); // Z���W�������_���Ō��߂�

            _AreaPointz = Random.Range(1, _z - 1) - _randomNum;

            //Debug.Log(count + " ��؂�����");

            _keepSide = _randomPosX - _randomNum; // �e�G���A�̍��[


            //�G���A�����for��
            for (int x = _randomPosX - _randomNum; x < _randomPosX + _randomNum; x++)
            {
                for (int z = _AreaPointz - _randomNum; z < _AreaPointz + _randomNum; z++)
                {

                    if (x > 0 || z > 0 && x > _x || z > _z) // ��Ԓ[�͊O�g�ɂȂ邽��
                    {
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

            _center = _AreaPointz; // �����Z���W��ۑ�


            if (i == 0 )
            {
                //Debug.Log("��������");
                //for (int backSide = _keepBackSide; backSide < _keepMinAreaSize; backSide++)
                //{
                //    Instantiate(_obj[(int)obj.walk], new Vector3(backSide, 0, _center), Quaternion.identity);

                //}
            }
            else if(i != 0)
            {
                for( int aisle = _keepMinAreaSize; aisle < _keepSide; aisle++)
                {
                    Instantiate(_obj[(int)obj.walk], new Vector3(aisle, 0, _center), Quaternion.identity);
                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
