using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AutocloseEventNotifications
{
    public class Settings : ModSettings
    {
        private static string hoursToCloseTextBuffer = "12"; // Unsaved

        public static bool ShowMessages = false;

        public static AutomaticPauseMode LetterTypesToKeep = AutomaticPauseMode.AnyThreat;

        public static int HoursToClose = 12;

        private static string ToStringHumanModded(AutomaticPauseMode mode)
        {
            if (mode == AutomaticPauseMode.Never)
            {
                return "ACEN.AutomaticPauseMode_Never".Translate();
            }

            return mode.ToStringHuman();
        }

        public static void DoSettingsWindowContents(Rect rect)
        {
            IEnumerable<AutomaticPauseMode> allModes = Enum.GetValues(typeof(AutomaticPauseMode)).Cast<AutomaticPauseMode>();

            Listing_Standard modOptions = new Listing_Standard();

            modOptions.Begin(rect);

            modOptions.Gap(20f);

            modOptions.CheckboxLabeled("ACEN.ShowMessages".Translate(), ref ShowMessages, "ACEN.ShowMessages.Tooltip".Translate());
            modOptions.Gap(20f);

            // Modified from Dialog_Options.DoGameplayOptions
            if (modOptions.ButtonTextLabeledPct("ACEN.LettersToKeep".Translate(), Settings.ToStringHumanModded(LetterTypesToKeep), 0.6f, TextAnchor.MiddleLeft, null, "ACEN.LettersToKeep.Tooltip".Translate()))
            {
                List<FloatMenuOption> letterTypes = new List<FloatMenuOption>();

                foreach (AutomaticPauseMode pauseMode in allModes)
                {
                    letterTypes.Add(new FloatMenuOption(Settings.ToStringHumanModded(pauseMode), delegate ()
                    {
                        Settings.LetterTypesToKeep = pauseMode;
                    }));
                }

                Find.WindowStack.Add(new FloatMenu(letterTypes));
            }

            modOptions.Gap(20f);

            Rect timerRect = modOptions.GetRect(Text.LineHeight);
            Rect timerLabelRect = timerRect.LeftPart(0.75f);
            Rect timerInputRect = timerRect.RightPart(0.20f);

            Widgets.Label(timerLabelRect, "ACEN.Timer".Translate());
            Widgets.DrawHighlightIfMouseover(timerLabelRect);
            TooltipHandler.TipRegion(timerLabelRect, "ACEN.Timer.Tooltip".Translate());
            Widgets.TextFieldNumeric(timerInputRect, ref HoursToClose, ref hoursToCloseTextBuffer);

            modOptions.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref ShowMessages, "ACEN_ShowMessages", false);
            Scribe_Values.Look(ref LetterTypesToKeep, "ACEN_LetterTypesToKeep", AutomaticPauseMode.AnyThreat);
            Scribe_Values.Look(ref HoursToClose, "ACEN_HoursToClose", 12);

            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                // Set unsaved values
                hoursToCloseTextBuffer = HoursToClose.ToString();
            }
        }
    }
}
