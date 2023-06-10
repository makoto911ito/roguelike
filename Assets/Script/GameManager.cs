using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] StairPointCreate _stairPointCreate;
    [SerializeField] RizumuController _rizumuController;
    [SerializeField] MapManager _mapManager;
    [SerializeField] public GameObject _mapController;
    [SerializeField] GameObject _nootCanvas;
    [SerializeField] Image _loadImage;

    AreaController _areaController;

    [SerializeField] GameObject _closeDoor;
    [SerializeField] GameObject _stair;

    //��������t���A�̍ő�l�ƍŏ��l
    [SerializeField] int _maxFloor = 4;
    [SerializeField] int _minFloor = 2;

    [SerializeField] int _randomFloorNum = 0;

    /// <summary>�e�X�g�p�̃t���O</summary>
    [SerializeField] bool _tesutFrag = false;

    /// <summary>�K�i���g������</summary>
    [SerializeField] public int _count = 0;

    /// <summary>���U���g��ʂŕ\������N���A���ۂ��̔���</summary>
    public string _gameResult;

    /// <summary>�N���A�����̔���</summary>
    public bool _isVictory = false;

    /// <summary>���킵�Ă��鎞��</summary>
    public float _playTime = 0;

    /// <summary>�_���W�����ɐ����Ă��邩�ǂ���</summary>
    public bool _nowPlay = false;

    [SerializeField] EnemyList _enemyList;

    private void Start()
    {
        if (_tesutFrag == false)
        {
            LoadMap(true);
            GoPlay(0);
        }
        else
        {
            _mapManager.BossMapCreate(_mapController);
        }
        _randomFloorNum = Random.Range(_minFloor, _maxFloor + 1);
    }

    /// <summary>
    /// �}�b�v�̐������s��
    /// </summary>
    public void GoPlay(int count)
    {
        LoadMap(true);
        _count += count;

        //��ԍŏ��̃t���A�ł���΂��̂܂܃}�b�v�̐����B�����łȂ���΍�����}�b�v���������ēx�}�b�v�̐������s��
        if (_count > 0)
        {

            foreach (Transform map in _mapController.transform)
            {
                _areaController = map.GetComponent<AreaController>();
                _areaController.ResetStatus();
                Destroy(map.gameObject);
                var obj = GameObject.Find("stair");
                Destroy(obj);
            }

            _enemyList.EnemyReset();

            if (_count == _randomFloorNum)
            {              
                //�{�X�̕��������
                StartCoroutine(CreateBoosMap());
            }
            else
            {
                StartCoroutine(CreateMap());
            }
        }
        else
        {
            _mapManager.MapCreate(_mapController);
            _stairPointCreate.PointCreate(_closeDoor);
        }
    }

    /// <summary>
    /// �}�b�v�̐����ƕ��Ă���K�i�̐ݒu
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateMap()
    {
        yield return new WaitForSeconds(0.5f);
        _rizumuController.StopSound();
        _mapManager.MapCreate(_mapController);
        _stairPointCreate.PointCreate(_closeDoor);
    }

    IEnumerator CreateBoosMap()
    {
        yield return new WaitForSeconds(0.5f);
        _rizumuController.StopSound();
        _mapManager.BossMapCreate(_mapController);
        _stairPointCreate.PointCreate(_closeDoor);
    }

    /// <summary>
    /// �K�i��\������
    /// </summary>
    public void DetBoosEnemy()
    {
        _stairPointCreate.OpenDoor(_stair);
    }

    public void DetGameBoosEnemy()
    {
        _gameResult = "�N���A���܂���";
        _isVictory = true;
        _nootCanvas.SetActive(false);
        SceneManager.LoadScene("SelectScene", LoadSceneMode.Additive);
    }

    /// <summary>
    /// �T�E���h�𗬂�
    /// </summary>
    public void GoSound()
    {
        _rizumuController.PlaySound();
    }

    /// <summary>
    /// �}�b�v�����`�v���C���[�X�|�[���܂ł̎��ԂɃ��[�h��ʕ\�����邽�߂̊֐�
    /// </summary>
    public void LoadMap(bool Loadflag)
    {
        if (Loadflag == true)
        {
            _nowPlay = false;
            _loadImage.enabled = true;
            _nootCanvas.SetActive(false);
        }
        else
        {
            _loadImage.enabled = false;
            _nootCanvas.SetActive(true);
            _nowPlay = true;
        }
    }

    /// <summary>
    /// �v���C���[���|���ꂽ��Ă΂��֐�
    /// </summary>
    public void GameOvare(int PlayerPosX, int PlayerPosZ)
    {
        Debug.Log("���S���܂���");
        _areaController = MapManager._areas[PlayerPosX, PlayerPosZ].GetComponent<AreaController>();
        _areaController._onPlayer = false;
        _nootCanvas.SetActive(false);
        _gameResult = "�|����܂���";//�\���̎d�����u�i�G�̖��O�j�ɓ|����܂����v�ɂ���
        _isVictory = false;
        SceneManager.LoadScene("SelectScene", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if(_nowPlay == true)
        {
            _playTime += Time.deltaTime;
        }
    }
}
