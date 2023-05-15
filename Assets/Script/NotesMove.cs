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

    /// <summary>
    /// �P�r�[�g�̕b�����擾���邽�߂̊֐�
    /// </summary>
    /// <param name="beat">�P�r�[�g�̕b��</param>
    public void Setup(float beat)
    {
        _time = beat;
    }

    // Start is called before the first frame update
    void Start()
    {
        _point = GameObject.Find("NotesJudgePoint");

        _pointRect = _point.GetComponent<RectTransform>();

        _myRect = GetComponent<RectTransform>();

        //Debug.Log(_pointRect.anchoredPosition.x);

        _kyori = Mathf.Abs(_myRect.anchoredPosition.x - _pointRect.anchoredPosition.x);
        //Debug.Log(_kyori);
    }

    // Update is called once per frame
    void Update()
    {
        _myRect.anchoredPosition = new Vector2(_myRect.anchoredPosition.x + _kyori * _time * Time.deltaTime * 1f, 0);

        if (_myRect.anchoredPosition.x >= _pointRect.anchoredPosition.x)
        {
            Destroy(gameObject);
        }
    }
}
