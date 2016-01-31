using System;
using UnityEngine;
using Type;

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
        Game.instance.PrepareEndTurn();
    }

    private void Update()
    {
        if (_isActivated == true && _timer.IsFinished() == true)
        {
            _isActivated = false;

            for (int i = 0; i < Game.instance.players.Count; i++)
            {
                Player player = Game.instance.players[i];
                Tile tile = Game.instance.mapManager.map.tiles[player.tileIndex];

                if (tile.type == (int)TileType.ALTAR)
                {
                    player.score += player.getPointsMinions();
                    player.portrait.UpdateScore(player.score);
                    player.KillAllMinions();
                }
            }
        }
    }
}