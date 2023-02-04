using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour, IEasyListener
{
    public void OnBeat(EasyEvent audioEvent) 
	{ 
        print("CACA");
		print("Beat: " + audioEvent.CurrentBeat.ToString());
	}
}
