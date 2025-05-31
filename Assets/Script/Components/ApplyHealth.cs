using System.Collections;
using System.Collections.Generic;
using Script.Components.Interface;
using UnityEngine;

public class ApplyHealth : MonoBehaviour, IAbilityTarget
{
   public int Health = 40;
   
   public List<GameObject> Targets { get; set; }
   public void Execute()
   {
      foreach (var target in Targets)
      {
         var health = target.GetComponent<CharacterHealth>();
         
         if (health != null)
         {
            if (health.Health == 100)
            {
               Debug.Log("already full health");
               return;
            }

            else
            {
               health.Health += Health;
               Destroy(gameObject);
            }
            
         }
         
      }
      
   }
}
