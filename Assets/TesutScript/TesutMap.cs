using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TesutMap : MonoBehaviour
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
    [SerializeField] TesutPlayerSpawner _sponPlayer;

    /// <summary>�G�𐶐����邽�߂̃X�N���v�g���Q�Ƃ��邽�߂̕ϐ�</summary>
    [SerializeField] SponEnemy _sponEnemy;

    AreaController areaController;

    [SerializeField] public GameObject _mapController;

    private void Start()
    {
        BossMapCreate(_mapController);
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



    static public int _bossMapX = 40;
    static public int _bossMapZ = 40;

    public void BossMapCreate(GameObject _mapController)
    {
        _areas = new GameObject[_bossMapX + 1, _bossMapZ + 1];

        for (var x = 1; x < 16; x++)
        {
            for (var z = 1; z < _bossMapZ; z++)
            {
                if (x >= 12)
                {
                    if (z > 17 && z < 23)
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

        for (var x = 16; x < _bossMapX; x++)
        {
            for (var z = 1; z < _bossMapZ; z++)
            {
                _areas[x, z] = Instantiate(_obj[(int)AreaObj.walk], new Vector3(x, 0, z), Quaternion.identity);
                _areas[x, z].transform.parent = _mapController.transform;
            }
        }

        StartCoroutine("Spawner");
    }
}
