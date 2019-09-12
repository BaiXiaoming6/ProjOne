using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour {

    /// <summary>
    /// 牌的工具类
    /// </summary>
    public CardTool Cardtool = new CardTool();

    public List<Card> list_Card_Red = new List<Card>();
    public List<Card> list_Card_Blue = new List<Card>();

    //红蓝牌组对象
    public List<GameObject> RedCard_list = new List<GameObject>();
    public List<GameObject> BlueCard_list = new List<GameObject>();

    public List<int> Card_Temp = new List<int>();

	// Use this for initialization
	void Start () {
        
	}
	
    /// <summary>
    /// 生成牌
    /// </summary>
    public void CreateCard(int type, int index)
    {
        
        System.Random rnd = new System.Random(unchecked((int)System.DateTime.Now.Ticks)); //生成一个不重复的随机数
        int dex = rnd.Next(Card_Temp.Count); //System.Random.Next(maxValue); 随机生成卡牌数组中的下标
        int data = Card_Temp[dex];  //取出卡牌数组中对应的值
        Card_Temp.RemoveAt(dex);    //把取出的值从数组中移除  

        //Debug.Log("CardDataNumb____" + Cardtool.CardData.Count);

        int color = data & 0xF0;            //执行语句后得到变量data的二进制数高4位取值
        int value = data & 0x0F;            //执行语句后得到变量data的二进制数低4位取值

        if (type == 1)
        {
            RedCard_list[index].GetComponent<Image>().sprite = Resources.Load("Sprites/ChessSprites/" + "card_" + color + "_" + value,typeof(Sprite))as Sprite;
            list_Card_Red.Add(new Card(color, value, RedCard_list[index]));
        }
        else
        {
            BlueCard_list[index].GetComponent<Image>().sprite = Resources.Load("Sprites/ChessSprites/" + "card_" + color + "_" + value, typeof(Sprite)) as Sprite;
            list_Card_Blue.Add(new Card(color, value, BlueCard_list[index]));
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            ChessIns();
            CreateRedAndBlueChess();

            CardTypeToCompare();
        }
	}

    

    /// <summary>
    /// 牌型初始化
    /// </summary>
    public void ChessIns()
    {
        Card_Temp.Clear();
        for (int i = 0; i < 3; i++)
        {
            RedCard_list[i].GetComponent<Image>().sprite = Resources.Load("Sprites/ChessSprites/" + "card_back" , typeof(Sprite)) as Sprite;
            BlueCard_list[i].GetComponent<Image>().sprite = Resources.Load("Sprites/ChessSprites/" + "card_back", typeof(Sprite)) as Sprite;
        }
        Card_Temp.AddRange(Cardtool.CardData);  //复制一份卡牌数组  进行移除数据使用
    }

    /// <summary>
    /// 创建红蓝牌组
    /// </summary>
    public void CreateRedAndBlueChess()
    {
        for (int i = 0; i < 3; i++)
        {
            CreateCard(1, i);
            CreateCard(2, i);
        }
    }

    /// <summary>
    /// 牌型比较
    /// </summary>
    public void CardTypeToCompare()
    {
        int redType = Cardtool.GetCardJType(list_Card_Red);
        
        int blueType = Cardtool.GetCardJType(list_Card_Blue);

        Debug.Log("red__" + redType + "/n" + "blue__" + blueType);

        if (redType > blueType )
        {
            Debug.Log("RED Win");
        }
        else if(redType < blueType)
        {
            Debug.Log("BLUE Win");
        }
        else if(redType == blueType)
        {
            bool sub = Cardtool.CardTypeSame(list_Card_Red, list_Card_Blue, redType);
            if (sub)
            {
                Debug.Log("red Win");
            }
            else
            {
                Debug.Log("blue Win");
            }
        }
    }
}
