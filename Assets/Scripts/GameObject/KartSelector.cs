using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class KartSelector : MonoBehaviour
{
    [SerializeField] List<KartModel> Karts = new();
    [SerializeField] int _idSelected = 1;

    private GameObject _parent;
    private GameObject _currentElement;

    private void Start()
    {
        if (_idSelected != 0)
        {
            CreateKart();
        }
    }

    public int GetId()
    {
        return _idSelected;
    }

    public void SetId(int id)
    {
        _idSelected = id;
        CreateKart();
    }

    public void CreateKart()
    {
        CreateKart(_idSelected);
    }

    public void CreateKart(int id)
    {
        if (_currentElement != null) Destroy(_currentElement);

        _parent = gameObject.transform.parent.gameObject;
        var GO = Instantiate(Karts.ElementAt(id - 1));
        GO.transform.position = _parent.transform.position;
        GO.transform.SetParent(_parent.transform);

        GO.transform.localPosition = Vector3.zero;
        GO.transform.localRotation = Quaternion.identity;
        GO.transform.localScale = Vector3.one;

        _currentElement = GO.gameObject;
    }
}
