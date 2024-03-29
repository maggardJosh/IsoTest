﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FIsoTile : FSprite
{
    private float _isoX;
    private float _isoY;
    private float _isoHeight;

    public float isoX
    {
        get { return _isoX; }
        set
        {
            _isoX = value;
            this.x = _isoX;
           
        }
    }

    public float isoY
    {
        get { return _isoY; }
        set
        {
            _isoY = value;
            
            this.y = _isoY + _isoHeight;
           
        }
    }

    private float maxDepth = -200;

    public float isoHeight
    {
        get { return _isoHeight; }
        set
        {
            _isoHeight = Mathf.Min(0, Mathf.Max(maxDepth, value));
            this.alpha = 1 - (_isoHeight / maxDepth);
            this.y = _isoY + _isoHeight;
        }
    }

    public FIsoTile(string elementName)
        : base(elementName)
    {

    }
}
