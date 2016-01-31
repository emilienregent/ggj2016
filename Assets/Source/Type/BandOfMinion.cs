using System;
using System.Collections.Generic;
using UnityEngine;

namespace Type
{
    public class BandOfMinion
    {
        private List<Minion> _minions = new List<Minion>();

        public BandOfMinion(Tile tile, int count = 0)
        {
            int quantity = tile.quantity > 0 ? tile.quantity : count;
                
            switch((TileType) tile.type)
            {
                case TileType.MINION_RED:
                    Initialize(MinionColor.RED, quantity);
                    break;
                case TileType.MINION_BLUE:
                    Initialize(MinionColor.BLUE, quantity);
                    break;
                case TileType.MINION_GREEN:
                    Initialize(MinionColor.GREEN, quantity);
                    break;
                case TileType.MINION_YELLOW:
                    Initialize(MinionColor.YELLOW, quantity);
                    break;
            }

            if (_minions.Count > 0)
            {
                AnchorToTile(tile);
            }
        }

        private void Initialize(MinionColor color, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Minion minion = Game.instance.CreateMinion(color);

                _minions.Add(minion);
            }
        }

        public void AnchorToTile(Tile tile)
        {
            for (int i = 0; i < _minions.Count; i++)
            {
                Minion minion = _minions[i];

                minion.anchor = tile.gameObject.transform;
                minion.transform.position = tile.gameObject.transform.position;
                minion.transform.SetParent(tile.gameObject.transform);
            }
        }

        public void AnchorToPlayer(Player player)
        {
            PlayerUI ui = player.portrait.GetComponent<PlayerUI>();
            for (int i = 0; i < _minions.Count; i++)
            {
                Minion minion = _minions[i];

                if (player.CanAddMinion() == true)
                {
                    player.AddMinion(minion);
                    ui.FillMinionSlots(player.minions);
                }
                else
                {
                    minion.Kill();
                }
            }
        }
    }
}

