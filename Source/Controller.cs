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

		public override void DoSettingsWindowContents(Rect inRect)
		{
			Listing_Standard list = new Listing_Standard();
			list.ColumnWidth = inRect.width;
			list.Begin(inRect);

			list.Gap(20f);

			{
				Rect currentRect = list.GetRect(Text.LineHeight);
				Rect currentRectLeft = currentRect.LeftHalf().Rounded();
				Rect currentRectRight = currentRect.RightHalf().Rounded();
				string ACENTimer_label = "ACEN_setting_timetoClose_label".Translate(new object[] { Settings.ACENTimer });

				//Text label for timer settings, translated.
				Widgets.Label(currentRectLeft, ACENTimer_label);

				//Increment timer value by -1 (button).
				if (Widgets.ButtonText(new Rect(currentRectRight.xMin, currentRectRight.y, currentRectRight.height, currentRectRight.height), "-", true, false, true))
				{
					if (Settings.ACENTimer <= 120 && Settings.ACENTimer > 0)
					{
						Settings.ACENTimer--;
					}
				}

				//Set timer value (slider).
				Settings.ACENTimer = Mathf.RoundToInt(Widgets.HorizontalSlider(new Rect(currentRectRight.xMin + currentRectRight.height + 10f, currentRectRight.y, currentRectRight.width - (currentRectRight.height * 2 + 20f), currentRectRight.height), Settings.ACENTimer, 0, 120, true));

				//Increment timer value by +1 (button).
				if (Widgets.ButtonText(new Rect(currentRectRight.xMax - currentRectRight.height, currentRectRight.y, currentRectRight.height, currentRectRight.height), "+", true, false, true))
				{
					if (Settings.ACENTimer < 120 && Settings.ACENTimer >= 0)
					{
						Settings.ACENTimer++;
					}
				}
			}

			list.Gap(20f);

			list.CheckboxLabeled("ACEN_setting_showMessage_label".Translate(), ref Settings.ShowMessage, "ACEN_setting_showMessage_desc".Translate());

			list.Gap(20f);

			list.CheckboxLabeled("ACEN_setting_closeGood_label".Translate(), ref Settings.CloseGood, "ACEN_setting_closeGood_desc".Translate());

			list.Gap(20f);

			list.CheckboxLabeled("ACEN_setting_closeNonUrgent_label".Translate(), ref Settings.CloseNonUrgent, "ACEN_setting_closeNonUrgent_desc".Translate());

			list.Gap(20f);

			list.CheckboxLabeled("ACEN_setting_closeUrgent_label".Translate(), ref Settings.CloseUrgent, "ACEN_setting_closeUrgent_desc".Translate());

			list.End();
		}

		public override string SettingsCategory()
		{
			return "ACEN".Translate();
		}
	}
}