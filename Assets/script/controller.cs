using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public GameObject hajime_red, hajime_yellow, hajime_button, sample_black, sample_white;
    int select_status = -1;
    // Start is called before the first frame update
    void Start()
    {
        ini();
    }

    // Update is called once per frame
    void Update()
    {

        if (glob.remain_time <= 0f)
        {
            select_status = -1;
            glob.change_controller();
        }
        glob.mou.auto_move();
        for(int i = 0; i < 8; ++i)
        {
            if (glob.chesses[i].is_destroyed == 0)
            {
                glob.chesses[i].auto_move();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (glob.is_win == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && glob.mou.is_moving == 0) 
            {
                select_status = -1;
                for (int i = 0; i < 8; ++i)
                {
                    if (glob.mou.posx == glob.chesses[i].posx && glob.mou.posy == glob.chesses[i].posy && glob.cur_id == glob.chesses[i].id) 
                    {
                        select_status = i;
                        break;
                    }
                }
           //     print(select_status + "!");
            }
            if (select_status == -1)
            {
                if (glob.mou.is_moving == 0)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        glob.mou.move(0, 1);
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                        glob.mou.move(0, -1);
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        glob.mou.move(-1, 0);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        glob.mou.move(1, 0);
                }
            }
            else
            {
                int mx = 0, my = 0;
                if (Input.GetKeyDown(KeyCode.UpArrow)) my = 1;
                if (Input.GetKeyDown(KeyCode.DownArrow)) my = -1;
                if (Input.GetKeyDown(KeyCode.LeftArrow)) mx = -1;
                if (Input.GetKeyDown(KeyCode.RightArrow)) mx = 1;
                if (glob.chesses[select_status].move(mx, my) != 0)
                {
                    select_status = -1;
                    glob.change_controller();
             //       glob.check();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < 8; ++i)
                {
                    if (glob.chesses[i].is_destroyed == 0)
                    {
                        Debug.Log("重玩销毁");
                        glob.chesses[i].des();
                    }
                }
                if (glob.mou.is_destroyed == 0)
                {
                    glob.mou.des();
                }
                ini();
            }
        }
        
    }

    void ini()
    {
        glob.mouse_picture_red = sample_black.GetComponent<SpriteRenderer>().sprite;
        glob.mouse_picture_yellow = sample_white.GetComponent<SpriteRenderer>().sprite;
        glob.chess_num[1] = 4;
        glob.chess_num[2] = 4;
        glob.cur_id = 1;
        glob.is_win = 0;
        glob.mou = new mouse(0, 0);
        for (int i = 0; i < 4; ++i)
        {
            glob.map[i] = new int[4];
            for (int o = 0; o < 4; ++o)
            {
                glob.map[i][o] = 0;
            }
        }
        for (int i = 0; i < 4; ++i)
        {
            glob.chesses[i] = new chess(1, i, 0);
        }
        for (int i = 4; i < 8; ++i)
        {
            glob.chesses[i] = new chess(2, i - 4, 3);
        }
    }
}
