using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPrizeDraw : MonoBehaviour
{
    [SerializeField] public RawImage PrizeDraw;
    [SerializeField] public Animator PrizeDrawAnimator;
    [SerializeField] public List<PowerUpPercentaged> PowerUpPercentageds = new ();

    private List<PowerUpPercentaged> _validList;
    private int _currentPlayerPosition = 4;

    private void OnEnable()
    {
        RandomBox.HittingBox += StartPrizeDraw;
        PlayerInputControllerActions.ConsumePowerUp += ConsumePowerUp;
    }

    private void OnDisable()
    {
        RandomBox.HittingBox -= StartPrizeDraw;
    }

    private void StartPrizeDraw(GameObject obj, int position)
    {
        _currentPlayerPosition = position;

        _validList = PowerUpPercentageds.FindAll(pw => pw.GetPercentageByPosition(position) > 0);
        int acc = 0;
        foreach (PowerUpPercentaged item in _validList) acc += item.GetPercentageByPosition(position);
        if (acc < 100) throw new Exception("The SUM of Percentages should be equal 100");

        if (PlayerIdentifier.IsPlayer(obj)) PrizeDrawAnimator.SetBool("IsSorting", true);
        
        OnPrizeDraw();
    }

    private void OnPrizeDraw()
    {
        int acc = 0;

        System.Random rnd = new System.Random();
        int random = rnd.Next(1, 100);

        Debug.Log(random);

        foreach (PowerUpPercentaged item in _validList)
        {
            acc += item.GetPercentageByPosition(_currentPlayerPosition);
            if (random <= acc)
            {
                StartCoroutine(SelectPowerUp(item.PowerUp));
                return;
            }
        }
    }

    IEnumerator SelectPowerUp(PowerUp powerUp)
    {
        yield return new WaitForSeconds(3);

        PrizeDrawAnimator.SetInteger("PowerUpId", powerUp.GetId());
        PrizeDrawAnimator.SetBool("IsSorting", false);
    }

    private void ConsumePowerUp()
    {
        PrizeDrawAnimator.SetTrigger("UsePoweUp");
    }

}
