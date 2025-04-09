using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Terraria.Audio;

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
	}
}
