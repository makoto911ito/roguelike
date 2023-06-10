using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesut : MonoBehaviour
{
    GameManager _gm;

    [SerializeField] EnemyList _enemyList;

    PlayerPresenter _playerPresenter;

    [SerializeField] bool _testFlag;

    public void Start()
    {
        _gm = GetComponent<GameManager>();
    }

    private void Update()
    {
        if (_testFlag != false)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _gm.DetBoosEnemy();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                _gm.GoPlay(1);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                //�v���C���[�̏����擾
                var gameObject = GameObject.Find("Player");
                if (gameObject == null)
                {
                    Debug.Log("�v���C���[���擾�ł��܂���ł���");
                }

                _playerPresenter = gameObject.GetComponent<PlayerPresenter>();

                _playerPresenter.EnemyAttack(1);
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                var obj = GameObject.FindGameObjectWithTag("Boss");
                _enemyList.EnemyDestroy(obj);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                _gm.DetGameBoosEnemy();
            }
        }
        else
        {

        }
    }
}

