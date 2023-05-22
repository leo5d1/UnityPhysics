using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menu2DScript : Singleton<Menu2DScript> 
{
	[Header("Game Behavior Script")]
	[SerializeField] GameBehaviorScript gameManager;

	[Header("Menu Buttons")]
	[SerializeField] Button buildBTN;
    [SerializeField] Button resourceBTN;
    [SerializeField] Button creatureBTN;

	[Header("Menus")]
	[SerializeField] GameObject buildBG;
    [SerializeField] GameObject resourceBG;
    [SerializeField] GameObject creatureBG;

	[Header("Build Menu Buttons")]
	[SerializeField] Button house;
	[SerializeField] Button woodFarm;
	[SerializeField] Button stoneFarm;
	[SerializeField] Button cropFarm;

	[Header("Creature Menu Buttons")]
	[SerializeField] Button basicCreat;
	[SerializeField] Button lumberjackCreat;
	[SerializeField] Button minerCreat;
	[SerializeField] Button farmerCreat;

	[Header("Resource Amount Tags")]
	[SerializeField] TMP_Text creatures;
	[SerializeField] TMP_Text wood;
	[SerializeField] TMP_Text stone;
	[SerializeField] TMP_Text crop;

	[Header("Cursor Textures")]
	[SerializeField] Texture2D houseTexture;
	[SerializeField] Texture2D woodFarmTexture;
	[SerializeField] Texture2D stoneFarmTexture;
	[SerializeField] Texture2D cropFarmTexture;
	[SerializeField] Texture2D basicCreatTexture;
	[SerializeField] Texture2D lumberjackCreatTexture;
	[SerializeField] Texture2D minerCreatTexture;
	[SerializeField] Texture2D farmerCreatTexture;

	// On Click events to change the menu we are on
	public void OnBuildBTN()
    {
        if(buildBG.activeInHierarchy == false)
        {
			buildBG.SetActive(true);
			resourceBG.SetActive(false);
			creatureBG.SetActive(false);
        }
    }

	public void OnResourceBTN()
	{
		if (resourceBG.activeInHierarchy == false)
		{
			buildBG.SetActive(false);
			resourceBG.SetActive(true);
			creatureBG.SetActive(false);
		}
	}

	public void OnCreatureBTN()
	{
		if (creatureBG.activeInHierarchy == false)
		{
			buildBG.SetActive(false);
			resourceBG.SetActive(false);
			creatureBG.SetActive(true);
		}
	}


	// On Click events for the build menu buttons
	public void OnHouseClick()
	{
		Cursor.SetCursor(houseTexture, Vector2.zero, CursorMode.Auto);
		gameManager.select = GameBehaviorScript.Selected.HOUSE;
	}

	public void OnWoodFarmClick()
	{
		Cursor.SetCursor(woodFarmTexture, Vector2.zero, CursorMode.Auto);
		gameManager.select = GameBehaviorScript.Selected.TREE_FARM;
	}

	public void OnStoneFarmClick()
	{
		Cursor.SetCursor(stoneFarmTexture, Vector2.zero, CursorMode.Auto);
		gameManager.select = GameBehaviorScript.Selected.STONE_FARM;
	}

	public void OnCropFarmClick()
	{
		Cursor.SetCursor(cropFarmTexture, Vector2.zero, CursorMode.Auto);
		gameManager.select = GameBehaviorScript.Selected.CROP_FARM;
	}

	// On Click events for the Creature menu buttons

	public void OnCreatureClick()
	{
		Cursor.SetCursor(basicCreatTexture, Vector2.zero, CursorMode.Auto);
		gameManager.select = GameBehaviorScript.Selected.BASIC_CREAT;
	}

	public void OnLumberjackClick()
	{
		Cursor.SetCursor(lumberjackCreatTexture, Vector2.zero, CursorMode.Auto);
		gameManager.select = GameBehaviorScript.Selected.LUMBERJACK_CREAT;
	}

	public void OnMinerClick()
	{
		Cursor.SetCursor(minerCreatTexture, Vector2.zero, CursorMode.Auto);
		gameManager.select = GameBehaviorScript.Selected.MINER_CREAT;
	}

	public void OnFarmerClick()
	{
		Cursor.SetCursor(farmerCreatTexture, Vector2.zero, CursorMode.Auto);
		gameManager.select = GameBehaviorScript.Selected.FARMER_CREAT;
	}

	// Updating the Resource Menu values
	public void Update()
	{
		creatures.text = gameManager.creatureAmount.ToString();
		wood.text = gameManager.woodAmount.ToString();
		stone.text = gameManager.stoneAmount.ToString();
		crop.text = gameManager.cropAmount.ToString();
	}
}
