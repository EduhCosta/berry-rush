using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PowerUpPercentaged : MonoBehaviour
{
    [SerializeField] public PowerUp PowerUp;
    [SerializeField] public int PercentagePosition1 = 0;
    [SerializeField] public int PercentagePosition2 = 0;
    [SerializeField] public int PercentagePosition3 = 0;
    [SerializeField] public int PercentagePosition4 = 0;
    [SerializeField] public int PercentagePosition5 = 0;
    [SerializeField] public int PercentagePosition6 = 0;
    [SerializeField] public int PercentagePosition7 = 0;
    [SerializeField] public int PercentagePosition8 = 0;

    private List<int> _percentages = new(8);

    private void Awake()
    {
        _percentages.Add(PercentagePosition1);
        _percentages.Add(PercentagePosition2);
        _percentages.Add(PercentagePosition3);
        _percentages.Add(PercentagePosition4);
        _percentages.Add(PercentagePosition5);
        _percentages.Add(PercentagePosition6);
        _percentages.Add(PercentagePosition7);
        _percentages.Add(PercentagePosition8);

        foreach(int percentage in _percentages)
        {
            if (percentage > 100 || percentage < 0)
            {
                throw new Exception("Please, keep the Percentage property between 0 and 100");
            }
        }
    }

    public int GetPercentageByPosition(int position)
    {
        if (position < 1 || position > _percentages.Count) throw new Exception("Invalid cart position");

        return _percentages[position - 1];
    }
}
