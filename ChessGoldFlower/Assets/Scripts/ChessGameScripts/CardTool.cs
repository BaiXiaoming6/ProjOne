using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTool
{

    /// <summary>
    /// 所有牌的16进制
    /// </summary>
	public List<int> CardData = new List<int>
    {
        //0x 代表16进制
        0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x0A,0x0B,0x0C,0x0D,0x0E,   //黑桃 2 - A(14)
		0x12,0x13,0x14,0x15,0x16,0x17,0x18,0x19,0x1A,0x1B,0x1C,0x1D,0x1E,   //红桃 2 - A
		0x22,0x23,0x24,0x25,0x26,0x27,0x28,0x29,0x2A,0x2B,0x2C,0x2D,0x2E,   //樱花 2 - A
		0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x39,0x3A,0x3B,0x3C,0x3D,0x3E    //方块 2 - A
    };

    /// <summary>
    /// 牌的类型
    /// </summary>
    public enum CardType
    {
        UNDEFINE = 0, //单牌
        DUI_ZI = 1,   //对子
        SHUN_ZI = 2,  //顺子
        TONG_HUA = 3, //同花
        TONG_HUA_SHUN = 4, //同花顺
        BAO_ZI = 5,    //豹子
    }

    /// <summary>
    /// 获取牌型
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public int GetCardJType(List<Card> cards)
    {
        //cards  一组牌排序   核心  升降序比较
        cards.Sort((x, y) => { return -x.card_value.CompareTo(y.card_value); });

        int card_type = (int)CardType.UNDEFINE;
        bool ret;
        if (cards != null)
        {
            //豹子
            ret = IsBaozi(cards);
            if (ret == true)
            {
                card_type = (int)CardType.BAO_ZI;
                return card_type;
            }

            //同花顺
            ret = IsTongHuaShun(cards);
            if (ret == true)
            {
                card_type = (int)CardType.TONG_HUA_SHUN;
                return card_type;
            }

            //《同花
            ret = IsTongHua(cards);
            if (ret == true)
            {
                card_type = (int)CardType.TONG_HUA;
                return card_type;
            }

            //《顺子
            ret = IsShunZi(cards);
            if (ret == true)
            {
                card_type = (int)CardType.SHUN_ZI;
                return card_type;
            }

            //《对子
            ret = IsDuiZi(cards);
            if (ret == true)
            {
                card_type = (int)CardType.DUI_ZI;
                return card_type;
            }
        }
        return card_type;
    }

    /// <summary>
    /// 豹子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public bool IsBaozi(List<Card> cards)
    {
        if (cards[0].card_value == cards[2].card_value)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 同花顺
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public bool IsTongHuaShun(List<Card> cards)
    {
        bool TagTongHua;
        bool TagShunZi;
        TagTongHua = IsTongHua(cards);
        TagShunZi = IsShunZi(cards);
        if (TagTongHua && TagShunZi)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 同花
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public bool IsTongHua(List<Card> cards)
    {
        if (cards[0].card_color == cards[1].card_color && cards[1].card_color == cards[2].card_color)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public bool IsShunZi(List<Card> cards)
    {
        if (IsA23(cards))
            return true;
        if (cards[2].card_value - cards[1].card_value == 1 && cards[1].card_value - cards[0].card_value == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 对子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public bool IsDuiZi(List<Card> cards)
    {
        if (cards[0].card_value != cards[2].card_value)
        {
            if (cards[0].card_value == cards[1].card_value)
            {
                return true;
            }
            if (cards[1].card_value == cards[2].card_value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    /// <summary>
    /// A23特殊处理
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public bool IsA23(List<Card> cards)
    {
        if (cards[0].card_value == 2 && cards[1].card_value == 3 && cards[2].card_value == 14)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 同样牌型下比较大小
    /// </summary>
    /// <param name="cards1"> 牌组一</param>
    /// <param name="cards2"> 牌组二</param>
    /// <param name="type"> 牌类型</param>
    /// <returns></returns>
    public bool CardTypeSame(List<Card> cards1,List<Card> cards2,int type)
    {
        if (type == 0 || type == 3) //单牌 同花
        {
            //首先比较当前牌型最大的一张牌
            if (cards1[2].card_value - cards2[2].card_value > 0)
            {
                return true;
            }
            if (cards1[2].card_value - cards2[2].card_value < 0)
            {
                return false;
            }
            //如果最大牌值相同比较第二位牌型
            if (cards1[1].card_value - cards2[1].card_value > 0)
            {
                return true;
            }
            if (cards1[1].card_value - cards2[1].card_value < 0)
            {
                return false;
            }

            //如果最大牌值相同比较第三位牌型
            if (cards1[0].card_value - cards2[0].card_value > 0)
            {
                return true;
            }
            if (cards1[0].card_value - cards2[0].card_value < 0)
            {
                return false;
            }

            return false;
        }

        if (type == 1)  //对子
        {
            int subValueDuiZi = cards1[2].card_value - cards2[2].card_value;
            if (subValueDuiZi > 0)
            {
                return true;
            }
            else if (subValueDuiZi < 0)
            {
                return false;
            }
            else
            {
                if ((cards1[0].card_value + cards1[2].card_value) > (cards2[0].card_value + cards2[2].card_value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        if (type == 2 || type == 4 || type == 5) //顺子 同花顺 豹子
        {
            if (cards1[2].card_value > cards2[2].card_value)
            {
                return true;
            }
            else if (cards1[2].card_value < cards2[2].card_value)
            {
                return false;
            }
            
        }

        return false;
    }
}
