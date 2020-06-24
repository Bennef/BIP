﻿using UnityEngine;
using System.Collections;
using Scripts.Player;

namespace Scripts.Player
{
    public class PlayerPower : PlayerHealth
    {
        public override void TakeDamage(float _value)
        {
            isRegenerating = false;
            value -= _value;
            value = Mathf.Clamp(value, 0, maxValue);
            StartCoroutine("RegenDelayCo");
        }

        IEnumerator RegenDelayCo()
        {
            yield return new WaitForSeconds(delay);
            isRegenerating = true;
        }
    }
}