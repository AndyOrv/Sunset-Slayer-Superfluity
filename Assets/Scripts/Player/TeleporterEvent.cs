using System.Collections;
using UnityEngine;

public class TeleporterEvent : MonoBehaviour
{
    public float chargeTime = 5f; // Time it takes for the teleporter to charge
    public int requiredEnemies = 10; // Number of enemies that need to be defeated to activate the teleporter
    public Transform teleporterLocation; // Location to teleport the player to
    public GameObject teleporterPrefab; // Reference to the teleporter prefab

    private bool isCharged = false; // Flag to indicate if the teleporter is charged
    private int defeatedEnemies = 0; // Number of enemies that have been defeated

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the teleporter area
        if (other.gameObject.CompareTag("Player"))
        {
            // If the teleporter is charged, teleport the player
            if (isCharged)
            {
                other.gameObject.transform.position = teleporterLocation.position;
            }
            // If the teleporter is not charged, start charging it
            else
            {
                StartCoroutine(ChargeTeleporter());
            }
        }
    }

    public void EnemyDefeated()
    {
        // Increment the number of defeated enemies
        defeatedEnemies++;

        // If the required number of enemies have been defeated, charge the teleporter
        if (defeatedEnemies >= requiredEnemies)
        {
            isCharged = true;
            Instantiate(teleporterPrefab, teleporterLocation.position, Quaternion.identity);
        }
    }

    private IEnumerator ChargeTeleporter()
    {
        float elapsedTime = 0f;

        while (elapsedTime < chargeTime)
        {
            // Update the charge percentage of the teleporter
            float chargePercentage = elapsedTime / chargeTime;
            // TODO: Update UI to display charge percentage

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isCharged = true;
        Instantiate(teleporterPrefab, teleporterLocation.position, Quaternion.identity);
    }
}
