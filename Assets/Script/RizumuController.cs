using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RizumuController : MonoBehaviour
{
    [SerializeField] AudioClip _audio;
    AudioSource _audioSource = default;

    //[SerializeField] Text _testText;

    float _bpm = 0;

    int _count = 1;
    /// <summary>�^����ꂽ�����̎��g��</summary>
    int _frequency = 0;

    /// <summary></summary>
    float _time = 0;
    static public float _beat = 0;
    float _keikajikan = 0;
    float _judgeTime = 0;

    /// <summary>�P�r�[�g�̕b��</summary>
    float _beatTime = 0;

    static public float _moveTime = 0;

    static float _mae = 0;
    static float _usiro = 0;

    GameObject _player;
    PlayerMove _playerMove;

    [SerializeField] Image _sponImage;
    [SerializeField] NotesMove _notesImage;
    [SerializeField] Image _notesPoint;

    [SerializeField] EnemyList _enemyList;

    public void Judgetiming(float audioTime, float moveNumber, string direction)
    {
        Debug.Log(audioTime + " �n���ꂽ�l" + " " + _mae + "���͉\�͈́i���߁j");

        if (audioTime >= _mae)
        {
            Debug.Log("��������");
            _playerMove.GoMove(moveNumber, direction);
        }
        else if (audioTime >= _usiro || audioTime < _mae)
        {
            Debug.Log("�~�X");
        }
    }

    public void PlaySound()
    {
        _audioSource = GetComponent<AudioSource>();

        //�^����ꂽ�����̎��g�����m�F
        _frequency = _audio.frequency;

        _bpm = BpmChecker.AnalyzeBpm(_audio, _frequency);
        if (_bpm < 0)//�A���Ă���BPM�̒l���O�����������l�ł���Ή����������Ă��Ȃ����ƂɂȂ邻����G���[�Ƃ��ăf�o�b�N����
        {
            Debug.LogError("AudioClip is null.");
            return;
        }

        Debug.Log(_bpm);

        _beatTime = 60 / _bpm;

        //����̃Y���̕b�����擾
        _judgeTime = _frequency * Time.fixedDeltaTime;

        //�Ȃ𗬂�
        _audioSource.clip = _audio;
        _audioSource.Play();
    }

    public void NowPlayerSpawn()
    {
        _player = GameObject.Find("Player");
        _playerMove = _player.GetComponent<PlayerMove>();
    }

    //Debug.Log(_audioSource.timeSamples + "�@�^�C��");
    //    float bite = 60f / (float)_bpm;
    //Debug.Log(bite + "�r�[�g");
    //    float ans = _frequency * bite;
    //Debug.Log(ans + " �T���v��/�r�[�g");

    private void Update()
    {
        _time = _audioSource.time;
        _moveTime = _audioSource.timeSamples;//�Ȃ̌o�ߎ��Ԃ��擾
        _beat = Beat(_count);//����̊Ԋu���擾
        _mae = _beat - _judgeTime;//�擾��������̑O�Y��
        _usiro = _beat + _judgeTime;//�擾�����Ԋu�̌�Y��

        _keikajikan = _moveTime - _beat;
        if (_moveTime >= _beat)
        {
            //Debug.Log(_time);
            //_testText.text = "�͂�".ToString();
        }

        if (_keikajikan > _judgeTime && _moveTime > _usiro)
        {

            _enemyList.GoEnemyMove();
            //_testText.text = "".ToString();
            _count++;
            var image = Instantiate(_notesImage, _sponImage.rectTransform);
            //image.transform.parent = _notesPoint.transform;
            image.transform.SetParent(_notesPoint.transform);
            image.Setup(_beatTime);
        }
    }

    /// <summary>
    /// ����̊Ԋu�����߂�
    /// </summary>
    /// <param name="count">��</param>
    /// <returns></returns>
    float Beat(float count)
    {
        return _frequency * (60 * count / _bpm);
    }
}
