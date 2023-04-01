using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			Instantiate(prefab, transform.position, transform.rotation);
		}
	}
}
