﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameTile;

[CreateAssetMenu]
public class GameTileContentFactory : ScriptableObject
{
	[SerializeField]
	GameTileContent destinationPrefab = default;

	[SerializeField]
	GameTileContent emptyPrefab = default;

	[SerializeField]
	GameTileContent wallPrefab = default;

	Scene contentScene;
	public void Reclaim(GameTileContent content)
	{
		Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
		Destroy(content.gameObject);
	}

	GameTileContent Get(GameTileContent prefab)
	{
		GameTileContent instance = Instantiate(prefab);
		instance.OriginFactory = this;
		MoveToFactoryScene(instance.gameObject);
		return instance;
	}
	public GameTileContent Get(GameTileContentType type)
	{
		switch (type)
		{
			case GameTileContentType.Destination: return Get(destinationPrefab);
			case GameTileContentType.Empty: return Get(emptyPrefab);
			case GameTileContentType.Wall: return Get(wallPrefab);
		}
		Debug.Assert(false, "Unsupported type: " + type);
		return null;
	}

	void MoveToFactoryScene(GameObject o)
	{
		if (!contentScene.isLoaded)
		{
			if (Application.isEditor)
			{
				contentScene = SceneManager.GetSceneByName(name);
				if (!contentScene.isLoaded)
				{
					contentScene = SceneManager.CreateScene(name);
				}
			}
			else
			{
				contentScene = SceneManager.CreateScene(name);
			}
		}
		SceneManager.MoveGameObjectToScene(o, contentScene);
	}




}
