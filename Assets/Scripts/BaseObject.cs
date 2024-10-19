using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public bool Activity => gameObject.activeSelf;

    public virtual void Activate() =>
    SetActivity(true);

    public virtual void Deactivate() =>
        SetActivity(false);

    protected void SetActivity(bool value) =>
        gameObject.SetActive(value);
}
