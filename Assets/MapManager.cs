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
    int _areaUnderPointZ = 0;

    int _areaUpPointZ = 0;

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

    /// <summary>�������ꂽ�G���A�̈�Ԍ��̃}�X�̂����W</summary>
    int _keepBackSide = 0;
    /// <summary>�������ꂽ�G���A�̈�ԍŏ��̃}�X�̂����W</summary>
    int _keepFrontSide = 0;

    /// <summary>���������G���A�̒��S�i�����W�j</summary>
    int _center = 0;

    /// <summary>�e�G���A�̍��[</summary>
    int _keepSide = 0;

    /// <summary>�G���A�̑傫��</summary>
    int _randomNum = 0;

    /// <summary>��ԍŏ��ɐ������ꂽ�}�X�̂����W���Ƃ邽�߂̃J�E���g</summary>
    int _count = 0;

    int _zLine = 0;

    int _randomJudgeNum = 0;// �ǂ���̕����ɐ������邩

    [SerializeField] int _minZLine = 10;

    // Start is called before the first frame update
    void Start()
    {
        _areaSize = _x / _areaNum; //��������傫�������߂�

        _zLine = Random.Range(_minZLine, _z);// ���̋�؂�̈ʒu�����߂�
        Debug.Log(_zLine + " ����؂�̒l");

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

            // Z���W�������_���Ō��߂�
            _areaUnderPointZ = Random.Range(1, _zLine) - _randomNum;// z���W�P�̏�����z���W�̋�؂����ꏊ�܂ł̊ԂŌ��߂�
            _areaUpPointZ = Random.Range(_zLine, _z - 1) - _randomNum;// z���W�̋�؂����ꏊ����z���W�̍ő�l�܂ł̊ԂŌ��߂�

            _randomJudgeNum = Random.Range(0, 2);

            //�G���A�����for��
            for (int x = _randomPosX - _randomNum; x < _randomPosX + _randomNum; x++)
            {
                if (_randomJudgeNum == 0)
                {
                    for (int z = _areaUpPointZ - _randomNum; z < _areaUpPointZ + _randomNum; z++)
                    {
                        if (x > 0 && x < _x) // ��Ԓ[�͊O�g�ɂȂ邽��
                        {
                            //�}�b�v���̂��������Ȃ��悤�ɍő啝�����}�X�������Ƃ���ɐ���������
                            if (x >= _keepMinAreaSize + 2)
                            {
                                if (x <= keepMaxArea - 2)
                                {
                                    if (z < _z)
                                    {
                                        if (z > _zLine)
                                        {
                                            Instantiate(_obj[(int)obj.walk], new Vector3(x, 0, z), Quaternion.identity);
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
                    }
                }
                else
                {
                    for (int z = _areaUnderPointZ - _randomNum; z < _areaUnderPointZ + _randomNum; z++)
                    {
                        if (x > 0 && x < _x) // ��Ԓ[�͊O�g�ɂȂ邽��
                        {
                            //�}�b�v���̂��������Ȃ��悤�ɍő啝�����}�X�������Ƃ���ɐ���������
                            if (x >= _keepMinAreaSize + 2)
                            {
                                if (x <= keepMaxArea - 2)
                                {
                                    if (z > 0)
                                    {
                                        if (z < _zLine)
                                        {
                                            Instantiate(_obj[(int)obj.walk], new Vector3(x, 0, z), Quaternion.identity);
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
                    }
                }

            }

            _count = 0;//�����ŃJ�E���g�����Z�b�g
            Debug.Log(_keepBackSide + "�G���A�Ō���̈ʒu");

            ////���������G���A�̍Ō���̃}�X���獡��̍ő啝�܂œ����Ȃ���
            //if (i != _areaNum - 1)
            //{
            //    for (int miti = _keepBackSide; miti < keepMaxArea; miti++)
            //    {
            //        Debug.Log("������");
            //        Instantiate(_obj[(int)obj.walk], new Vector3(miti, 0, _areaUnderPointZ), Quaternion.identity);
            //    }
            //}

            ////�O��̍ő啝���獡��̃G���A�̈�ԍŏ��ɐ������ꂽ�̗�ɂƂ���܂œ������
            //if (i > 0)
            //{
            //    for (int aisle = _keepMinAreaSize; aisle < _keepFrontSide; aisle++)
            //    {
            //        Instantiate(_obj[(int)obj.walk], new Vector3(aisle, 0, _areaUnderPointZ), Quaternion.identity);
            //    }

            //    //�����Ȃ��邽�߂̃R�[�h
            //    if (_areaUnderPointZ < _center)
            //    {
            //        for (int rodo = _areaUnderPointZ; rodo <= _center; rodo++)
            //        {
            //            Instantiate(_obj[(int)obj.walk], new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
            //        }
            //    }
            //    else
            //    {
            //        for (int rodo = _center; rodo <= _areaUnderPointZ; rodo++)
            //        {
            //            Instantiate(_obj[(int)obj.walk], new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
            //        }
            //    }

            //}

            _center = _areaUnderPointZ; // �����Z���W��ۑ�

        }

    }
}
