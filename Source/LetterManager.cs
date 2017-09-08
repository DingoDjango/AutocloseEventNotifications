using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AutocloseEventNotifications
{
	public class LetterManager : GameComponent
	{
		private readonly string messageText = "ACEN_MessageText".Translate();

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

						if (ticksGame - arrivalTick >= (Settings.ACENTimer * GenDate.TicksPerHour) && Settings.PrefByLetterDef(letter.def).closePreference)
						{
							Find.LetterStack.RemoveLetter(letter);

							if (Settings.ShowMessage)
							{
								Messages.Message(string.Format(this.messageText, letter.label), MessageSound.Silent);
							}
						}
					}
				}
			}
		}

		//Empty constructors due to A17 bug
		public LetterManager()
		{
		}

		public LetterManager(Game game)
		{
		}
	}
}
