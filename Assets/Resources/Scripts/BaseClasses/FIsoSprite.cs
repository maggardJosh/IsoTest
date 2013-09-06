﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FIsoSprite : FSprite
{
    private float _isoX;
    private float _isoY;
    private float _isoHeight;

    private FSprite shadow;

    private FTilemap tilemap;

    private void updateSort()
    {
        if (tilemap == null)
            return;
        Vector2 tileVect = tilemap.getCart(getIsoPosition() + Vector2.up * -height / 2);
        this.sortZ = Mathf.FloorToInt(tileVect.x) * tilemap.heightInTiles + Mathf.FloorToInt(tileVect.y) + .5f;
    }

    public float isoX
    {
        get { return _isoX; }
        set
        {
            _isoX = value;
            this.x = _isoX;
            isoHeight = tilemap.getHeight(tilemap.getTileFromIso(new Vector2(_isoX, _isoY - height / 2)));
            if (shadow != null)
            {
                shadow.x = _isoX;
                if (tilemap != null)
                    shadow.y = _isoY - height / 2 + shadow.height / 2 + tilemap.getHeight(tilemap.getTileFromIso(new Vector2(_isoX, _isoY - height / 2)));
                else
                    shadow.y = _isoY - height / 2 + shadow.height / 2;
                updateSort();
            }
        }
    }

    public float isoY
    {
        get { return _isoY; }
        set
        {
            _isoY = value;
            isoHeight = tilemap.getHeight(tilemap.getTileFromIso(new Vector2(_isoX, _isoY - height / 2)));
            this.y = _isoY + _isoHeight;
            if (shadow != null)
            {
                if (tilemap != null)
                    shadow.y = _isoY - height / 2 + shadow.height / 2 + tilemap.getHeight(tilemap.getTileFromIso(new Vector2(_isoX, _isoY - height / 2)));
                else
                    shadow.y = _isoY - height / 2 + shadow.height / 2;
            }
            updateSort();
        }
    }

    public float isoHeight
    {
        get { return _isoHeight; }
        set
        {
            _isoHeight = Mathf.Max(0, value);
            this.y = _isoY + _isoHeight;
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

