using System.Collections;
using System.Collections.Generic;
using Script.Components.Interface;
using UnityEngine;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
   public int Damage=10;
   public List<GameObject> Targets { get; set; }
   public void Execute()
   {
      foreach (var target in Targets)
      {
         var health = target.GetComponent<CharacterHealth>();
         if (health != null)
         {
            health.Health -= Damage;
         }
         
      }
      
   }
}
