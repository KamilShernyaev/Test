using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
	[CreateAssetMenu(menuName = "Spells/Healing Spell")]
	public class HealingSpell: SpellItem
	{
		public int healAmount;

		public override void AttemptToCastSpell(PlayerAnimatorManager playerAnimatorManager, PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
		{
			base.AttemptToCastSpell(playerAnimatorManager, playerStats, weaponSlotManager);
			GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, playerAnimatorManager.transform);
			playerAnimatorManager.PlayTargetAnimation(spellAnimation, true);
			Debug.Log("Attempting to cast spell...");
		}

		public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats)
		{
			base.SuccessfullyCastSpell(animatorHandler, playerStats);
			GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
			playerStats.HealPlayer(healAmount);
			Debug.Log("Spell cast successful");			
		}
	}
}