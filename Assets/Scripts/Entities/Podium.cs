using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Podium
{
    public CartGameSettings cart { get; set; } 
    public int checkpointOrder { get; set; }
    public float timeStamp { get; set; }
    public LapData currentLap { get; set; }

    public Podium(CartGameSettings _cart, int _checkpointOrder, float _timeStamp, LapData _currentLap)
    {
        this.cart = _cart;
        this.checkpointOrder = _checkpointOrder;
        this.timeStamp = _timeStamp;
        this.currentLap = _currentLap;
    }
}
