using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterEvents
{
    public static UnityAction<GameObject, int> characterDamaged;


    public static UnityAction<GameObject, int> characterHealed;
}