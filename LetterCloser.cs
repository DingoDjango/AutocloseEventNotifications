using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AutocloseEventNotifications
{
	public class LetterCloser : GameComponent
	{
		public override void GameComponentTick()
		{
			int ticksGame = Find.TickManager.TicksGame;

            if (ticksGame % GenDate.TicksPerHour == 0)
			{
#if DEBUG
                Log.Message($"AutocloseEventNotifications :: ticksGame == {ticksGame}, checking letter stack.");
#endif

                List<Letter> activeLetters = Find.LetterStack.LettersListForReading;

				if (activeLetters.Count > 0)
				{
					for (int i = activeLetters.Count - 1; i >= 0; i--)
					{
						Letter letter = activeLetters[i];
#if DEBUG
                        Log.Message($"AutocloseEventNotifications :: checking {letter.Label}, arrived at {letter.arrivalTick}");
#endif

                        if (letter.def.pauseMode > Settings.LetterTypesToKeep &&
							ticksGame - letter.arrivalTick >= (Settings.HoursToClose * GenDate.TicksPerHour))
						{
#if DEBUG
                            Log.Message($"AutocloseEventNotifications :: Close letter at {ticksGame} ticks, arrived at {letter.arrivalTick}.");
#endif

                            Find.LetterStack.RemoveLetter(letter);

							if (Settings.ShowMessages)
							{
								Messages.Message(string.Format("ACEN.RemovedLetter".Translate(), letter.Label), MessageTypeDefOf.SilentInput);
							}
						}
					}
				}
			}
		}

		public LetterCloser()
		{
#if DEBUG
            Log.Message($"AutocloseEventNotifications :: initiated game component");
#endif
        }

        public LetterCloser(Game game)
		{
#if DEBUG
            Log.Message($"AutocloseEventNotifications :: initiated game component");
#endif
        }
    }
}
