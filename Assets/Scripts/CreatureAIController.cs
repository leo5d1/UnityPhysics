using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CreatureAIController : MonoBehaviour
{
    [SerializeField] Type type;

    private float basicTimer = 3;
    private float lumberjackTimer = 3;
    private float minerTimer = 3;
    private float farmerTimer = 3;

    private Vector2 target;
	Vector2 direction = Vector2.zero;

	enum Type
    {
        BASIC,
        LUMBERJACK,
        MINER,
        FARMER
    }

	private void Start()
	{
		NewTarget();
	}

	void Update()
    {
		float dx = Mathf.Abs(target.x - transform.position.x);
		if (dx <= 1f)
		{
            NewTarget();
		}
		else
		{
			direction.x = Mathf.Sign(target.x - transform.position.x);
		}


		switch (type)
        {
            case Type.BASIC:
                if (basicTimer > 0)
                {
                    basicTimer -= Time.deltaTime;
                }
                else
                {
                    int choice = Random.Range(1, 4);

					switch (choice)
                    {
                        case 1:
                            GameBehaviorScript.Instance.woodAmount += 1;
                            basicTimer = 3;
                            break;

                        case 2:
							GameBehaviorScript.Instance.stoneAmount += 1;
							basicTimer = 3;
							break;

                        case 3:
							GameBehaviorScript.Instance.cropAmount += 1;
							basicTimer = 3;
							break;
                    }
                }
                
                break;

            case Type.LUMBERJACK:
                if (lumberjackTimer > 0)
                {
                    lumberjackTimer -= Time.deltaTime;
                }
                else
                {
                    GameBehaviorScript.Instance.woodAmount += 1 * GameBehaviorScript.Instance.treeFarmCount;
                    lumberjackTimer = 3;
                }

				break;

            case Type.MINER:
				if (minerTimer > 0)
				{
					minerTimer -= Time.deltaTime;
				}
				else
				{
                    GameBehaviorScript.Instance.stoneAmount += 1 * GameBehaviorScript.Instance.stoneFarmCount;
                    minerTimer = 3;
				}

				break;

            case Type.FARMER:
				if (farmerTimer > 0)
				{
					farmerTimer -= Time.deltaTime;
				}
				else
				{
                    GameBehaviorScript.Instance.cropAmount += 1 * GameBehaviorScript.Instance.cropFarmCount;
                    farmerTimer = 3;
				}

				break;
        }
    }

    public void NewTarget()
    {
        target.x = (transform.position.x + (Random.value - 0.5f) * 2);
        target.y = (transform.position.y + (Random.value - 0.5f) * 2);
	}
}
