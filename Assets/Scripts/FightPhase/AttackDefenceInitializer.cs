using UnityEngine;

public class AttackDefenceInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject attackObject;
    
    [SerializeField]
    private GameObject defendObject;

    public void InstantiateAttackObject()
    {
        InstantiateObject(attackObject);
    }
    public void InstantiateDefendObject()
    {
        InstantiateObject(defendObject);
    }
    
    private void InstantiateObject(GameObject obj)
    {
        GameObject temp = Instantiate(obj, gameObject.transform);
        temp.transform.localPosition = new Vector3(0, 0, 0);
    }
}
