using System.Collections.Generic;
using RimWorld;
using Verse;
using System.Reflection;
using RimWorld.Planet;

namespace AutocloseEN
{
	public class AutocloseEventBoxes : WorldComponent
	{
		//Get the private letter list from LetterStack
		static readonly FieldInfo letterStack = typeof(LetterStack).GetField("letters", BindingFlags.NonPublic | BindingFlags.Instance);

		//Dictionary keeps track of letter arrival times
		internal static Dictionary<Letter, int> letterSpawnTicks = new Dictionary<Letter, int>();

		public AutocloseEventBoxes(World world) : base(world)
		{
		}

		public override void WorldComponentTick()
		{
			if (Find.TickManager.TicksGame % GenDate.TicksPerHour == 0)
			{
				var activeLetters = letterStack.GetValue(Find.LetterStack) as List<Letter>;

				if (activeLetters.Count > 0)
				{
					for (int i = activeLetters.Count - 1; i >= 0; i--)
					{
						var letter = activeLetters[i];
						int arrivalTick;

						if (!letterSpawnTicks.TryGetValue(letter, out arrivalTick))
						{
							arrivalTick = Find.TickManager.TicksGame;
							letterSpawnTicks[letter] = arrivalTick;
						}

						var deltaTick = Find.TickManager.TicksGame - arrivalTick;

						if (deltaTick >= (Settings.ACENTimer * GenDate.TicksPerHour))
						{
							string userNotification = "ACEN".Translate() + ": " + "ACEN_message_part1".Translate() + " '" + letter.label + "' " + "ACEN_message_part2".Translate();

							if (letter.def.defName == "Good" && Settings.CloseGood)
							{
								Find.LetterStack.RemoveLetter(letter);

								if (Settings.ShowMessage)
								{
									Messages.Message(userNotification, MessageSound.Silent);
								}
							}

							if (letter.def.defName == "BadNonUrgent" && Settings.CloseNonUrgent)
							{
								Find.LetterStack.RemoveLetter(letter);

								if (Settings.ShowMessage)
								{
									Messages.Message(userNotification, MessageSound.Silent);
								}
							}

							if (letter.def.defName == "BadUrgent" && Settings.CloseUrgent)
							{
								Find.LetterStack.RemoveLetter(letter);

								if (Settings.ShowMessage)
								{
									Messages.Message(userNotification, MessageSound.Silent);
								}
							}
						}
					}
				}
			}
		}
	}
}