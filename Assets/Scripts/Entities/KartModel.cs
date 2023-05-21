using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class KartModel: MonoBehaviour
{
    [SerializeField] private int _id;

    private GameObject _model;

    private void Awake()
    {
        _model = GetComponent<GameObject>();
    }

    public int GetId()
    {
        return _id;
    }

    public GameObject GetModel()
    {
        return _model;
    }
}

