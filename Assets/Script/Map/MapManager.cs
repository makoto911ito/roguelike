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
    [SerializeField] static public int _x = 50;
    /// <summary>�c��</summary>
    [SerializeField] static public int _z = 50;
    /// <summary>�G���A�̐�</summary>
    [SerializeField] int _areaNum = 4;

    static public GameObject[,] _areas;

    //�����̑傫���̌��߂邽�߂͈̔�
    /// <summary>�G���A�傫���̍ŏ��l</summary>
    [SerializeField] int _mapMin = 3;
    /// <summary>�G���A�傫���̍ő�l</summary>
    [SerializeField] int _mapMax = 7;

    //����ǂȂǂ̃I�u�W�F�N�g
    public GameObject[] _obj;

    //���������G���A�Ő��������}�b�v�̒��S�iz���W�j
    int _areaUnderPointZ = 0;

    /// <summary>���������G���A�̏㑤�Ő��������}�b�v�̒��S�iz���W�j </summary>
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

    /// <summary>�G���A�̑傫��</summary>
    int _randomNum = 0;

    /// <summary>��ԍŏ��ɐ������ꂽ�}�X�̂����W���Ƃ邽�߂̃J�E���g</summary>
    int _count = 0;

    /// <summary>�������̋�؂�ʒu</summary>
    int _zLine = 0;

    /// <summary>�ǂ���̕����ɐ������邩���肷�邽�߂̕ϐ�</summary>
    int _randomJudgeNum = 0;

    /// <summary>�w��G���A���������ɋ�؂邽�߂͈̔͂̍Œ�l</summary>
    [SerializeField] int _minZLine = 10;

    /// <summary>�v���C���[�𐶐����邽�߂̃X�N���v�g���Q�Ƃ��邽�߂̕ϐ�</summary>
    [SerializeField] SponPlayer _sponPlayer;

    /// <summary>�G�𐶐����邽�߂̃X�N���v�g���Q�Ƃ��邽�߂̕ϐ�</summary>
    [SerializeField] SponEnemy _sponEnemy;

    AreaController areaController;


    public void MapCreate(GameObject _mapController)
    {
        _areas = new GameObject[_x, _z + 1];

        _areaSize = _x / _areaNum; //��������傫�������߂�

        _zLine = Random.Range(_minZLine, _z);// ���̋�؂�̈ʒu�����߂�
        //Debug.Log(_zLine + " ����؂�̒l");

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
                //Debug.Log(_keepMinAreaSize + "�O��̍ő�̕�");
                //Debug.Log(keepMaxArea + "�@����̍ő�̕�");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "�@�Ō�̒��S�_");
            }
            else // �Ԃ̋��
            {
                keepMaxArea += _areaSize;
                //Debug.Log(_keepMinAreaSize + "�O��̍ő�̕�");
                //Debug.Log(keepMaxArea + "�@����̍ő�̕�");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
                //Debug.Log(_randomPosX + "�@����̒��S�_");
            }

            _randomNum = Random.Range(_mapMin, _mapMax);�@//�����̑傫�������߂Ă���
            //Debug.Log("�G���A�̑傫��" + _randomNum);

            // Z���W�������_���Ō��߂�
            _areaUnderPointZ = Random.Range(1, _zLine - 1);// z���W�P�̏�����z���W�̋�؂����ꏊ�܂ł̊ԂŌ��߂�
            _areaUpPointZ = Random.Range(_zLine + 1, _z - 3);// z���W�̋�؂����ꏊ����z���W�̍ő�l�܂ł̊ԂŌ��߂�

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
                                            _areas[x, z] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(x, 0, z), Quaternion.identity);
                                            _areas[x, z].transform.parent = _mapController.transform;
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
                                            _areas[x, z] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(x, 0, z), Quaternion.identity);
                                            _areas[x, z].transform.parent = _mapController.transform;
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
            //Debug.Log(_keepBackSide + "�G���A�Ō���̈ʒu");

            //���������G���A�̍Ō���̃}�X���獡��̍ő啝�܂œ����Ȃ���
            if (i != _areaNum - 1)
            {
                if (_randomJudgeNum == 0)
                {
                    for (int miti = _keepBackSide + 1; miti <= keepMaxArea; miti++)
                    {
                        //Debug.Log("������");
                        _areas[miti, _areaUpPointZ] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(miti, 0, _areaUpPointZ), Quaternion.identity);
                        _areas[miti, _areaUpPointZ].transform.parent = _mapController.transform;
                    }
                }
                else
                {
                    for (int miti = _keepBackSide + 1; miti <= keepMaxArea; miti++)
                    {
                        //Debug.Log("������");
                        _areas[miti, _areaUnderPointZ] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(miti, 0, _areaUnderPointZ), Quaternion.identity);
                        _areas[miti, _areaUnderPointZ].transform.parent = _mapController.transform;
                    }
                }
            }

            //�O��̍ő啝���獡��̃G���A�̈�ԍŏ��ɐ������ꂽ�̗�ɂƂ���܂œ������
            if (i > 0)
            {
                if (_randomJudgeNum == 0)
                {
                    for (int aisle = _keepMinAreaSize; aisle < _keepFrontSide; aisle++)
                    {
                        _areas[aisle, _areaUpPointZ] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(aisle, 0, _areaUpPointZ), Quaternion.identity);
                        _areas[aisle, _areaUpPointZ].transform.parent = _mapController.transform;
                    }

                    //�����Ȃ��邽�߂̃R�[�h
                    if (_areaUpPointZ < _center)
                    {
                        for (int rodo = _areaUpPointZ + 1; rodo < _center; rodo++)
                        {
                            _areas[_keepMinAreaSize, rodo] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                            _areas[_keepMinAreaSize, rodo].transform.parent = _mapController.transform;
                        }
                    }
                    else
                    {
                        for (int rodo = _center + 1; rodo < _areaUpPointZ; rodo++)
                        {
                            _areas[_keepMinAreaSize, rodo] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                            _areas[_keepMinAreaSize, rodo].transform.parent = _mapController.transform;
                        }
                    }
                }
                else
                {
                    for (int aisle = _keepMinAreaSize; aisle < _keepFrontSide; aisle++)
                    {
                        _areas[aisle, _areaUnderPointZ] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(aisle, 0, _areaUnderPointZ), Quaternion.identity);
                        _areas[aisle, _areaUnderPointZ].transform.parent = _mapController.transform;
                    }

                    //�����Ȃ��邽�߂̃R�[�h
                    if (_areaUnderPointZ < _center)
                    {
                        for (int rodo = _areaUnderPointZ + 1; rodo < _center; rodo++)
                        {
                            _areas[_keepMinAreaSize, rodo] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                            _areas[_keepMinAreaSize, rodo].transform.parent = _mapController.transform;
                        }
                    }
                    else
                    {
                        for (int rodo = _center + 1; rodo < _areaUnderPointZ; rodo++)
                        {
                            _areas[_keepMinAreaSize, rodo] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                            _areas[_keepMinAreaSize, rodo].transform.parent = _mapController.transform;
                        }
                    }
                }

            }

            //Z���W��ۑ�����
            if (_randomJudgeNum == 0)
            {
                _center = _areaUpPointZ; // �����Z���W��ۑ�
            }
            else
            {
                _center = _areaUnderPointZ; // �����Z���W��ۑ�
            }

        }

        //�����Ȃ��ꏊ��ǂŖ��߂�
        WallCreate(_mapController);


        StartCoroutine("Spawner");

    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(1);
        //�v���C���[���X�|�[��������
        _sponPlayer.Spon();
        //�G�L�������X�|�[��������
        _sponEnemy.Spon();
        //�{�X�G���X�|�[��������
        _sponEnemy.BossSpon();
    }


    void WallCreate(GameObject _mapController)
    {
        for (var x = 0; x < _x; x++)
        {
            for (var z = 0; z <= _z; z++)
            {
                if (_areas[x, z] == null)
                {
                    _areas[x, z] = Instantiate(_obj[(int)AreaObj.obj2], new Vector3(x, 1, z), Quaternion.identity);
                    _areas[x, z].transform.parent = _mapController.transform;
                    areaController = MapManager._areas[x, z].GetComponent<AreaController>();
                    areaController._onWall = true;
                }
                else
                {

                }
            }
        }
    }



    static public int _bossMapX = 40;
    static public int _bossMapZ = 40;

    public void BossMapCreate(GameObject _mapController)
    {
        _x = _bossMapX;
        _z = _bossMapZ;

        _areas = new GameObject[_x + 1, _z + 1];

        for (var x = 1; x < 16; x++)
        {
            for(var z = 1; z < _bossMapZ; z++)
            {
                if(x >= 12)
                {
                    if(z > 17 && z < 23)
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

        for(var x = 16; x < _bossMapX; x ++)
        {
            for(var z = 1; z < _bossMapZ; z++)
            {
                _areas[x, z] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(x, 0, z), Quaternion.identity);
                _areas[x, z].transform.parent = _mapController.transform;
            }
        }

        //�ǐݒu
        for (var x = 0; x <= _bossMapX; x++)
        {
            for (var z = 0; z <= _bossMapZ; z++)
            {
                if (_areas[x, z] == null)
                {
                    _areas[x, z] = Instantiate(_obj[(int)AreaObj.obj2], new Vector3(x, 1, z), Quaternion.identity);
                    _areas[x, z].transform.parent = _mapController.transform;
                    areaController = MapManager._areas[x, z].GetComponent<AreaController>();
                    areaController._onWall = true;
                }
                else
                {

                }
            }
        }


        StartCoroutine("Spawner");

    }
}
