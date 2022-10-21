using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///�v���C���[�̓������Ǘ�����X�N���v�g
/// </summary>
public class PMove : MonoBehaviour
{
    /// <summary>�L�[���͂����������ǂ���</summary>
    static public bool _buttonDown = false;
    //int _count = 0;
    /// <summary>���ݒn�̂���</summary>
    int _pointX = 0;
    /// <summary>���ݒn�̂���</summary>
    int _pointZ = 0;

    /// <summary>�ړ���E�O���m�F�A�ύX���邽�߂�AreaController�X�N���v�g���l������ϐ�</summary>
    AreaController areaController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Pmove();

    }

    /// <summary>�v���C���[�𓮂������߂̊֐� </summary>
    void Pmove()
    {
        var velox = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");


        if (Input.GetButtonDown("Horizontal"))
        {
            //if(_buttonDown == false)//���̓L�[�������ꂽ�Ƃ��ɓG���ꏏ�ɓ����Ăق����̂�
            //{
            //    RizumuController._eMoveFlag = true;
            //}

            if (RizumuController._moveFlag == true && _buttonDown == false)
            {
                if (velox > 0)
                {
                    //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                    areaController = MapManager._areas[_pointX + 1, _pointZ].GetComponent<AreaController>();

                    //�ړ���̏��ɂ���čs�������߂�
                    if (areaController._onWall == true)
                    {

                    }
                    else if (areaController._onEnemy == true)
                    {

                    }
                    else
                    {
                        areaController._onPlayer = true;
                        areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                        areaController._onPlayer = false;
                        this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                        _pointX = _pointX + 1;
                        _buttonDown = true;
                    }

                    //this.transform.position = new Vector3(MapManager._areas[_pointX + 1, _pointZ].transform.position.x, MapManager._areas[_pointX + 1, _pointZ].transform.position.y, MapManager._areas[_pointX + 1, _pointZ].transform.position.z);
                }
                else if (velox < 0)
                {
                    areaController = MapManager._areas[_pointX - 1, _pointZ].GetComponent<AreaController>();

                    //�ړ���̏��ɂ���čs�������߂�
                    if (areaController._onWall == true)
                    {

                    }
                    else if (areaController._onEnemy == true)
                    {

                    }
                    else
                    {
                        areaController._onPlayer = true;
                        areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                        areaController._onPlayer = false;
                        this.transform.position = new Vector3(MapManager._areas[_pointX - 1, _pointZ].transform.position.x, this.transform.position.y, this.transform.position.z);
                        _pointX = _pointX - 1;
                        _buttonDown = true;
                    }
                }
            }
            else
            {
                Debug.Log("MISS");
                _buttonDown = true;
            }
            
        }

        if (Input.GetButtonDown("Vertical"))
        {
            if (RizumuController._moveFlag == true && _buttonDown == false)
            {
                if (vertical > 0)
                {
                    //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                    areaController = MapManager._areas[_pointX, _pointZ + 1].GetComponent<AreaController>();

                    //�ړ���̏��ɂ���čs�������߂�
                    if (areaController._onEnemy == true)
                    {

                    }
                    else if (areaController._onWall == true)
                    {

                    }
                    else
                    {
                        areaController._onPlayer = true;
                        areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                        areaController._onPlayer = false;
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, MapManager._areas[_pointX, _pointZ + 1].transform.position.z);
                        _pointZ = _pointZ + 1;
                        _buttonDown = true;
                    }

                }
                else if (vertical < 0)
                {
                    //�s�����������̏����m�F�������̂ňړ���̃X�N���v�g���擾����
                    areaController = MapManager._areas[_pointX, _pointZ - 1].GetComponent<AreaController>();

                    //�ړ���̏��ɂ���čs�������߂�
                    if (areaController._onEnemy == true)
                    {

                    }
                    else if (areaController._onWall == true)
                    {

                    }
                    else
                    {
                        areaController._onPlayer = true;
                        areaController = MapManager._areas[_pointX, _pointZ].GetComponent<AreaController>();
                        areaController._onPlayer = false;
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, MapManager._areas[_pointX, _pointZ - 1].transform.position.z);
                        _pointZ = _pointZ - 1;
                        _buttonDown = true;
                    }

                }
            }
            else
            {
                Debug.Log("MISS");
                _buttonDown = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        for (int x = 0; x < MapManager._x; x++)
        {
            for (int z = 0; z < MapManager._z; z++)
            {
                if (MapManager._areas[x, z] == collision.gameObject)
                {
                    //���݂̃v���C���[�̈ʒu�𒲂ׂ�
                    _pointX = x;
                    _pointZ = z;
                    Debug.Log("���݂̔z��ԍ�" + _pointX + " , " + _pointZ);
                }
            }
        }
    }
}
