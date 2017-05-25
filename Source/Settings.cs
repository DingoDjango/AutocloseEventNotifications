using Verse;

namespace AutocloseEN
{
	public class Settings : ModSettings
	{
		internal static int ACENTimer = 12; //12 in-game hours by default

		internal static bool ShowMessage = false;

		internal static bool CloseGood = true;

		internal static bool CloseNonUrgent = true;

		internal static bool CloseUrgent = false;

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref ACENTimer, "ACENTimer", 12);
			Scribe_Values.Look(ref ShowMessage, "ShowMessage", false);
			Scribe_Values.Look(ref CloseGood, "CloseGood", true);
			Scribe_Values.Look(ref CloseNonUrgent, "CloseNonUrgent", true);
			Scribe_Values.Look(ref CloseUrgent, "CloseUrgent", false);
		}
	}
}