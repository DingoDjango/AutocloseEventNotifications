using HugsLib.Utils;
using System.Collections.Generic;
using System.Reflection;
using RimWorld;
using Verse;

namespace AutocloseEN
{
	public class AutocloseEventBoxes : MapComponent
	{
		//Get the private letter list from LetterStack
		static readonly FieldInfo letterStack = typeof(LetterStack).GetField("letters", BindingFlags.NonPublic | BindingFlags.Instance);

		internal static int ACENTimer = 12 * GenDate.TicksPerHour; //12 in-game hours by default

		internal static bool ShowMessage = false;

		internal static bool CloseGood = true;

		internal static bool CloseNonUrgent = true;

		internal static bool CloseUrgent = false;

		internal static Dictionary<Letter, int> letterSpawnTicks = new Dictionary<Letter, int>();

		public AutocloseEventBoxes(Map map) : base(map)
		{
			this.EnsureIsActive();
		}

		public override void MapComponentTick()
		{
			if (Find.TickManager.TicksGame % GenDate.TicksPerHour == 0)
			{
				var letters = letterStack.GetValue(Find.LetterStack) as List<Letter>;

				if (letters.Count > 0)
				{
					for (int i = letters.Count - 1; i >= 0; i--)
					{
						var letter = letters[i];
						int arrivalTick;

						if (!letterSpawnTicks.TryGetValue(letter, out arrivalTick))
						{
							arrivalTick = Find.TickManager.TicksGame;
							letterSpawnTicks[letter] = arrivalTick;
						}

						var deltaTick = Find.TickManager.TicksGame - arrivalTick;
						string userNotification = "Autoclose Event Notifications: " + "ACEN_message_part1".Translate() + " '" + letter.label + "' " + "ACEN_message_part2".Translate();

						if (deltaTick >= ACENTimer)
						{
							if (letter.LetterType == LetterType.Good && CloseGood)
							{
								Find.LetterStack.RemoveLetter(letter);

								if (ShowMessage)
								{
									Messages.Message(userNotification, MessageSound.Silent);
								}
							}

							if (letter.LetterType == LetterType.BadNonUrgent && CloseNonUrgent)
							{
								Find.LetterStack.RemoveLetter(letter);

								if (ShowMessage)
								{
									Messages.Message(userNotification, MessageSound.Silent);
								}
							}

							if (letter.LetterType == LetterType.BadUrgent && CloseUrgent)
							{
								Find.LetterStack.RemoveLetter(letter);

								if (ShowMessage)
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