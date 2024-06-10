using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoTransfer : MonoBehaviour
{
	public static PlayerInfoTransfer Instance;

	public Dictionary<Tribe, string> PlayersInfo;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
}
