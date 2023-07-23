using UnityEngine;

namespace AlexzanderCowell
{
    public class SeedStorage : MonoBehaviour
    {
        // [HideInInspector] public static int totalSeedStorage, totalVegStorage; // Unused atm but will be implementing in future.
        [HideInInspector] public static int carrotSeed, potatoSeed,carrots,potatos,wood; // Total of each Seed & Vegetables.
        [HideInInspector] public static float carrotSeedOutput, potatoSeedOutPut; // Carrot Seed & Potato Seed Pools.


        private void Start()
        {
            carrotSeed = 2; // Total Carrot Seeds Start with.
            potatoSeed = 2; // Total Potato Seeds Start with.
            carrots = 1; // Total Carrots Start with.
            potatos = 1; // Total Potato's Start with.
            wood = 0;
        }

        private void Update()
        {
            carrotSeed = Mathf.Clamp(carrotSeed, 0, 300); // Sets Carrot Seeds Can't be less than 0 and can't have more then 300.
            potatoSeed = Mathf.Clamp(potatoSeed, 0, 300); // Sets Potato Seeds Can't be less than 0 and can't have more then 300.
            carrots = Mathf.Clamp(carrots, 0, 300); // Sets Carrots Can't be less than 0 and can't have more then 300.
            potatos = Mathf.Clamp(potatos, 0, 300); // Sets Potato's Can't be less than 0 and can't have more then 300.
            wood = Mathf.Clamp(wood, 0, 300); // Sets Wood Can't be less than 0 and can't have more then 300.

            if (carrotSeedOutput >= 1) // If Carrot Seed Pool gets to 1 or more then you get 2 carrot seeds and lose 2 float from carrot float pool.
            {
                carrotSeed = 2;
                carrotSeedOutput -= 2;
            }

            if (potatoSeedOutPut >= 1) // If Potato Seed Pool gets to 1 or more then you get 1 carrot seeds and lose 1 float from potato float pool.
            {
                potatoSeed = 1;
                potatoSeedOutPut -= 1;
            }
            
            // totalSeedStorage = carrotSeed + potatoSeed; // Unused atm but will be implementing in future.
            // totalVegStorage = carrots + potatos; // Unused atm but will be implementing in future.
        }
    }
}
