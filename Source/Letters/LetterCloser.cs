using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AutocloseEventNotifications
{
	public class LetterCloser : GameComponent
	{
		private Dictionary<Letter, int> letterSpawnTicks = new Dictionary<Letter, int>();

		public override void GameComponentTick()
		{
			int ticksGame = Find.TickManager.TicksGame;

			if (ticksGame % GenDate.TicksPerHour == 0)
			{
				List<Letter> activeLetters = Find.LetterStack.LettersListForReading;

				if (activeLetters.Count > 0)
				{
					for (int i = activeLetters.Count - 1; i >= 0; i--)
					{
						Letter letter = activeLetters[i];

						if (!this.letterSpawnTicks.TryGetValue(letter, out int arrivalTick))
						{
							arrivalTick = ticksGame;
							this.letterSpawnTicks[letter] = ticksGame;
						}

						if (ticksGame - arrivalTick >= (Controller.Timer * GenDate.TicksPerHour) && Controller.PrefByDef[letter.def])
						{
							Find.LetterStack.RemoveLetter(letter);

							if (Controller.ShowMessages)
							{
								Messages.Message(string.Format("ACEN_RemovedLetter".Translate(), letter.label), MessageTypeDefOf.SilentInput);
							}
						}
					}
				}
			}
		}

		public LetterCloser()
		{
		}

		public LetterCloser(Game game)
		{
		}
	}
}
