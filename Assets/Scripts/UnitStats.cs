using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour 
{
	//whole script is just a data container to store each unit's statistics;
	//variables are public for them to be editable by other scripts
	public string UnitType;
	//Every unit in the game will have 20 HP at start
	public const double MaxHP = 20.0;
	public double CurrentHP = 20.0;
	//Attack and defense ratings are multiplied by current HP
	private double AttPerHP, DefPerHP;
	private int BaseSpeed;
	//Modificators
	public double AttBonus, DefBonus;
	public int SpeedBonus;
	//Attack and defense final values
	public double Attack, Defense;
	public int Speed;
	//Range indicates max distance between attacker and defender; most probably it will change
	public double Range;

	//SPECIALS
	public bool isCounterAttack = true;


	void Start()
	{
		Attack = AttPerHP * CurrentHP + AttBonus;
		Defense = DefPerHP * CurrentHP + DefBonus;
		Speed = BaseSpeed + SpeedBonus;

		switch(UnitType)
		{
			case ("Spearman"):
				AttPerHP = 3.0;
				DefPerHP = 3.0;
				BaseSpeed = 3;
				Range = 1.0;
				break;

			case ("Heavy"):
				AttPerHP = 3.0;
				DefPerHP = 6.0;
				BaseSpeed = 3;
				Range = 1.0;
				break;

			case ("Archer"):
				AttPerHP = 5.0;
				DefPerHP = 3.0;
				BaseSpeed = 3;
				Range = 3.0;
				break;

		default:
			Debug.Log ("Invalid Unit Type, check if given names are correct or unit name is assigned");
			break;
		}
			
	}
		
}
