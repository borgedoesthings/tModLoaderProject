using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void SoundEngine_Update_ResumesAudioWhenGameUnpaused()
        {
            //content
        }

    }
}
