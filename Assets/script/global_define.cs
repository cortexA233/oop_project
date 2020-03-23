using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class global_define : MonoBehaviour
{
    public GameObject cred, cyellow;//该两个gameobject为红黄双方棋子的样本，定义在monobehavior子类中是为了方便在unity编辑器中直接赋予变量初始值
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }
}

public class glob //此类专用于定义全局变量和全局方法
{
    public static int is_pause = 0;
    public static float remain_time = 30f;
    public static int cur_id;//表示当前玩家ID，1为红方，2为黄方
    public static int[][] map = new int[4][];//0表示空格子，1表示红方占据格子，2表示黄方占据格子
    public static chess[] chesses = new chess[8];//用来存储棋子，0-3为红方，4-7为黄方
    public static mouse mou;//用来存储光标
    public static int[] chess_num = new int[3];//双方的棋子数量，1为红方，2为黄方
    public static Sprite mouse_picture_red;
    public static Sprite mouse_picture_yellow;//用来存储两种光标图案
    public static int is_win;//判定是否比赛结束，0表示未分出胜负
    public static void change_controller() //回合结束换人
    {
        remain_time = 30f;
        if (cur_id == 1)
        {
            cur_id = 2;
            mou.obj.GetComponent<SpriteRenderer>().sprite = mouse_picture_yellow;
        }
        else
        {
            cur_id = 1;
            mou.obj.GetComponent<SpriteRenderer>().sprite = mouse_picture_red;
        }
    }
    //此方法用于刷新棋盘状态
    public static void check()
    {
        int[] ch = new int[11];
        int des_num=0;
        for (int i = 0; i < 4; ++i) 
        {
            for(int o = 0; o < 2; ++o)
            {
                if (map[i][o] == map[i][o + 1] && map[i][o + 2] != map[i][o] && map[i][o] != 0)   
                {
                    for(int p = 0; p < 8; ++p)
                    {
                        if (glob.chesses[p].posx == i && glob.chesses[p].posy == o + 2)
                        {
                            ch[des_num++] = p;
                            Debug.Log("横吃-右" + p);
                     //       glob.chesses[p].des();
                        }
                    }
                }
            }
            for(int o = 3; o > 1; --o)
            {
                if (map[i][o] == map[i][o - 1] && map[i][o - 2] != map[i][o] && map[i][o] != 0)
                {
                    for (int p = 0; p < 8; ++p)
                    {
                        if (glob.chesses[p].posx == i && glob.chesses[p].posy == o - 2)
                        {
                            ch[des_num++] = p;
                            Debug.Log("横吃-左" + p);
                      //      glob.chesses[p].des();
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 4; ++i)
        {
            for (int o = 0; o < 2; ++o)
            {
                if (map[o][i] == map[o + 1][i] && map[o + 2][i] != map[o][i] && map[o][i] != 0) 
                {
                    for (int p = 0; p < 8; ++p)
                    {
                        if (glob.chesses[p].posx == o + 2 && glob.chesses[p].posy == i)
                        {
                            ch[des_num++] = p;
                            Debug.Log("纵吃-下" + p);
                       //     glob.chesses[p].des();
                        }
                    }
                }
            }
            for (int o = 3; o > 1; --o)
            {
                if (map[o][i] == map[o - 1][i] && map[o - 2][i] != map[o][i] && map[o][i] != 0) 
                {
                    for (int p = 0; p < 8; ++p)
                    {
                        if (glob.chesses[p].posx == o - 2 && glob.chesses[p].posy == i)
                        {
                            ch[des_num++] = p;
                            Debug.Log("纵吃-上" + p);
                        //    glob.chesses[p].des();
                        }
                    }
                }
            }
        }
        for(int i = 0; i < des_num; ++i)
        {
            glob.chesses[ch[i]].des();
        }
    }
}


[System.Serializable]
public class chess
{
    public int cur_movx = 0, cur_movy = 0;//实现平滑移动的中间变量
    public int is_moving = 0;//表示棋子是否在移动
    public GameObject obj;//表示棋子的gameobject成员
    public int posx, posy;//棋子的当前位置（以棋盘的左下角第一格为（0，0）坐标轴原点）
    public int is_selected = 0, id;//分别表示棋子是否被选中及其所属玩家
    public int is_destroyed = 0;//表示棋子是否已经被破坏（游戏结束或者被吃掉）
    public chess(int id,int px,int py)
    {
        if (id == 1)
        {
            this.id = 1;
            this.obj = GameObject.Instantiate(GameObject.FindGameObjectWithTag("cred"));
        }
        else
        {
            this.id = 2;
            this.obj = GameObject.Instantiate(GameObject.FindGameObjectWithTag("cyellow"));
        }
        glob.map[px][py] = this.id;
        this.obj.transform.position = new Vector2(px + 0.5f, py + 0.5f);
        this.posx = px;
        this.posy = py;
        this.is_selected = 0;
        this.is_moving = 0;
        this.is_destroyed = 0;
    } 
    /*多线程延时，废案
    Thread t1;
    int movx=0, movy=0;
    public int move(int x,int y)//外部调用
    {
        if (this.posx <= 0 && x < 0) return 0;
        if (this.posy <= 0 && y < 0) return 0;
        if (this.posx >= 3 && x > 0) return 0;
        if (this.posy >= 3 && y > 0) return 0;
        if (glob.map[this.posx + x][this.posy + y] != 0) return 0;
        if (x + y > 1) return 0;
        t1 = new Thread(move_r);
        
        
        this.cur_movx = x;
        this.cur_movy = y;
        movx = x;
        movy = y;
        Debug.Log(movx);
        Debug.Log(movy);
        t1.Start();
        return 1;
    }
    
    public void move_r()//多线程马甲
    {
        Thread.Sleep(1000);
        //   Debug.Log(posx);
        //  Debug.Log(posy);
        glob.map[this.posx][this.posy] = 0;
        this.posx += movx;
        this.posy += movy;
        glob.map[this.posx][this.posy] = this.id;
        glob.check();
    }
   */
    public int move(int x,int y)//移动棋子的方法
    {
        if (this.posx <= 0 && x < 0) return 0;
        if (this.posy <= 0 && y < 0) return 0;
        if (this.posx >= 3 && x > 0) return 0;
        if (this.posy >= 3 && y > 0) return 0;
        if (glob.map[this.posx + x][this.posy + y] != 0) return 0;
        if (x + y > 1) return 0;

        glob.map[this.posx][this.posy] = 0;
        this.cur_movx = x;
        this.cur_movy = y;

        this.posx += x;
        this.posy += y;
        glob.map[this.posx][this.posy] = this.id;
        return 1;
    }
    public void des()
    {
        this.is_destroyed = 1;
        GameObject.Destroy(this.obj);
        --glob.chess_num[this.id];
        glob.map[this.posx][this.posy] = 0;
        this.posx = -222;
        this.posy = -222;
    }
    public void auto_move()//实现棋子平滑移动的方法
    {
        if (this.is_moving >= 30)
        {
            glob.check();
            this.cur_movx = 0;
            this.cur_movy = 0;
            this.is_moving = 0;
            return;
        }
        if (this.cur_movy != 0 || this.cur_movx != 0)
        {
            this.obj.transform.Translate(new Vector2(this.cur_movx / 30f, this.cur_movy / 30f));
            ++this.is_moving;
        }
    }
}

public class mouse
{
    public int cur_movx = 0, cur_movy = 0;
    public int is_moving = 0;
    public int is_destroyed = 0;
    public GameObject obj;
    public int posx, posy;
    public void auto_move()
    {
        if (this.is_moving > 20)
        {
            this.cur_movx = 0;
            this.cur_movy = 0;
            this.is_moving = 0;
            return;
        }
        if (this.cur_movy != 0 || this.cur_movx != 0)
        {
            this.obj.transform.Translate(new Vector2(this.cur_movx / 20f, this.cur_movy / 20f));
            ++this.is_moving;
        }
    }
    public mouse(int px, int py)
    {
        this.obj = GameObject.Instantiate(GameObject.FindGameObjectWithTag("mouse"));
        this.obj.transform.position = new Vector2(px + 0.5f, py + 0.5f);
        this.posx = px;
        this.posy = py;
        this.is_destroyed = 0;
    }

    public void move(int x, int y)
    {
        if (this.posx <= 0 && x < 0) return;
        if (this.posy <= 0 && y < 0) return;
        if (this.posx >= 3 && x > 0) return;
        if (this.posy >= 3 && y > 0) return;
        if (Mathf.Abs(x) + Mathf.Abs(y) > 1) return;
        this.cur_movx = x;
        this.cur_movy = y;
        this.posx += x;
        this.posy += y;
        this.is_moving = 1;
    }
    public void des()
    {
        this.is_destroyed = 1;
        GameObject.Destroy(this.obj);
        this.posx = -222;
        this.posy = -222;
    }
}

