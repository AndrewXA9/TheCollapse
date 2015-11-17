using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this is where the heatmap data is stored and calculated
public class TileManager: MonoBehaviour {
	
	//container for all tile heat data
	public List<HeatMapEntity> tiles;
	
	//size of tiles
	public static float gridSize = 2.0f;
	
	//elevation for tiles to fall
	public static float lavaHeight = -10.0f;
	//base strength of tile heat
	public float heatStrength = 0.01f;
	//distance from tile to player to activate heat
	public float heatDistance = 5f;
	//curve to adjust strength of heat from player over distance
	public AnimationCurve heatFalloff;
	//heat limit, tiles break when exceeding this
	public float heatLimit = 5f;
	//player object
	private GameObject player;
	
	void Start () {
		//find player
		player = GameObject.FindGameObjectWithTag("Player");
		
		//find all tiles, make heatmap data object and store in a list
		tiles = new List<HeatMapEntity> ();
		GameObject[] aTiles = GameObject.FindGameObjectsWithTag ("Tile");
		foreach(GameObject i in aTiles){
			tiles.Add(new HeatMapEntity(i,0));
		}
		
		/*
		//debug output number of tiles
		for(int i=0;i<tiles.Count;i++){
			Debug.Log("Tile "+i.ToString());
		}*/
	}
	
	//use fixedupdate for better performance when running heatmap logic
	void FixedUpdate(){
		
		//store any tile indicies that break
		List<int> deadTiles = new List<int>();
		
		//iterate through enabled tile list
		for(int i = 0;i<tiles.Count;i++){
		
			//get player distance to tile
			float playerDist = (tiles[i].tile.transform.position-player.transform.position).magnitude;
			if((playerDist) < heatDistance){
				//apply heat equasion to tile
				tiles[i].heat += heatStrength*heatFalloff.Evaluate(playerDist/heatDistance);
			}
			if(tiles[i].heat >= heatLimit){
				//break tiles that exceeded heat limit, initialize their fall logic
				tiles[i].tile.gameObject.SendMessage("breakTile");
				deadTiles.Add(i);
			}
		}
		//remove all broken tiles from list so we don't waste time applying heat
		foreach(int i in deadTiles){
			tiles.RemoveAt(i);
		}
		
	}
	
	void OnDrawGizmos(){
		//visual representation of heat map
		foreach(HeatMapEntity i in tiles){
			Gizmos.color = new Color(1f,0f,0f,0.5f);
			float size = (gridSize*(i.heat/heatLimit));
			Gizmos.DrawCube(i.tile.transform.position+(Vector3.up*1.5f),new Vector3(size,0.2f,size));
		}
	}
	
	
}

//small class for storing tile and tile's heat data
[System.Serializable]
public class HeatMapEntity{
	public GameObject tile;
	public float heat;
	public HeatMapEntity(GameObject _tile,float _heat){
		tile = _tile;
		heat = _heat;
	}
}
