using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMove : MonoBehaviour
{
    /// <summary>�͂����ʒu�܂ł̋���</summary>
    float _kyori;
    /// <summary>�͂����ʒu�܂łɂ����鎞��</summary>
    float _time;

    /// <summary>�X�|�[���ꏊ</summary>
    GameObject _spon;
    /// <summary>������Ƃ�ꏊ</summary>
    GameObject _point;

    RectTransform _sponRect;
    RectTransform _pointRect;
    RectTransform _myRect;

    float _speed = 0;

    float _behainTime;

    /// <summary>
    /// �P�r�[�g�̕b�����擾���邽�߂̊֐�
    /// </summary>
    /// <param name="beat">�P�r�[�g�̕b��</param>
    public void Setup(float beat , float behind)
    {
        _time = beat;
        _behainTime = behind;
        Debug.Log(_time + "�m�[�c�̑��x");
        Debug.Log(_behainTime + "�덷");
    }

    // Start is called before the first frame update
    void Start()
    {
        _point = GameObject.Find("NotesJudgePoint");

        _pointRect = _point.GetComponent<RectTransform>();

        _myRect = GetComponent<RectTransform>();

        //Debug.Log(_pointRect.anchoredPosition.x);

        _kyori = Mathf.Abs(_myRect.transform.position.x - _pointRect.transform.position.x);
        //Debug.Log(_kyori);

        _speed = _kyori %( _time + _behainTime);

        Debug.Log(_speed);
    }

    // Update is called once per frame
    void Update()
    {
        _myRect.transform.position += new Vector3(_speed * Time.deltaTime * 1.5f, 0,0);

        //if (_myRect.anchoredPosition.x >= _pointRect.anchoredPosition.x)
        //{
        //    Destroy(gameObject);
        //}

        if (_myRect.transform.position.x >= _pointRect.transform.position.x + 10)
        {
            Destroy(gameObject);
        }
    }
}
