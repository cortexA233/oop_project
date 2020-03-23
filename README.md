# oop_project
面向对象程序设计，shu17级赵海武班，老师复查请进！

# 1.项目概述/简介
## 面向对象17级赵海武班课程设计——申必四子棋
- 本仓库考虑到工程大小，未将打包编译完成的成品上传，老师若需复查，可使用单独上传的成品！
- DEMO操作方法见游戏内“帮助”按钮按下后弹出的说明。
- 本项目基于unity 2019.3稳定版开发，如需查看场景或项目全貌，请使用高于2019.3.0f的版本。
- 如需查看代码，请打开asset/script目录，其中global_define文件为定义/声明专用，controller为游戏逻辑，player_score为UI逻辑。

# 2.项目代码详解
## global_define.cs
- global_define中有三个类：glob，chess和mouse（默认提供的monobehavior在本脚本中并不需要用到）。
  - glob类中负责定义各类静态（全局）数据及方法（例如判定吃子状态的check方法）。
  - chess类负责定义棋子，其中包含棋子的gameobject成员obj，位置变量posx，posy等；以及实现移动的move和auto_move等方法。本来想在chess类中利用多线程（Thread/Thread.task）实现一个延时判定+平滑移动的featrue，并顺便期待提高整体性能，但效果不是很理想，后改用其他方法实现。废案代码也作为注释保留在了chess类构造函数之后。
  - mouse类负责定义光标，其中包含光标的gameobject成员obj，两种光标图案等。
## controller.cs
- 该类的主要功能都在unity下的monobehavior命名空间内实现，为游戏逻辑脚本。
- 
