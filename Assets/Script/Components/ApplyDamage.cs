using System.Collections;
using System.Collections.Generic;
using Script.Components.Interface;
using UnityEngine;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
   private PlayerStats _health;
   public int Damage=20;
   public List<GameObject> Targets { get; set; }
   public void Execute()
   {
      foreach (var target in Targets)
      {
         var _health = target.GetComponent<CharacterHealth>();
         if (_health != null)
         {
            _health.Health -= Damage;
            // _health.Health;
            Destroy(gameObject);
            
            
         }
         
      }
      
   }
}
