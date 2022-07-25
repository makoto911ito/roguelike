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
    /// <summary>�c��</summary>
    [SerializeField] private int _z = 50;
    /// <summary>�G���A�̐�</summary>
    [SerializeField] int _areaNum = 4;

    //�����̑傫���̌��߂邽�߂͈̔�
    /// <summary>�G���A�傫���̍ŏ��l</summary>
    [SerializeField] int _mapMin = 3;
    /// <summary>�G���A�傫���̍ő�l</summary>
    [SerializeField] int _mapMax = 7;

    //����ǂȂǂ̃I�u�W�F�N�g
    public GameObject[] _obj;

    //���������G���A�Ő��������}�b�v�̒��S�iz���W�j
    int _AreaPointz = 0;

    /// <summary>��������G���A�̑傫���̍ő�l</summary>
    int _areaSize = 0;

    //�������G���A�̓��Ń����_����x���W���擾
    /// <summary>�e�敪�̒��S�ƂȂ�x���W</summary>
    int _randomPosX;

    /// <summary>����x���W�̍ő�l</summary>
    int keepMaxArea = 1;
    //
    /// <summary>�O���x���W�̍ő�l</summary>
    int _keepMinAreaSize = 1;

    int _keepBackSide = 0;
    int _keepFrontSide = 0;

    /// <summary>���S�i�����W�j</summary>
    int _center = 0;

    /// <summary>�e�G���A�̍��[</summary>
    int _keepSide = 0;

    /// <summary>�G���A�̑傫��</summary>
    int _randomNum = 0;

    int _count = 0;

    // Start is called before the first frame update
    void Start()
    {
        _areaSize = _x / _areaNum; //��������傫�������߂�

        for (int i = 0; i < _areaNum; i++)�@//�}�b�v�̐���
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
                Debug.Log(_keepMinAreaSize + "�O��̍ő�̕�");
                Debug.Log(keepMaxArea + "�@����̍ő�̕�");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "�@�Ō�̒��S�_");
            }
            else // �Ԃ̋��
            {
                keepMaxArea += _areaSize;
                Debug.Log(_keepMinAreaSize + "�O��̍ő�̕�");
                Debug.Log(keepMaxArea + "�@����̍ő�̕�");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "�@����̒��S�_");
            }

            _randomNum = Random.Range(_mapMin, _mapMax);�@//�����̑傫�������߂Ă���

            _AreaPointz = Random.Range(1, _z - 1) - _randomNum;�@// Z���W�������_���Ō��߂�


            //�G���A�����for��
            for (int x = _randomPosX - _randomNum; x < _randomPosX + _randomNum; x++)
            {
                for (int z = _AreaPointz - _randomNum; z < _AreaPointz + _randomNum; z++)
                {
                    if (x > 0 || z > 0 && x > _x || z > _z) // ��Ԓ[�͊O�g�ɂȂ邽��
                    {
                        //�}�b�v���̂��������Ȃ��悤�ɍő啝�����}�X�������Ƃ���ɐ���������
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
            Debug.Log(_keepBackSide + "�G���A�Ō���̈ʒu");

            //���������G���A�̍Ō���̃}�X���獡��̍ő啝�܂œ����Ȃ���
            if(i != _areaNum - 1)
            {
                for (int miti = _keepBackSide; miti < keepMaxArea; miti++)
                {
                    Debug.Log("������");
                    Instantiate(_obj[(int)obj.walk], new Vector3(miti, 0, _AreaPointz), Quaternion.identity);
                }
            }

            //�O��̍ő啝���獡��̃G���A�̈�ԍŏ��ɐ������ꂽ�̗�ɂƂ���܂œ������
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

            _center = _AreaPointz; // �����Z���W��ۑ�

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
