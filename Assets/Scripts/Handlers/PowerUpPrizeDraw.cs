using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPrizeDraw : MonoBehaviour
{

    [SerializeField] public RawImage PrizeDraw;
    [SerializeField] public AudioClip SortingSound;
    [SerializeField] public AudioSource AudioSFX;
    [SerializeField] public Animator PrizeDrawAnimator;
    [SerializeField] public List<PowerUpPercentaged> PowerUpPercentageds = new();

    private List<PowerUpPercentaged> _validList;
    private int _currentPlayerPosition = 4;
    private GameObject _cart;
    private PowerUp _pu;
    private bool _isSorting = false;
    private bool _canGetPowerUp = true;

    private void OnEnable()
    {
        RandomBox.HittingBox += StartPrizeDraw;
        PlayerInputControllerActions.ConsumePowerUp += OnConsumePowerUp;
    }

    private void OnDisable()
    {
        RandomBox.HittingBox -= StartPrizeDraw;
        PlayerInputControllerActions.ConsumePowerUp -= OnConsumePowerUp;
    }

    private void StartPrizeDraw(GameObject obj, int position)
    {

        if (_canGetPowerUp)
        {
            // Debug.Log(obj);
            _cart = obj;
            _currentPlayerPosition = position;

            _validList = PowerUpPercentageds.FindAll(pw => pw.GetPercentageByPosition(position) > 0);
            int acc = 0;
            foreach (PowerUpPercentaged item in _validList) acc += item.GetPercentageByPosition(position);
            if (acc < 100) throw new Exception("The SUM of Percentages should be equal 100");

            if (PlayerIdentifier.IsPlayer(obj)) PrizeDrawAnimator.SetBool("IsSorting", true);

            // Start sound
            AudioSFX.clip = SortingSound;
            AudioSFX.Play();

            _canGetPowerUp = false;
            _isSorting = true;
            OnPrizeDraw();
        }
    }

    private void OnPrizeDraw()
    {
        int acc = 0;

        System.Random rnd = new System.Random();
        int random = rnd.Next(1, 100);

        // Debug.Log(random);

        foreach (PowerUpPercentaged item in _validList)
        {
            acc += item.GetPercentageByPosition(_currentPlayerPosition);
            if (random <= acc)
            {
                _pu = item.PowerUp;
                StartCoroutine(SelectPowerUp(item.PowerUp));
                return;
            }
        }
    }

    IEnumerator SelectPowerUp(PowerUp powerUp)
    {
        yield return new WaitForSeconds(3); // Time to keep sorting

        PrizeDrawAnimator.SetInteger("PowerUpId", powerUp.GetId());
        PrizeDrawAnimator.SetBool("IsSorting", false);
        _isSorting = false;

        // End Sound
        AudioSFX.Stop();

        yield return new WaitForSeconds(1);

        if (powerUp.IsHotTriggered)
        {
            OnConsumePowerUp();
        }
    }

    private void OnConsumePowerUp()
    {
        if (_isSorting == false && _canGetPowerUp == false)
        {
            _pu.Run(_cart);

            PrizeDrawAnimator.SetTrigger("UsePoweUp");
            if (_pu.FeedbackAudio != null)
            {
                AudioSFX.clip = _pu.FeedbackAudio;
                AudioSFX.Play();
            }

            if (_pu.FeedbackParticles != null)
            {
                _pu.FeedbackParticles.gameObject.SetActive(true);
                _pu.FeedbackParticles.Play();
            }

            StartCoroutine(StopPoweUpEffects(_pu.GetLifeTime()));
            _canGetPowerUp = true;
        }
    }

    IEnumerator StopPoweUpEffects(float secDelay)
    {
        yield return new WaitForSeconds(secDelay); // Time to keep sorting

        if (_pu.FeedbackAudio != null)
        {
            AudioSFX.Stop();
        }

        if (_pu.FeedbackParticles != null)
        {
            _pu.FeedbackParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
