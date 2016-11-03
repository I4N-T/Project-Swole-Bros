using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class AbShove : MonoBehaviour {

    string aname = "Shove";
    string adescription = "Shove the enemy. Does minor damage with a chance to knock down the opponent.";

    float adamageLowerLim = 2f;
    float adamageUpperLim = 4f;
    int aCalorieCost = 300;

    public int damageRange;
    public int damageToDo;
    public int critVal;
    public int critVal2;
    public bool iscrit;
    public bool isKnockDown;
    public int enemyHP;
    public int playerCalories;

    public bool insufficientCalBool;

    /*public AbWeakPunch()
		: base(new BaseAbility(aname, adescription, adamageLowerLim, adamageUpperLim, aCalorieCost)
		       {
			damageToDo = Random.Range(1,5);


		}*/
    public void UseShove()
    {
        playerCalories = GameManager.instance.playerCalories;

        if (playerCalories >= aCalorieCost)
        {
            ShoveCritCheck();
            ShoveDamageMethod();
            ShoveKnockDownCheck();

            if (isKnockDown == true)
            {
                EnemyObjectDisable.instance2.enemyStatsScript.isKnockDown = true;
                //make in an enemy conrolling script, if isKnockDown = true, then enemy can't attack next turn and your next attack does extra damage
            }
        }
        else if (playerCalories < aCalorieCost)
        {
            insufficientCalBool = true;
        }
    }

    public void ShoveCritCheck()
    {

        critVal = GetRandomValue();

        if (critVal >= 4)
        {
            iscrit = false;
        }
        else if (critVal < 4)
        {
            iscrit = true;
        }
    }

    public void ShoveKnockDownCheck()
    {

        critVal2 = GetRandomValue2();

        if (critVal2 == 2)
        {
            isKnockDown = false;
        }
        else if (critVal2 == 1)
        {
            isKnockDown = true;
        }
    }

    int GetRandomValue()
    {
        float rand = Random.value;
        if (rand <= .3f)
            return Random.Range(0, 4);

        return Random.Range(5, 11);
    }

    int GetRandomValue2()
    {
        float rand2 = Random.value;
        if (rand2 <= .35f)
            return 1;

        return 2;
    }

    public void ShoveDamageMethod()
    {

        enemyHP = EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints;

        damageRange = (int)Random.Range(adamageLowerLim, adamageUpperLim);

        if (iscrit == false)
        {
            damageToDo = damageRange;
        }
        else if (iscrit == true)
        {
            damageToDo = (int)(damageRange * 2f);
        }

        enemyHP -= damageToDo;

        GameManager.instance.playerCalories -= aCalorieCost;


        EnemyObjectDisable.instance2.enemyStatsScript.eHitPoints = enemyHP;
    }


}
