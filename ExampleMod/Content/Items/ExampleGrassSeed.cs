using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace ExampleMod.Content.Items
{
	public class ExampleGrassSeed : ModItem
	{

		public override void SetDefaults() {
			Item.width = 16;
			Item.height = 16;
			Item.useTime = 10;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.White;
			Item.value = 100;
			Item.UseSound = SoundID.Grass;
		}

		public override bool? UseItem(Player player) {
			int i = Player.tileTargetX;
			int j = Player.tileTargetY;
			Tile tile = Framing.GetTileSafely(i, j);

			SoundEngine.PlaySound(SoundID.Grass, player.Center);

			Point tilePos = Main.MouseWorld.ToTileCoordinates();
			Vector2 worldPos = new Vector2(tilePos.X * 16, tilePos.Y * 16);

			// Dust effect
			for (int k = 0; k < 8; k++) {
				Dust.NewDust(worldPos, 16, 16, DustID.Grass,
					Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-2f, 0f));
			}

			// Only replace regular dirt
			if (tile.HasTile && tile.TileType == TileID.Dirt) {
				WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.ExampleGrass>(), mute: true, forced: true);
				NetMessage.SendTileSquare(-1, i, j, 1); // sync for multiplayer
				return true;
			}

			return false;
		}

		public override bool CanUseItem(Player player) {
			int i = Player.tileTargetX;
			int j = Player.tileTargetY;
			Tile tile = Framing.GetTileSafely(i, j);
			return tile.HasTile && tile.TileType == TileID.Dirt;
		}
	}
}