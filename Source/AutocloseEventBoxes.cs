using System.Collections.Generic;
using System.Reflection;
using Verse;
using UnityEngine;

namespace AutocloseEN
{
	public class AutocloseEventBoxes : MapComponent
	{
		static readonly FieldInfo letterStack = typeof(LetterStack).GetField("letters", BindingFlags.NonPublic | BindingFlags.Instance);

		internal static bool ShowMessage = false;

		internal static bool CloseGood = true;

		internal static bool CloseNonUrgent = true;

		internal static bool CloseUrgent = false;

        internal static float ACENTimer = 36f;

		public override void MapComponentTick()
		{
			if (Find.TickManager.TicksGame % 3600 == 0)
			{
				var letters = letterStack.GetValue(Find.LetterStack) as List<Letter>;
				if (letters.Count > 0)
				{
					for (int i = 0; i < letters.Count; i++)
					{
						float timeactive = Time.time - letters[i].arrivalTime;
						if (timeactive >= ACENTimer)
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
