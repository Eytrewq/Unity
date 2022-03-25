using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    [SyncVar]
    public int PlayerScore1 = 0;
    [SyncVar]
    public int PlayerScore2 = 0;
    public GUISkin layout;
    GameObject Ball;

    public void Score(string WallID)
    {
        if (WallID == "RightWall")
        {
            PlayerScore1++;
        }
        if (WallID == "LeftWall")
        {
            PlayerScore2++;
        }
    }

    public void UpdateScore1(int i)
    {
        GUI.Label(new Rect(Screen.width / 2 - 220, 30, 100, 100), "" + i);
    }

    public void UpdateScore2(int i)
    {
        GUI.Label(new Rect(Screen.width / 2 + 260 - 100, 30, 100, 100), "" + i);
    }

    void OnGUI()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball");
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 220, 30, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 260 - 100, 30, 100, 100), "" + PlayerScore2);

        if (PlayerScore1 == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 400, 300, 2000, 1000), "PLAYER ONE WINS");
            Ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (PlayerScore2 == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 400, 300, 2000, 1000), "PLAYER TWO WINS");
            Ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }
}
