using System;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private Timer   _timer          = new Timer(0f);
    private bool    _isActivated    = false;

    public void OnShow()
    {
        _isActivated = true;
        _timer.duration = 0.25f;
        _timer.Start();
    }

    public void OnHide()
    {
        Game.instance.EndTurn();
    }

    private void Update()
    {
        if (_isActivated == true && _timer.IsFinished() == true)
        {
            _isActivated = false;

            Game.instance.currentPlayer.score += Game.instance.currentPlayer.getPointsMinions();
            Game.instance.currentPlayer.portrait.UpdateScore(Game.instance.currentPlayer.score);
            Game.instance.currentPlayer.KillAllMinions();
        }
    }
}