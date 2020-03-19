using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_score : MonoBehaviour
{
    public Text t_red, t_yellow, winning_tag, time_tag;
    public Text testmap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   /*     testmap.text = "";
        for(int i = 0; i < 4; ++i)
        {
            for(int o = 0; o < 4; ++o)
            {
                testmap.text += glob.map[o][i];
            }testmap.text += "\n";
        }*/
        glob.remain_time -= Time.deltaTime;
        time_tag.text = "剩余时间："+glob.remain_time;
        t_red.text = "红方棋子数：" + glob.chess_num[1];
        t_yellow.text = "黄方棋子数：" + glob.chess_num[2];
        if (glob.is_win == 0)
        {
            winning_tag.text = "";
        }
        if (glob.chess_num[1] < 2)
        {
            glob.is_win = 1;
            winning_tag.text = "黄方胜！按空格键重新开始游戏";
        }
        if (glob.chess_num[2] < 2)
        {
            glob.is_win = 1;
            winning_tag.text = "红方胜！按空格键重新开始游戏";
        }
    }
}
