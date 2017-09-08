using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AutocloseEventNotifications
{
	public class Settings : ModSettings
	{
		private static List<LetterPrefs> letterPrefs = new List<LetterPrefs>();

		private static Dictionary<LetterDef, LetterPrefs> letterPrefsLookup = new Dictionary<LetterDef, LetterPrefs>();

		internal static int ACENTimer = 12; //Represents in-game hours

		internal static bool ShowMessage = false;

		public static LetterPrefs PrefByLetterDef(LetterDef def)
		{
			if (!letterPrefsLookup.TryGetValue(def, out LetterPrefs pref))
			{
				pref = letterPrefs.Find(lp => lp.defName == def.defName);

				if (pref == null)
				{
					bool closeStatus = false;

					if (def == LetterDefOf.Good || def == LetterDefOf.BadNonUrgent)
					{
						closeStatus = true;
					}

					pref = new LetterPrefs(def, closeStatus);

					letterPrefs.Add(pref);
				}

				letterPrefsLookup[def] = pref;
			}

			return pref;
		}

		public override void ExposeData()
		{
			base.ExposeData();

			Scribe_Values.Look(ref ACENTimer, "ACENTimer", 12);
			Scribe_Values.Look(ref ShowMessage, "ShowMessage", false);
			Scribe_Collections.Look(ref letterPrefs, "letterPrefs", LookMode.Deep, new object[0]);
		}
	}
}
