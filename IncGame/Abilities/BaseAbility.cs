using UnityEngine;
using System.Collections;

public class BaseAbility  {

	private string name;
	private string description;

	private float damageLowerLim;
	private float damageUpperLim;
	private float calorieCost;

	private bool requiresTarget;
	private bool canCastOnSelf;

	private bool statEffectMirin;
	private bool statEffectSore;
	private bool statEffectBlind;
	private bool statEffectKnockedDown;
	private bool statEffectPumped;



public BaseAbility (string aname, string adescription, float adamageLowerLim, float adamageUpperLim, float aCalorieCost)
	{
		name = aname;
		description = adescription;
		damageLowerLim = adamageLowerLim;
		damageUpperLim = adamageUpperLim;
		calorieCost = aCalorieCost;
	}

	public string AbilityName {
		get { return name;}
	}

	public string AbilityDescription {
		get { return description;}
	}

	public float AbilityDamageLowerLim {
		get { return damageLowerLim;}
	}

	public float AbilityDamageUpperLim {
		get { return damageUpperLim;}
	}

	public float AbilityCalorieCost {
		get { return calorieCost;}
	}


}
