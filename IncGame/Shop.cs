using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

	private bool openShopBtn;
    private bool closeShopBtn;
	private bool buyConsumablesBtn;
	private bool buyExerciseEquipmentBtn;
    private bool buyThisBtn;
    

	private bool showCatTabs;
	private bool testBool;
    private bool buyThisBool;
    private bool notEnoughGold;
    private bool boughtItem;

    private bool buyEquip;

    public bool hasProteinShake;

    public int consumableSelGridInt = 6;
    public int equipSelGridInt = 6;

    public int playerGold;

    private bool equipBool;

    //SOUNDS
    private AudioClip canNot = new AudioClip();
    //canNot = (AudioClip)Resources.Load("cannot",typeof(AudioClip));

	//"A" refers to consumables
	string[] itemNamesA = new string[5]; 
	int Y = 0;
	int X;

    //"B" refers to equipment
    string[] itemNamesB = new string[5];
    int Y2 = 0;
    int X2;

	// Use this for initialization
	void Start () {
        canNot = (AudioClip)Resources.Load("cannot", typeof(AudioClip));
        //if GameManager object exists, set playerGold = GameManager.instance.playerGold
        //else if GameManager doesn't exist, set playerGold = 0
        GoldSetMethod();
        playerGold = IncGameManager.playerGold;

        itemNamesA[0] = "Protein Shake";
		itemNamesA [1] = "Item B";
		itemNamesA [2] = "Item C";
		itemNamesA [3] = "Item D";
		itemNamesA [4] = "Item E";

        itemNamesB[0] = "Workout Bench";
        itemNamesB[1] = "Squat Rack";
        itemNamesB[2] = "Item C";
        itemNamesB[3] = "Item D";
        itemNamesB[4] = "Item E";

    }

	void OnGUI(){

       /* if (GUI.Toggle(new Rect(200, 55, 150, 50), tabBool, "Consumables"))
            iTabSelected = 0;

        if (GUI.Toggle(new Rect(200, 55, 150, 50), tabBool, "Consumables"))
            iTabSelected = 1;*/

        OpenShopButton ();

		if (showCatTabs == true) {
            MenuBox();
            CloseShopButton();
			ConsumablesButton();
            EquipmentButton();
		}

        if (testBool == true)
        {
            ConsumableSelGridMeth();
            BuyButton();
            if (notEnoughGold == true)
            {
                GUI.Label(new Rect (Screen.width * 0.305f, Screen.height * 0.7f, Screen.width * 0.47f, Screen.height * 0.1f), "You don't have enough gold.");
            }
            if (boughtItem == true)
            {
                GUI.Label(new Rect(Screen.width * 0.305f, Screen.height * 0.7f, Screen.width * 0.47f, Screen.height * 0.1f), "Enjoy your purchase!");
            }
        }

        if (equipBool == true)
        {
            EquipSelGridMeth();
            BuyButton();
            if (notEnoughGold == true)
            {
                GUI.Label(new Rect(Screen.width * 0.305f, Screen.height * 0.7f, Screen.width * 0.47f, Screen.height * 0.1f), "You don't have enough gold.");
            }
            if (boughtItem == true)
            {
                GUI.Label(new Rect(Screen.width * 0.305f, Screen.height * 0.7f, Screen.width * 0.47f, Screen.height * 0.1f), "Enjoy your purchase!");
            }
        }

    }

	void OpenShopButton(){

		openShopBtn = GUI.Button (new Rect (Screen.width * 0.3f ,0,Screen.width * 0.23f ,Screen.height * 0.1f), "Shop");
		if (openShopBtn) {
			showCatTabs = true;	
		}		
	}

    void MenuBox() {
        GUI.Box(new Rect(Screen.width * 0.29f , Screen.height * 0.1f, Screen.width * 0.5f, Screen.height * 0.8f), "Shop");
    }

    void CloseShopButton()
    {

        closeShopBtn = GUI.Button(new Rect (Screen.width * 0.695f, Screen.height * 0.785f, Screen.width * 0.08f, Screen.height * 0.1f), "X");
        if (closeShopBtn)
        {
            showCatTabs = false;
            testBool = false;
            equipBool = false;
            notEnoughGold = false;
            boughtItem = false;
        }
    }

    void ConsumablesButton(){
		
		buyConsumablesBtn = GUI.Button (new Rect (Screen.width * 0.305f, Screen.height * 0.145f, Screen.width * 0.23f, Screen.height * 0.1f), "Consumables");
		if (buyConsumablesBtn)
        {
            consumableSelGridInt = 0;
            equipSelGridInt = 6;
            equipBool = false;
			testBool = true;
		}		
	}

    void EquipmentButton()
    {

        buyEquip = GUI.Button(new Rect (Screen.width * 0.545f, Screen.height * 0.145f, Screen.width * 0.23f, Screen.height * 0.1f), "Equipment");
        if (buyEquip)
        {
            consumableSelGridInt = 6;
            equipSelGridInt = 0;
            testBool = false;
            equipBool = true;
        }
    }

    /*void TestMethOK(){
			for (int i = 0; i < itemNamesA.Length; i++) {
			if (GUI.Button (new Rect (5, Y, 50, 50), itemNamesA[i])) {
				print("w/e");
			}
			Y += 55;
		}
	}*/

    void ConsumableSelGridMeth() {
        consumableSelGridInt = GUI.SelectionGrid(new Rect (Screen.width * 0.305f, Screen.height * 0.275f, Screen.width * 0.47f, Screen.height * 0.2f), consumableSelGridInt, itemNamesA, 3);
        GUI.Label(new Rect (Screen.width * 0.305f, Screen.height * 0.5f, Screen.width * 0.3f, Screen.height * 0.1f), FindItemCostA(consumableSelGridInt));
        GUI.Label(new Rect (Screen.width * 0.305f, Screen.height * 0.6f, Screen.width * 0.47f, Screen.height * 0.2f), FindItemDescriptionA(consumableSelGridInt));
    }

    void EquipSelGridMeth()
    {
        equipSelGridInt = GUI.SelectionGrid(new Rect (Screen.width * 0.305f, Screen.height * 0.275f, Screen.width * 0.47f, Screen.height * 0.2f), equipSelGridInt, itemNamesB, 3);
        GUI.Label(new Rect (Screen.width * 0.305f, Screen.height * 0.5f, Screen.width * 0.3f, Screen.height * 0.1f), FindItemCostB(equipSelGridInt));
        GUI.Label(new Rect (Screen.width * 0.305f, Screen.height * 0.6f, Screen.width * 0.47f, Screen.height * 0.2f), FindItemDescriptionB(equipSelGridInt));
    }

    void BuyButton()
    {

        buyThisBtn = GUI.Button(new Rect (Screen.width * 0.305f, Screen.height * 0.785f, Screen.width * 0.23f, Screen.height * 0.1f), "Click to Buy");
        if (buyThisBtn && consumableSelGridInt == 0)
        {
            if (playerGold < 2)
            {
                SoundManager.instance.efxSource.clip = canNot;
                SoundManager.instance.efxSource.Play();
                boughtItem = false;
                notEnoughGold = true;
            }
            else if (playerGold >= 2)
            {
                notEnoughGold = false;
                boughtItem = true;
                GameManager.hasProteinShake = true;
                playerGold -= 2;
            }
        }

        if (buyThisBtn && equipSelGridInt == 0)
        {
            if (playerGold < 8)
            {
                SoundManager.instance.efxSource.clip = canNot;
                SoundManager.instance.efxSource.Play();
                boughtItem = false;
                notEnoughGold = true;
            }
            else if (playerGold >= 8)
            {
                notEnoughGold = false;
                boughtItem = true;
                IncGameManager.hasWorkoutBench = true;
                playerGold -= 8;
            }
        }

        if (buyThisBtn && equipSelGridInt == 1)
        {
            if (playerGold < 10)
            {
                SoundManager.instance.efxSource.clip = canNot;
                SoundManager.instance.efxSource.Play();
                boughtItem = false;
                notEnoughGold = true;
            }
            else if (playerGold >= 10)
            {
                notEnoughGold = false;
                boughtItem = true;
                IncGameManager.hasSquatRack = true;
                playerGold -= 10;
            }
        }
    }

    //CONSUMABLES
    private string FindItemCostA(int consumableSelGridInt) {
        if (consumableSelGridInt == 0)
        {
            return "Costs 2 Gold";
        }
        if (consumableSelGridInt == 1)
        {
            return "Costs 0 Gold";
        }
        if (consumableSelGridInt == 2)
        {
            return "Costs 0 Gold";
        }
        if (consumableSelGridInt == 3)
        {
            return "Costs 0 Gold";
        }
        if (consumableSelGridInt == 4)
        {
            return "Costs 0 Gold";
        }
        return "Costs 0 Gold";
    }

    private string FindItemDescriptionA(int consumableSelGridInt) {
        if (consumableSelGridInt == 0)
        {
            return "Drink this delicious protein shake during battle to gain 1000 calories. You can only hold one at a time.";
        }
        if (consumableSelGridInt == 1)
        {
            return "Insert item description here.";
        }
        if (consumableSelGridInt == 2)
        {
            return "Insert item description here.";
        }
        if (consumableSelGridInt == 3)
        {
            return "Insert item description here.";
        }
        if (consumableSelGridInt == 4)
        {
            return "Insert item description here.";
        }
        return "Will this ever be seen?";
    }

    //EQUIPMENT/EXERCISES
    private string FindItemCostB(int equipSelGridInt)
    {
        if (equipSelGridInt == 0)
        {
            return "Costs 8 Gold";
        }
        if (equipSelGridInt == 1)
        {
            return "Costs 10 Gold";
        }
        if (equipSelGridInt == 2)
        {
            return "Costs 0 Gold";
        }
        if (equipSelGridInt == 3)
        {
            return "Costs 0 Gold";
        }
        if (equipSelGridInt == 4)
        {
            return "Costs 0 Gold";
        }
        return "Costs 0 Gold";
    }

    private string FindItemDescriptionB(int equipSelGridInt)
    {
        if (equipSelGridInt == 0)
        {
            return "The workout bench lets you perform the Bench Press! Doing actual bench presses will increase your max bench press at a faster rate.";
        }
        if (equipSelGridInt == 1)
        {
            return "The squat rack allows you to perform barbell squats, which will increase your max squat.";
        }
        if (equipSelGridInt == 2)
        {
            return "Insert item description here.";
        }
        if (equipSelGridInt == 3)
        {
            return "Insert item description here.";
        }
        if (equipSelGridInt == 4)
        {
            return "Insert item description here.";
        }
        return "Will this ever be seen?";
    }


    //GOLD - SETTING VARIABLES BTWN SCRIPTS
    void GoldSetMethod()
    {
        if (GameObject.FindWithTag("GameManager"))
        {
            IncGameManager.playerGold = GameManager.instance.playerGold;
        }
        else
        {
            IncGameManager.playerGold = 0;
        }
    }
         
    

	
	// Update is called once per frame
	void Update () {
        IncGameManager.playerGold = playerGold;

        if (GameObject.FindWithTag("GameManager"))
        {
            GameManager.instance.playerGold = playerGold;
        }
        
    }
}
