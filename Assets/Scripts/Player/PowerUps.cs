﻿using System;

[Serializable]
public class PowerUps
{
	// ----------------------------------------------- Data members ----------------------------------------------
	// Stores the items that Bip collects throughout the game. Also stores the powerups and keys.
	public bool canEMP = false;				// True if Bip is able to EMP. Will become true again after cooldown.
	public bool canBoost = true;			// True if Bip is able to Speed boost.
    public bool canDoubleJump = true;       // True if Bip is able to double jump.
}