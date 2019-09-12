using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameBtnCor : MonoBehaviour {

    //开始游戏按钮
    public Button StartGameBtn;
    //游戏帮助按钮
    public Button GameHelpBtn;
    //关闭帮助按钮
    public Button CloseHelpBtn;

    //游戏帮助界面父物体
    public GameObject GameHelpFace; 


	// Use this for initialization
	void Start () {

        //添加按钮事件
        StartGameBtn.GetComponent<Button>().onClick.AddListener(LoadGameFace);
        GameHelpBtn.GetComponent<Button>().onClick.AddListener(OpenHelpFace);
        CloseHelpBtn.GetComponent<Button>().onClick.AddListener(CloseHelpFace);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 打开帮助界面
    /// </summary>
    public void OpenHelpFace()
    {
        print("打开游戏帮助界面");
        GameHelpFace.GetComponent<CanvasGroup>().alpha = 1;
        GameHelpFace.GetComponent<CanvasGroup>().blocksRaycasts = true;
        StartGameBtn.GetComponent<Image>().raycastTarget = false;
        CloseHelpBtn.GetComponent<Image>().raycastTarget = false;
    }

    /// <summary>
    /// 关闭帮助界面
    /// </summary>
    private void CloseHelpFace()
    {
        print("关闭游戏帮助界面");
        GameHelpFace.GetComponent<CanvasGroup>().alpha = 0;
        GameHelpFace.GetComponent<CanvasGroup>().blocksRaycasts = false;
        StartGameBtn.GetComponent<Image>().raycastTarget = true;
        CloseHelpBtn.GetComponent<Image>().raycastTarget = true;
    }

    /// <summary>
    /// 打开加载游戏界面
    /// </summary>
    private void LoadGameFace()
    {
        SceneManager.LoadScene(1);
    }

    
}
