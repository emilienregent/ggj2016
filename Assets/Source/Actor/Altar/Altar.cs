using System;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public void OnActivate()
    {
        Game.instance.currentPlayer.score += Game.instance.currentPlayer.getPointsMinions();
        Game.instance.currentPlayer.portrait.UpdateScore(Game.instance.currentPlayer.score);
        Game.instance.currentPlayer.KillAllMinions();
    }

    public void OnHide()
    {
        Game.instance.EndTurn();
    }
}