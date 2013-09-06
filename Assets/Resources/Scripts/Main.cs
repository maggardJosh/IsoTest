using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    Player player;
    FTilemap tilemap;

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

         tilemap = (FTilemap)tmxMap.getLayerNamed("Tilemap");

        FContainer tilemapLayer = new FContainer();
        tilemapLayer.AddChild(tmxMap);

        Futile.stage.AddChild(tilemapLayer);
        player = new Player(tilemap);
        player.isoX = 20;
        player.isoY = -20;
        player.isoHeight = 0;
        FCamObject camera = new FCamObject();
        camera.follow(player);

        tilemap.clipNode = player;
        tilemap.AddChild(player);
        Futile.stage.AddChild(camera);
	}
    const float speed = 200.0f;
	// Update is called once per frame
	void Update () {
       Vector2 pos =  tilemap.getCart(player.GetPosition() + Vector2.up * -player.height / 2);

       if (Input.GetKey(KeyCode.S))
           player.isoY -= speed * UnityEngine.Time.deltaTime;
       if (Input.GetKey(KeyCode.W))
           player.isoY += speed * UnityEngine.Time.deltaTime;
       if (Input.GetKey(KeyCode.A))
           player.isoX -= speed * UnityEngine.Time.deltaTime;
       if (Input.GetKey(KeyCode.D))
           player.isoX += speed * UnityEngine.Time.deltaTime;
       if (Input.GetKey(KeyCode.Space))
           player.jump();
        
    }
}
