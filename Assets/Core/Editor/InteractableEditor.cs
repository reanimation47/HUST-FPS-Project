using UnityEditor;

[CustomEditor(typeof(Interactable), true)]

public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable _interactable = (Interactable)target;
        base.OnInspectorGUI();
        if(_interactable.useEvents)
        {
            if(_interactable.GetComponent<InteractionEvent>() == null)
            {
                _interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }else
        {
            if(_interactable.GetComponent<InteractionEvent>() != null)
            {
                DestroyImmediate(_interactable.GetComponent<InteractionEvent>());
            }
        }
    }
}
