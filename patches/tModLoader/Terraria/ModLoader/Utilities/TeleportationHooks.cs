using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Terraria.ModLoader.Utilities
{
    public class TeleportationHooks
    {
		// static method to allow modders to disable teleportation to certain tiles, eg progression locked tiles
		// by default ive made it check for a few contions first: inside map, on a damaging tile, on a deactivated tile
		public static bool CanTeleportToTile(int TileX, int TileY)
		{
			if (TileX < 0 || TileY < 0 || TileX >= Main.maxTilesX || TileY >= Main.maxTilesY) {
				// false if outside map bounds
				return false;
			}

			Tile tile = Main.tile[TileX, TileY];
			if (tile == null || !tile.active())
			{
				// false if tile is inactive/deactuated
				return false;
			}

			if (IsDangerousTile(Main.tile[TileX, TileY].TileType))
			{
				// false if tile is dangerous(lava, spikes, traps etc.)
				return false;
			}

			// by default returns true unless one of those conditionals is met
			return true;
		}
		private static bool IsDangerousTile(ushort tileType)
		{
			return tileType == TileID.Spikes ||
				   tileType == TileID.WoodenSpikes ||
				   tileType == TileID.Hellstone ||
				   tileType == TileID.Meteorite;
		}
	}
}
