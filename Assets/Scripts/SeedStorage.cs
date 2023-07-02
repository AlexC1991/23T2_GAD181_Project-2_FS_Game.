using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class SeedStorage : MonoBehaviour
    {
        // [HideInInspector] public static int totalSeedStorage, totalVegStorage;
        [HideInInspector] public static int carrotSeed, potatoSeed,carrots,potatos;
        [HideInInspector] public static float carrotSeedOutput, potatoSeedOutPut;


        private void Start()
        {
            carrotSeed = 2;
            potatoSeed = 2;
            carrots = 1;
            potatos = 1;
        }

        private void Update()
        {
            carrotSeed = Mathf.Clamp(carrotSeed, 0, 20);
            potatoSeed = Mathf.Clamp(potatoSeed, 0, 20);
            carrots = Mathf.Clamp(carrots, 0, 30);
            potatos = Mathf.Clamp(potatos, 0, 30);

            if (carrotSeedOutput >= 1)
            {
                carrotSeed = 2;
                carrotSeedOutput -= 2;
            }

            if (potatoSeedOutPut >= 1)
            {
                potatoSeed = 1;
                potatoSeedOutPut -= 1;
            }
            
            // totalSeedStorage = carrotSeed + potatoSeed;
            // totalVegStorage = carrots + potatos;
        }
    }
}
