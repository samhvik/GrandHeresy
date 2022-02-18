using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearManager : MonoBehaviour {
   
    double smoothening = 0.5;
    
    
    // will def need to interface with the player object for the current crit value that the player has and the crit
    // chance the player is trying to be at
    void critChance(double random) {
        var crit = 0.5;
        // this will be a value gotten from the player
        var playerCrit = 0.5;
        
        // I believe that this value can be a function in itself but I need to test that

        // crit happens
        if (random <= playerCrit) {
            playerCrit -= smoothening * (1 - crit);
        }
        // crit doesn't happen
        else {
            playerCrit = smoothening * crit;
        }
    }

    void lootTable(double random) {
        // placehoder for actual object
        var projectedTable = new double[]{0.25,0.25,0.25,0.25};
        
        // placeholder for the player 
        var playerTable = new double[]{0.25,0.25,0.25,0.25};

        var index = 0;

        var floor = playerTable[index];

        while (random > floor) {
            index++;
            floor += playerTable[index];
        }

        var change = smoothening * (1 - projectedTable[index]);

        var reduction = 1 - projectedTable[index];

        for (var changeIndex = 0; changeIndex < projectedTable.Length; changeIndex++) {
            if (changeIndex == index) {
                playerTable[changeIndex] -= change;
            }
            else {
                var cut = (projectedTable[changeIndex] / reduction);
                playerTable[changeIndex] += cut * change;
            }
        }


    }
}
