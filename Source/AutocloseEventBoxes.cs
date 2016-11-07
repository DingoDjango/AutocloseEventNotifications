using System.Collections.Generic;
using System.Reflection;
using RimWorld;
using Verse;

namespace AutocloseEN
{
	public class AutocloseEventBoxes : MapComponent
	{
		static readonly FieldInfo letterStack = typeof(LetterStack).GetField("letters", BindingFlags.NonPublic | BindingFlags.Instance);

		internal static bool ShowMessage = false;

		internal static bool CloseGood = true;

		internal static bool CloseNonUrgent = true;

		internal static bool CloseUrgent = false;

		internal static Dictionary<Letter, int> letterSpawnTicks = new Dictionary<Letter, int>();

		internal static float ACENTimer = 12 * GenDate.TicksPerHour; //12 in-game hours by default

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

						if (deltaTick >= ACENTimer)
						{
							if (letters[i].LetterType == LetterType.Good && CloseGood == true)
							{
								Find.LetterStack.RemoveLetter(letters[i]);
								if (ShowMessage)
								{
									Messages.Message("Autoclose Event Notifications removed a positive (blue) notification.", MessageSound.Silent);
								}
							}
							else if (letters[i].LetterType == LetterType.BadNonUrgent && CloseNonUrgent == true)
							{
								Find.LetterStack.RemoveLetter(letters[i]);
								if (ShowMessage)
								{
									Messages.Message("Autoclose Event Notifications removed a negative (yellow) notification.", MessageSound.Silent);
								}
							}
							else if (letters[i].LetterType == LetterType.BadUrgent && CloseUrgent == true)
							{
								Find.LetterStack.RemoveLetter(letters[i]);
								if (ShowMessage)
								{
									Messages.Message("Autoclose Event Notifications removed an urgent (red) notification.", MessageSound.Silent);
								}
							}
						}
					}
				}
			}
		}
	}
}
