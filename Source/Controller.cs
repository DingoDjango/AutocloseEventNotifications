using UnityEngine;
using Verse;

namespace AutocloseEN
{
	public class Controller : Mod
	{
		public Controller(ModContentPack content) : base(content)
		{
			GetSettings<Settings>();
		}

		public override void WriteSettings()
		{
			base.WriteSettings();
		}

		internal IntRange timerRange = new IntRange(0, 999);

		public override void DoSettingsWindowContents(Rect inRect)
		{
			Listing_Standard list = new Listing_Standard();
			list.ColumnWidth = inRect.width;
			list.Begin(inRect);
			list.Gap();
			list.Label("ACEN_setting_timetoClose_label".Translate(new object[]
			{
				Settings.ACENTimer
			}));
			Settings.ACENTimer = Mathf.RoundToInt(list.Slider(Settings.ACENTimer, 0, 120));
			list.Gap();
			list.CheckboxLabeled("ACEN_setting_showMessage_label".Translate(), ref Settings.ShowMessage, "ACEN_setting_showMessage_desc".Translate());
			list.Gap();
			list.CheckboxLabeled("ACEN_setting_closeGood_label".Translate(), ref Settings.CloseGood, "ACEN_setting_closeGood_desc".Translate());
			list.Gap();
			list.CheckboxLabeled("ACEN_setting_closeNonUrgent_label".Translate(), ref Settings.CloseNonUrgent, "ACEN_setting_closeNonUrgent_desc".Translate());
			list.Gap();
			list.CheckboxLabeled("ACEN_setting_closeUrgent_label".Translate(), ref Settings.CloseUrgent, "ACEN_setting_closeUrgent_desc".Translate());
			list.End();
		}

		public override string SettingsCategory()
		{
			return "ACEN".Translate();
		}
	}
}