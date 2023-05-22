using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameBehaviorScript : Singleton<GameBehaviorScript>
{
    [Header("Menu Script")]
    [SerializeField] Menu2DScript menuScript;

    [Header("GameObjects")]
    [SerializeField] GameObject house;
    [SerializeField] GameObject treeFarm;
    [SerializeField] GameObject stoneFarm;
    [SerializeField] GameObject cropFarm;
    [SerializeField] GameObject basicCreat;
    [SerializeField] GameObject lumberjackCreat;
    [SerializeField] GameObject minerCreat;
    [SerializeField] GameObject farmerCreat;

    public enum Selected
    {
		DEFAULT,
        HOUSE,
        TREE_FARM,
        STONE_FARM,
        CROP_FARM,
        BASIC_CREAT,
        LUMBERJACK_CREAT,
        MINER_CREAT,
        FARMER_CREAT
    }

    // Resource Amounts
    //[HideInInspector]
    public int MaxCreatureAmount = 1;
    public int creatureAmount = 1;

    public int woodAmount = 10;
    public int stoneAmount = 10;
    public int cropAmount = 10;

    // Building Counts
    public int houseCount = 0;
    public int treeFarmCount = 0;
    public int stoneFarmCount = 0;
    public int cropFarmCount = 0;

    public Selected select = Selected.DEFAULT;

	public void Update()
    {
		MaxCreatureAmount = MaxCreatureAmount * (houseCount * 2) + 2;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
			select = Selected.DEFAULT;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

		switch (select)
        {
			case Selected.DEFAULT:
				break;

            // Buildings
            case Selected.HOUSE:
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					if (woodAmount >= 5 && cropAmount >= 5)
					{
						Vector3 worldPoint = Input.mousePosition;
						worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
						Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
						mouseWorldPosition.z = 0f;

						Instantiate(house, mouseWorldPosition, Quaternion.identity);
						woodAmount -= 5;
						cropAmount -= 5;
						houseCount++;
					}
				}
                break;
                
            case Selected.TREE_FARM:
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					if (woodAmount >= 10 && cropAmount >= 5)
					{
						Vector3 worldPoint = Input.mousePosition;
						worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
						Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
						mouseWorldPosition.z = 0f;

						Instantiate(treeFarm, mouseWorldPosition, Quaternion.identity);
						woodAmount -= 10;
						cropAmount -= 5;
						treeFarmCount++;
					}
				}
				break;

            case Selected.STONE_FARM:
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					if (stoneAmount >= 10 && cropAmount >= 5)
					{
						Vector3 worldPoint = Input.mousePosition;
						worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
						Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
						mouseWorldPosition.z = 0f;

						Instantiate(stoneFarm, mouseWorldPosition, Quaternion.identity);
						stoneAmount -= 10;
						cropAmount -= 5;
						stoneFarmCount++;
					}
				}
				break;

            case Selected.CROP_FARM:
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					if (woodAmount >= 5 && stoneAmount >= 5)
					{
						Vector3 worldPoint = Input.mousePosition;
						worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
						Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
						mouseWorldPosition.z = 0f;

						Instantiate(cropFarm, mouseWorldPosition, Quaternion.identity);
						woodAmount -= 5;
						stoneAmount -= 5;
						cropFarmCount++;
					}
				}
				break;


            // Creatures
            case Selected.BASIC_CREAT:
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					if (creatureAmount < MaxCreatureAmount && cropAmount >= 5)
					{
						Vector3 worldPoint = Input.mousePosition;
						worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
						Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
						mouseWorldPosition.z = 0f;

						Instantiate(basicCreat, mouseWorldPosition, Quaternion.identity);
						cropAmount -= 5;
						creatureAmount++;
					}
				}
				break;

            case Selected.LUMBERJACK_CREAT:
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					if (creatureAmount < MaxCreatureAmount && cropAmount >= 5 && woodAmount >= 5)
					{
						Vector3 worldPoint = Input.mousePosition;
						worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
						Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
						mouseWorldPosition.z = 0f;

						Instantiate(lumberjackCreat, mouseWorldPosition, Quaternion.identity);
						cropAmount -= 5;
						woodAmount -= 5;
						creatureAmount++;
					}
				}
				break;

            case Selected.MINER_CREAT:
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					if (creatureAmount < MaxCreatureAmount && cropAmount >= 5 && stoneAmount >= 5)
					{
						Vector3 worldPoint = Input.mousePosition;
						worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
						Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
						mouseWorldPosition.z = 0f;

						Instantiate(minerCreat, mouseWorldPosition, Quaternion.identity);
						cropAmount -= 5;
						stoneAmount -= 5;
						creatureAmount++;
					}
				}
				break;

            case Selected.FARMER_CREAT:
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					if (creatureAmount < MaxCreatureAmount && cropAmount >= 10)
					{
						Vector3 worldPoint = Input.mousePosition;
						worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
						Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
						mouseWorldPosition.z = 0f;

						Instantiate(farmerCreat, mouseWorldPosition, Quaternion.identity);
						cropAmount -= 10;
						creatureAmount++;
					}
				}
				break;
        }
    }

}
