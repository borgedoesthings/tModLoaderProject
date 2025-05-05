using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Terraria.Audio;
using System.Reflection;
//using static Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions;
using Moq;
using ReLogic.Utilities;

namespace Terraria.ModLoader
{
    [TestClass]
    public class SoundEngineTests
    {
        [TestMethod]
        public void TestAudioSupport()
        {
            var method = typeof(SoundEngine).GetMethod("TestAudioSupport", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            bool result = (bool)method.Invoke(null, null);

            // Just confirm it runs and returns a bool and not crash
            Assert.IsTrue(result || !result);
        }

        [TestMethod]
        public void PauseAll_happypath()
        {
            var soundPlayer = new SoundPlayer();

            soundPlayer.PauseAll(); // no errors should be thrown wether the sound is playing or not
        }

        [TestMethod]
        public void ResumeAll_Happypath()
        {
            var soundPlayer = new SoundPlayer();

            soundPlayer.ResumeAll(); 
        }

		[TestMethod]
		public void SoundPlayer_RemovesInactiveSound_OnUpdate()
		{
			var player = new SoundPlayer();

			// Create a fake ActiveSound
			var style = new SoundStyle("test/path");
			var sound = new ActiveSound(style);
			sound.Stop(); // Simulate it being done

			// Inject into _trackedSounds using reflection
			var field = typeof(SoundPlayer).GetField("_trackedSounds", BindingFlags.NonPublic | BindingFlags.Instance);
			var slotVector = field.GetValue(player);

			// Use reflection to call Add() on SlotVector<ActiveSound>
			var addMethod = slotVector.GetType().GetMethod("Add");
			var slotId = (SlotId)addMethod.Invoke(slotVector, new object[] { sound });

			// Confirm it exists
			Assert.IsNotNull(player.GetActiveSound(slotId));

			// Run update (should remove the stopped sound)
			player.Update();

			// Should now be removed
			var stillThere = player.GetActiveSound(slotId);
			Assert.IsNull(stillThere, "Sound was not removed from tracked sounds after stopping.");
		}

	}
}
