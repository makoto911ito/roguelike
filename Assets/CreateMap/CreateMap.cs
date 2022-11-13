using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    //�}�b�v�S�̂̑傫�������߂�
    [SerializeField] static public int _x = 50;//�}�b�v�S�̂̉���
    [SerializeField] static public int _z = 50;//�}�b�v�S�̂̏c��
    [SerializeField] int _areaNum = 4;//���G���A�̐�

    //�����̑傫���̌��߂邽�߂͈̔�
    [SerializeField] int _mapSizeMin = 3;//���������ԏ����������̃T�C�Y
    [SerializeField] int _mapSizeMax = 7;//���������Ԃł��������̃T�C�Y

    [SerializeField] GameObject _obj;//��������I�u�W�F�N�g

    int keepMaxArea = 1;//����x���W�̍ő�l
    int _keepMinAreaSize = 1;//�O���x���W�̍ő�l

    int _areaSize = 0; //�}�b�v����؂�T�C�Y
    int _randomPosX = 0;//��؂������Ő������镔���̉����̒��S�_
    int _randomPosZ = 0;//��؂������Ő������镔���̏c���̒��S�_
    int _randomMapSize = 0;//�������镔���̑傫��
    int _count = 0;// ����ڂ̃I�u�W�F�N�g�������Ǘ�����ϐ�
    int _keepBackSide = 0;//�������������̈�ԍŌ�ɂ���I�u�W�F�N�g��x���W���Ǘ�����ϐ�
    int _keepFrontSide = 0;//�������������̈�ԍŏ��ɂ���I�u�W�F�N�g��x���W���Ǘ�����ϐ�
    int _keepPosZ = 0;//��������������Z���W���Ǘ�����ϐ�

    // Start is called before the first frame update
    void Start()
    {
        _areaSize = _x / _areaNum; //��������傫�������߂�

        for (int i = 0; i < _areaNum; i++) //�}�b�v�̐���
        {
            _keepMinAreaSize = keepMaxArea; //�O��̍ő啝��ۑ�����
            if (i == 0) // �ŏ��̋��
            {
                keepMaxArea = _areaSize;
                _randomPosX = Random.Range(1, keepMaxArea);//��؂������̒����牡���̒��S�����߂�
            }
            else if (i == _areaNum - 1) //�Ō�̋��̏ꍇ
            {
                keepMaxArea = _x - 1;
                Debug.Log(_keepMinAreaSize + "�O��̍ő�̕�");
                Debug.Log(keepMaxArea + "�@����̍ő�̕�");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
            }
            else // �Ԃ̋��
            {
                keepMaxArea += _areaSize;
                Debug.Log(_keepMinAreaSize + "�O��̍ő�̕�");
                Debug.Log(keepMaxArea + "�@����̍ő�̕�");
                _randomPosX = Random.Range(_keepMinAreaSize, keepMaxArea);
            }

            _randomPosZ = Random.Range(1, _z);//�c���̒��S�����߂Ă���

            _randomMapSize = Random.Range(_mapSizeMin, _mapSizeMax); //�����̑傫�������߂Ă���

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

            //���������G���A�̍Ō���̃}�X���獡��̍ő啝�܂œ����Ȃ���
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
                    //�����Ȃ��邽�߂̃R�[�h
                    for (int rodo = _keepPosZ + 1; rodo < _randomPosZ; rodo++)
                    {
                        Instantiate(_obj, new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                    }
                }
                else
                {
                    //�����Ȃ��邽�߂̃R�[�h
                    for (int rodo = _randomPosZ + 1; rodo < _keepPosZ; rodo++)
                    {
                        Instantiate(_obj, new Vector3(_keepMinAreaSize, 0, rodo), Quaternion.identity);
                    }
                }

            }

            //�����Z���W��ۑ�
            _keepPosZ = _randomPosZ;
        }
    }
}
