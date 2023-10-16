using UnityEditor;

[CustomEditor(typeof(Interactable), true)]

public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable _interactable = (Interactable)target;
        if(target.GetType() == typeof(EventOnlyInteractable))
        {
            _interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", _interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteractable can ONLY use UnityEvents", MessageType.Info);
            if (_interactable.GetComponent<InteractionEvent>() == null)
            {
                _interactable.useEvents = true;
                _interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            if (_interactable.useEvents)
            {
                if (_interactable.GetComponent<InteractionEvent>() == null)
                {
                    _interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else
            {
                if (_interactable.GetComponent<InteractionEvent>() != null)
                {
                    DestroyImmediate(_interactable.GetComponent<InteractionEvent>());
                }
            }
        }
        
    }
}
