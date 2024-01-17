using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    
    public void Start() 
    {
      List<string> inventory = new List<string>();

      inventory.Add("Kenzo");
      inventory.Add("Pixel");
      inventory.Add("Rave");
      inventory.Add("Barca");

      inventory.Sort();

      foreach (var item in inventory)
      {
        print(item);
      }
    }

   
}
