using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    /// <summary>�G�l�~�[�̃f�[�^�Ɋւ��ẴN���X</summary>
    EnemyModel _enemyModel = null;

    /// <summary>�G�l�~�[�̕\���Ɋւ��ẴN���X</summary>
    [SerializeField] EnemyView _enemyView = null;

    [SerializeField] int _enemyHp = 1;

    EnemyList _enemyList = null;

    public void GetLisut()
    {
        var gameObject = GameObject.Find("EnemyList");

        _enemyList = gameObject.GetComponent<EnemyList>();
    }

    public void Init()
    {
        _enemyModel = new EnemyModel(
            _enemyHp,
            x =>
            {
                _enemyView.ChangeSliderValue(_enemyHp, x);
                if(x == 0)
                {
                    _enemyList.EnemyDestroy(this.gameObject);
                }
            },
            _enemyView.gameObject);


    }

    public void Damage(int pPower)
    {
        if (_enemyModel == null)
        {
            Debug.Log("EnemyModel��null�ł�");
        }

        Debug.Log("�_���[�W�^���Ă���");
        _enemyModel.Damage(pPower);
    }
}
