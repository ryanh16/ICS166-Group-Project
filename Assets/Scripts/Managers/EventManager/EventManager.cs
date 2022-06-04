// Written by Kevin Chao
// Modified by Boyuan Huang

using System.Collections.Generic;
using UnityEngine;
using System;


// Singleton
public class EventManager : MonoBehaviour
{
    private static int CurrentEventIndex;

    private static List<EventTypes.Events> EventChain = new List<EventTypes.Events>
    { 
        EventTypes.Events.GameStart,
        EventTypes.Events.BusPassInteractedWith,
        EventTypes.Events.AfterPurchaseBusPass,
        EventTypes.Events.AfterExamineBusPass,
        EventTypes.Events.AfterCallTC,
        EventTypes.Events.AfterConsult, 
        EventTypes.Events.AfterClass,
        EventTypes.Events.AfterSupervisorIntroduced,
        EventTypes.Events.AfterReturnBusPass,
        EventTypes.Events.Ending
    };

    private static Dictionary<EventTypes.Events, Action> subscriberDict;
    public static EventManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            subscriberDict = new Dictionary<EventTypes.Events, Action>();
        }
    }

    private void Start()
    {
        subscriberDict[EventChain[CurrentEventIndex]]?.Invoke();
    }

    public void Subscribe(EventTypes.Events eventType, Action listener)
    {
        Action existingListeners;

        // If eventType has listeners already in subscriberDict
        if (subscriberDict.TryGetValue(eventType, out existingListeners))
        {
            // Add new listener to existingListeners
            existingListeners += listener;

            // Update subscriberDict
            subscriberDict[eventType] = existingListeners;
        }

        // If eventType has no listeners in subscriberDict
        else
        {
            // Add event to subscriberDict
            existingListeners += listener;
            subscriberDict.Add(eventType, existingListeners);
        }
    }

    public void Unsubscribe(EventTypes.Events eventType, Action listener)
    {
        // If EventManager is already destroyed, no reason to unsubscribe
        if (Instance == null) return;

        Action existingListeners;

        // If eventType has listeners already in subscriberDict
        if (subscriberDict.TryGetValue(eventType, out existingListeners))
        {
            // Remove listener from existingListeners
            existingListeners -= listener;

            // Update subscriberDict
            subscriberDict[eventType] = existingListeners;
        }
    }

    public void AdvanceToNextEvent()
    {
        CurrentEventIndex += 1;
        subscriberDict[EventChain[CurrentEventIndex]]?.Invoke();
        Debug.Log($"Advanced one event, the current event is {EventChain[CurrentEventIndex]}");
    }

    public bool HasReachedTheEnd()
    {
        return CurrentEventIndex == (EventChain.Count - 1);
    }
}