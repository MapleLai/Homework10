# 网络版巡逻兵
--------------
## 游戏
基于之前巡逻兵的游戏改成这一次的双人网络版巡逻兵。创建服务器的为玩家1，后面加入的为玩家2。玩家2担当巡逻兵，在30s内追赶到玩家1则获得游戏胜利，否则玩家1胜利。

## 演示
+ 游戏场景
![Player1](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/Player1.jpg)
![Player2](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/Player2.png)
+ 由于录屏时操作失误只录了一个窗口的视频，但也能看出是个网络游戏，希望TA能谅解  
[演示视频](https://pan.baidu.com/s/1JTS6024eN8ku4T5_fM85hg)

## 介绍
+ Player  
在Player预制上添加NetworkIdentity和NetworkTransform组件，使它能够在网络中被实例化和同步位置。
![Player](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/Player.jpg)

+ NetworkManager  
利用空对象创建一个NetworkManager管理网络状态，添加NetworkManager和NetworkManagerHUD组件。把刚才的Player拖进玩家预制插槽里。  
![View](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/View.png)
![NetworkManager](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/NetworkManager.jpg)
