using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCollisionHorizontal : MonoBehaviour
{
    public Tilemap tilemap;
    private Vector3 origin ;
    private Vector3Int size;

    void Start()
    {
        size = tilemap.size;
        BoxCollider bc = gameObject.AddComponent<BoxCollider>();
        bc.center = tilemap.localBounds.center;
        bc.size = new Vector3(size.x*3,0.1f,size.y*3);
    }
}
