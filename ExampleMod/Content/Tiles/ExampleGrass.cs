using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ID.ContentSamples.CreativeHelper;

namespace ExampleMod.Content.Tiles
{
	public class ExampleGrass : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMerge[Type][TileID.Dirt] = true;
			Main.tileMerge[TileID.Dirt][Type] = true;
			Main.tileBlendAll[Type] = true;
			Main.tileLighted[Type] = true;

			// Tells the game it behaves like grass (frame-wise)
			Main.tileFrameImportant[Type] = false;

			// Optional: if you want to allow placing other stuff (like vines)
			TileID.Sets.Grass[Type] = true;

			AddMapEntry(new Color(100, 200, 100), CreateMapEntryName());

			// Use default sound when mined
			DustType = DustID.Grass;
			RegisterItemDrop(ItemID.DirtBlock);
		}

		// Spreading logic
		public override void RandomUpdate(int i, int j) {
			// Try spreading to nearby dirt tiles
			int x = i + Main.rand.Next(-1, 2);
			int y = j + Main.rand.Next(-1, 2);

			Tile target = Framing.GetTileSafely(x, y);
			Tile above = Framing.GetTileSafely(x, y - 1);

			if (target.HasTile && target.TileType == TileID.Dirt && !above.HasTile) {
				WorldGen.PlaceTile(x, y, Type, mute: true);

				if (Main.netMode == NetmodeID.Server)
					NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
	}
}