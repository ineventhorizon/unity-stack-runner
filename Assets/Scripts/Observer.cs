using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer
{
    public static UnityAction StartMovement;
    public static UnityAction<Collectable> collected;
    public static UnityAction<Collectable> dropped;
    public static UnityAction<Collectable> upgraded;
    public static UnityAction<int> score;
}
