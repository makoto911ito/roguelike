using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //�Q�[���V�[���ւ̈ړ�
    public void GoGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    //�^�C�g���V�[���ւ̈ړ�
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    
}
