using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace AutocloseEventNotifications
{
	public class Controller : Mod
	{
		private List<LetterDef> letterDefs;

		private string labelTimer;
		private string labelShowMessage;
		private string descShowMessage;
		private string labelCloseLetterType;
		private string descCloseLetterType;

		private Vector2 scrollPosition = Vector2.zero;

		public Controller(ModContentPack content) : base(content)
		{
			GetSettings<Settings>();
		}

		public override void DoSettingsWindowContents(Rect inRect)
		{
			//Can't assign values on startup because mods are loaded too early
			if (this.labelTimer == default(string))
			{
				this.letterDefs = DefDatabase<LetterDef>.AllDefsListForReading;

				this.labelTimer = "ACEN_ACENTimer_Label".Translate();
				this.labelShowMessage = "ACEN_ShowMessage_Label".Translate();
				this.descShowMessage = "ACEN_ShowMessage_Desc".Translate();
				this.labelCloseLetterType = "ACEN_CloseType_Label".Translate();
				this.descCloseLetterType = "ACEN_CloseType_Desc".Translate();
			}

			Text.Font = GameFont.Small;
			float buttonHeight = Text.LineHeight;

			Rect listRect = new Rect(inRect.x, inRect.y + 20f, inRect.width - 20f, (buttonHeight + 20f) * (2 + this.letterDefs.Count));

			Widgets.BeginScrollView(inRect, ref this.scrollPosition, listRect, true);

			Vector2 mousePosition = Event.current.mousePosition;

			float currentY = listRect.y;

			float rectsX = listRect.x;
			float rectsWidth = listRect.width;

			Rect timerRect = new Rect(rectsX, currentY, rectsWidth, buttonHeight);
			Rect minusButtonRect = new Rect(timerRect.xMax - rectsWidth / 2f, currentY, buttonHeight, buttonHeight);
			Rect sliderRect = new Rect(minusButtonRect.xMax + 10f, currentY, rectsWidth / 2f - (2 * buttonHeight + 20f), buttonHeight);
			Rect plusButtonRect = new Rect(sliderRect.xMax + 10f, currentY, buttonHeight, buttonHeight);

			//Text label for timer settings (shows the timer integer in colour)
			Text.Anchor = TextAnchor.MiddleLeft;
			Widgets.Label(timerRect, string.Format(this.labelTimer, Settings.ACENTimer));
			Text.Anchor = TextAnchor.UpperLeft; //Reset

			//Increment timer value by -1 (button).
			if (Widgets.ButtonText(minusButtonRect, "-", true, false, true) && Settings.ACENTimer > 0)
			{
				Settings.ACENTimer--;
			}

			//Set timer value (slider).
			Settings.ACENTimer = Mathf.RoundToInt(Widgets.HorizontalSlider(sliderRect, Settings.ACENTimer, 0, 120, true));

			//Increment timer value by +1 (button).
			if (Widgets.ButtonText(plusButtonRect, "+", true, false, true) && Settings.ACENTimer < 120)
			{
				Settings.ACENTimer++;
			}

			currentY += buttonHeight + 20f;

			Rect showMessageRect = new Rect(rectsX, currentY, rectsWidth, buttonHeight);

			Widgets.CheckboxLabeled(showMessageRect, this.labelShowMessage, ref Settings.ShowMessage);

			if (showMessageRect.Contains(mousePosition))
			{
				Widgets.DrawHighlight(showMessageRect);
				TooltipHandler.TipRegion(showMessageRect, this.descShowMessage);
			}

			for (int i = 0; i < this.letterDefs.Count; i++)
			{
				currentY += buttonHeight + 20f;

				LetterPrefs pref = Settings.PrefByLetterDef(this.letterDefs[i]);

				Rect curPrefRect = new Rect(rectsX, currentY, rectsWidth, buttonHeight);

				Widgets.CheckboxLabeled(curPrefRect, string.Format(this.labelCloseLetterType, pref.colouredLabel), ref pref.closePreference);

				if (curPrefRect.Contains(mousePosition))
				{
					Widgets.DrawHighlight(curPrefRect);
					TooltipHandler.TipRegion(curPrefRect, this.descCloseLetterType);
				}
			}

			Widgets.EndScrollView();
		}

		public override string SettingsCategory()
		{
			return "Autoclose Event Notifications";
		}
	}
}
