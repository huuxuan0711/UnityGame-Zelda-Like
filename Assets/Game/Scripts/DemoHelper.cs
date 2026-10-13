using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Demo helper: press keys during Play mode to cheat for presentations.
/// Attach to any GameObject in the scene (e.g. an empty "DemoHelper" object).
/// T/F1 = teleport player in front of the BossDoor (walk through normally)
/// G/F2 = toggle god mode (ignore damage)
/// H/F3 = full heal
/// Delete this script / remove from scene before final submission if desired.
/// </summary>
public class DemoHelper : MonoBehaviour
{
    [Header("Boss door object name in scene")]
    [SerializeField] string bossDoorName = "BossDoor";
    [Header("Teleport offset from door")]
    [SerializeField] Vector3 doorOffset = new Vector3(0, 0, -8);
    [Header("PlayerManager object name (holds Health + PlayerController)")]
    [SerializeField] string playerManagerName = "PlayerManager";

    GameObject playerManager;
    Health playerHealth;
    bool godMode = false;

    void Start()
    {
        // PlayerManager holds both PlayerController and Health in this project
        playerManager = GameObject.Find(playerManagerName);
        if (playerManager == null)
            Debug.LogWarning("[DemoHelper] '" + playerManagerName + "' not found!");
        else
        {
            playerHealth = playerManager.GetComponent<Health>();
            Debug.Log("[DemoHelper] PlayerManager: " + (playerHealth != null ? "Health OK" : "Health MISSING"));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.T)) { TeleportToBossDoor(); }
        if (Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown(KeyCode.G)) { ToggleGodMode(); }
        if (Input.GetKeyDown(KeyCode.F3) || Input.GetKeyDown(KeyCode.H)) { FullHeal(); }
    }

    Transform FindInScene(string name)
    {
        foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            var found = FindRecursive(root.transform, name);
            if (found != null) return found;
        }
        return null;
    }

    Transform FindRecursive(Transform t, string name)
    {
        if (t.name == name) return t;
        for (int i = 0; i < t.childCount; i++)
        {
            var f = FindRecursive(t.GetChild(i), name);
            if (f != null) return f;
        }
        return null;
    }

    void TeleportToBossDoor()
    {
        if (playerManager == null) { Debug.LogWarning("[DemoHelper] No PlayerManager, cannot teleport."); return; }
        Transform door = FindInScene(bossDoorName);
        if (door == null) { Debug.LogWarning("[DemoHelper] BossDoor not found."); return; }
        Vector3 dest = door.position + doorOffset;
        dest.y = door.position.y;
        playerManager.transform.position = dest;
        Debug.Log("[DemoHelper] Teleported to BossDoor at " + door.position);
    }

    void ToggleGodMode()
    {
        if (playerHealth == null) { Debug.LogWarning("[DemoHelper] No Health, cannot godmode."); return; }
        godMode = !godMode;
        playerHealth.SetIgnoreDamage(godMode);
        Debug.Log("[DemoHelper] God mode: " + (godMode ? "ON" : "OFF"));
    }

    void FullHeal()
    {
        if (playerHealth == null) { Debug.LogWarning("[DemoHelper] No Health, cannot heal."); return; }
        playerHealth.Heal(Mathf.RoundToInt(playerHealth.GetMaxHealth()));
        Debug.Log("[DemoHelper] Healed to full.");
    }
}
