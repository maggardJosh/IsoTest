using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FIsoSprite : FAnimatedSprite
{
    private float _isoX;
    private float _isoY;
    private float _isoHeight;
    private float _offGroundHeight;

    private FSprite shadow;

    private FTilemap tilemap;

    private void updateSort()
    {
        if (tilemap == null)
            return;
        Vector2 tileVect = tilemap.getCart(getMaxTile());
        this.sortZ = Mathf.FloorToInt(tileVect.x) * tilemap.heightInTiles + Mathf.FloorToInt(tileVect.y) + .5f;
    }

    private void updateActualPosition()
    {
        this.x = _isoX;
        this.y = _isoY + _isoHeight + _offGroundHeight;
        if (shadow != null)
        {
            shadow.x = _isoX;
            shadow.y = _isoY - height / 2 + shadow.height / 2 + isoHeight;
        }
    }

    public virtual float isoX
    {
        get { return _isoX; }
        set
        {
            _isoX = value;
            isoHeight = tilemap.getHeight(tilemap.getTileFromIso(getMaxTile()));
            updateSort();
            updateActualPosition();
        }
    }


    public float isoY
    {
        get { return _isoY; }
        set
        {
            _isoY = value;
            isoHeight = tilemap.getHeight(tilemap.getTileFromIso(getMaxTile()));
           
            updateSort();
            updateActualPosition();
        }
    }

    public float isoHeight
    {
        get { return _isoHeight; }
        set
        {
            if (_isoHeight < value)
            {
                offGroundHeight -= (value - _isoHeight);
            }
            else if (_isoHeight > value)
                offGroundHeight += (_isoHeight - value);
            _isoHeight = Mathf.Max(0, value);
            
            updateActualPosition();
        }
    }

    /**
     * Returns closest tile the player is on (Used for depth sorting)
     */
    public Vector2 getMaxTile()
    {
        Vector2 bottomLeft = new Vector2(_isoX - width / 2, _isoY - height / 2);
        Vector2 bottomRight = new Vector2(_isoX + width / 2, _isoY - height / 2);
        Vector2 cartBottomLeft = tilemap.getCart(bottomLeft);
        Vector2 cartBottomRight = tilemap.getCart(bottomRight);

        if (cartBottomLeft.x * tilemap.heightInTiles + cartBottomLeft.y > cartBottomRight.x * tilemap.heightInTiles + cartBottomRight.y)
            return bottomLeft;
        else
            return bottomRight;
    }
    
    public float offGroundHeight
    {
        get
        {
            return _offGroundHeight;
        }
        set
        {
            _offGroundHeight = Mathf.Max(0, value);
            updateActualPosition();
        }
    }

    public FIsoSprite(String elementName, FTilemap tilemap)
        : base(elementName)
    {
        this.tilemap = tilemap;
        shadow = new FSprite("shadow");
    }

    public override void HandleAddedToContainer(FContainer container)
    {
        container.AddChild(shadow);
        base.HandleAddedToContainer(container);
    }

    public Vector2 getIsoPosition()
    {
        return new Vector2(_isoX, _isoY);
    }

    public override float sortZ
    {
        get
        {
            return base.sortZ;
        }
        set
        {
            base.sortZ = value;
            shadow.sortZ = value;
        }
    }
    public override void HandleRemovedFromContainer()
    {
        shadow.RemoveFromContainer();
        base.HandleRemovedFromContainer();
    }

}

