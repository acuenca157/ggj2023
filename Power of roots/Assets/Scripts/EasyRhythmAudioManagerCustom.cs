using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EasyRhythmAudioManagerCustom : MonoBehaviour
{
    private string lastType;

    // FMOD
    public EventReference eventFull; // A reference to the FMOD event we want to use
    public EventReference eventMedium; // A reference to the FMOD event we want to use
    public EventReference eventLow; // A reference to the FMOD event we want to use
    public EventReference eventTransition; // A reference to the FMOD event we want to use
    public EventReference eventLobby; // A reference to the FMOD event we want to use
    public EventReference eventDie; // A reference to the FMOD event we want to use
    public EasyEvent myAudioEvent; // EasyEvent is the object that will play the FMOD audio event, and provide our callbacks and other related info

    public bool startEventOnAwake = false;

    // You can pass an array of IEasyListeners through to the FMOD event, but we have to serialize them as objects.
    // You have to drag the COMPONENT that implements the IEasyListener into the object, or it won't work properly
    [RequireInterface(typeof(IEasyListener))]
    public Object[] myEventListeners;

    void Start()
    {
        // Passes the EventReference so EasyEvent can create the FMOD Event instance
        // Passes an array of listeners through (IEasyListener) so the audio event knows which objects want to listen to the callbacks
        myAudioEvent = new EasyEvent(eventLobby.Path, myEventListeners);
        myAudioEvent.stop();

        if (startEventOnAwake)
            myAudioEvent.start();
    }

    public void changeMusic(float health, bool isLobby, bool died = false) {
        if (died)
        {
            lastType = "died";
            myAudioEvent.stop();
            myAudioEvent = new EasyEvent(eventDie.Path, myEventListeners);
            myAudioEvent.start();
        }
        else if (isLobby)
        {
            lastType = "lobby";
            myAudioEvent.stop();
            myAudioEvent = new EasyEvent(eventLobby.Path, myEventListeners);
            myAudioEvent.start();
        }
        else if (health > 54f && lastType != "full")
        {
            lastType = "full";
            myAudioEvent.stop();
            myAudioEvent = new EasyEvent(eventFull.Path, myEventListeners);
            myAudioEvent.start();
        }
        else if (health > 27f && lastType != "mid")
        {
            lastType = "mid";
            myAudioEvent.stop();
            myAudioEvent = new EasyEvent(eventMedium.Path, myEventListeners);
            myAudioEvent.start();
        }
        else if ( lastType != "low") {
            lastType = "low";
            myAudioEvent.stop();
            myAudioEvent = new EasyEvent(eventLow.Path, myEventListeners);
            myAudioEvent.start();
        }
    }
}
