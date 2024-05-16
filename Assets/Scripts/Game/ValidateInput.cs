using TMPro;
using UnityEngine;
using static TMPro.TMP_InputField;

public class ValidateInput : MonoBehaviour
{
    [SerializeField] private CharacterValidation characterValidation;

    private TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.characterValidation = characterValidation;
    }
}
