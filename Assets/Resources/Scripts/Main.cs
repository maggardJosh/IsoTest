using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    FSprite player;

	// Use this for initialization
	void Start () {
		FutileParams futileParams = new FutileParams(true, false, false, false);

		futileParams.AddResolutionLevel(960, 1.0f, 1.0f, "");
		futileParams.origin = new Vector2(0.5f, 0.5f);
		futileParams.backgroundColor = new Color(.2f, .2f, .2f);
		Futile.instance.Init(futileParams);

		Futile.atlasManager.LoadAtlas("Atlases/atlasOne");

        FTmxMap tmxMap = new FTmxMap();
        tmxMap.LoadTMX("Maps/mapOne");

        FTilemap tilemap = (FTilemap)tmxMap.getLayerNamed("Tilemap");

        FContainer tilemapLayer = new FContainer();
        tilemapLayer.AddChild(tmxMap);

        Futile.stage.AddChild(tilemapLayer);
        player = new FSprite("tile_2");
        FCamObject camera = new FCamObject();
        camera.follow(player);

        tilemap.clipNode = camera;

        Futile.stage.AddChild(player);
        Futile.stage.AddChild(camera);
	}
    const float speed = 100.0f;
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.S))
            player.y -= speed * UnityEngine.Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            player.y += speed * UnityEngine.Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            player.x -= speed * UnityEngine.Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            player.x += speed * UnityEngine.Time.deltaTime;
        
    }
}
