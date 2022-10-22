using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RizumuController : MonoBehaviour
{
    /// <summary>�^�C�~���O�̎��Ԃ��擾����ϐ�</summary>
    static public float _time;
    /// <summary>������^�C�~���O�����߂�ϐ�</summary>
    public float _keisoku = 1;

    /// <summary>�v���C���[�������邩�ǂ������肷��t���O</summary>
    static public bool _moveFlag = false;

    /// <summary>�G�������邩�ǂ������������t���O</summary>
    static public bool _eMoveFlag = false;

    public Text _text;

    /// <summary>���͂��󂯎󂯂Ă��鎞��</summary>
    [SerializeField] float _pTaim = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _time.ToString();

        _time += Time.deltaTime;
        if (_time > _keisoku)
        {
            //Debug.Log("������");
            _moveFlag = true;

            if (_time > _keisoku + _pTaim) //���͂̃��O���l�����Ēx�点�Ă���
            {
                _eMoveFlag = true;
                _time = 0;
                PMove._buttonDown = false;
            }
        }
        else
        {
            _moveFlag = false;
        }
    }
}
