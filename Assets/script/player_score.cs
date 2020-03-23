using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_score : MonoBehaviour
{
    public Text t_red, t_yellow, winning_tag, time_tag;
    public Text manual_text;
    public Button manual_button;
    public GameObject ui_obj;
    const string shuoming= "操作说明：上/下/左/右方向键移动方形光标，光标黑色代表红方回合，白色代表黄方回合。按A键可选中光标停留处的棋子，选中后按上/下/左/右方向键即可移动棋子，移动后回合结束。\n（按Esc键退出演示）";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void disp()
    {
        manual_text.text = shuoming;
        manual_button.onClick.RemoveAllListeners();
        manual_button.onClick.AddListener(undisp);
        glob.is_pause = 1;
    }
    public void undisp()//disp/undisp分别为显示/隐藏操作说明文字的监听事件
    {
        manual_text.text = "";
        manual_button.onClick.RemoveAllListeners();
        manual_button.onClick.AddListener(disp);
        glob.is_pause = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (glob.is_pause == 0 && glob.is_win == 0) 
        {
            glob.remain_time -= Time.deltaTime;
        }
        
        time_tag.text = "剩余时间："+glob.remain_time;
        t_red.text = "红方棋子数：" + glob.chess_num[1];
        t_yellow.text = "黄方棋子数：" + glob.chess_num[2];
        if (glob.is_win == 0)
        {
            winning_tag.text = "";
        }
        if (glob.chess_num[1] < 2 || (glob.cur_id == 1 && glob.remain_time <= 0)) 
        {
            if (glob.remain_time <= 0)
            {
                glob.remain_time = 0;
            }
            glob.is_win = 1;
            winning_tag.text = "黄方胜！按A重新开始游戏";
        }
        if (glob.chess_num[2] < 2 || (glob.cur_id == 2 && glob.remain_time <= 0))
        {
            if (glob.remain_time <= 0)
            {
                glob.remain_time = 0;
            }
            glob.is_win = 1;
            winning_tag.text = "红方胜！按A重新开始游戏";
        }
    }
}
