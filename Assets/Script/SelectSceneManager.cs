using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneManager : MonoBehaviour
{
    Scene _gameScene;
    GameManager _gameManager;

    /// <summary>���s�̌��ʂ�\������e�L�X�g</summary>
    [SerializeField] Text _resultText;
    /// <summary>�Q�[���N���A�ɂ����������Ԃ�\�����邽�߂̃e�L�X�g</summary>
    [SerializeField] Text _clearTimeText;

    // Start is called before the first frame update
    void Start()
    {

        _gameScene = SceneManager.GetSceneByName("GameScene");

        foreach (var GameSecenObj in _gameScene.GetRootGameObjects())
        {

            if(GameSecenObj.name == "System")
            {
                _gameManager = GameSecenObj.transform.Find("GameManager").GetComponent<GameManager>();
                if (_gameManager != null)
                {
                    Debug.Log("������");
                    _resultText.text = _gameManager._gameResult;
                    if (_gameManager._isVictory == true)
                    {
                        _clearTimeText.enabled = true;
                        _clearTimeText.text = _gameManager._playTime.ToString();
                    }
                    else
                    {
                        Debug.Log("�����܂���");
                        _clearTimeText.enabled = false;
                    }

                    break;
                }
            }
        }
    }
}
