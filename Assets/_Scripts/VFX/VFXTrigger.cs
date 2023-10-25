using UnityEngine;

public class VFXTrigger : MonoBehaviour
{
    [SerializeField] GameObject vfxPrefab;
    
    public void TriggerEffects()
    {
        vfxPrefab.SetActive(true);
    }
}
