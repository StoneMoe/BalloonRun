BalloonRun
==
仿《飘飘》小游戏

Z轴层级规范
--

- 0 ~ 40 Player（见PlayerLayer定义）
- 0   Other（使用SortingLayer）
- -10 Main Camera

PlayerLayer

    - 头发 40 ~ inf
    - 眼睛 30 ~ 39
    - 脸部 20 ~ 29
    - 衣服 10 ~ 19
    - 飞机 0 ~ 9
    - 气球 0 ~ 9

TODO
--
实现GamePlay Rules  
Player碰撞反弹逻辑移动到服务器