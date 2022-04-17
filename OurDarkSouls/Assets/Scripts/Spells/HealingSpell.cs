using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
	[CreateAssetMenu(menuName = "Spells/Healing Spell")]
	public class HealingSpell: SpellItem
	{
		public int healAmount;

		public override void AttemptToCastSpell(AnimatorHadler animatorHandler, PlayerStats playerStats)
		{
			GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
			animatorHandler.PlayTargetAnimation(spellAnimation, true);
			Debug.Log("Attempting to cast spell...");
		}

		public override void SuccessfullyCastSpell(AnimatorHadler animatorHandler, PlayerStats playerStats)
		{
			GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
			playerStats.HealPlayer(healAmount);
			Debug.Log("Spell cast successful");			
		}
	}
}