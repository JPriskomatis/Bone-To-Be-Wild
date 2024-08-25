using System.Collections;
using UnityEngine;
using PlayerSpace;
using TMPro;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{
    private void OnEnable()
    {
        AbilityScores.OnCurrentHealthChange += DecreaseHealth;
    }

    private void OnDisable()
    {
        AbilityScores.OnCurrentHealthChange -= DecreaseHealth;
    }

    [SerializeField] private TextMeshPro HealthNumber;
    enum ShapeType
    {
        Circle, Box, Rhombus
    };

    [SerializeField] ShapeType _shape;
    [SerializeField, Range(0, 25)] float _healthNormalized;

    [Header("Fill")]
    [SerializeField] Gradient _lowToHighHealthTransition;

    [Header("Wave")]
    [SerializeField, Range(0, 0.1f)] float _fillWaveAmplitude;
    [SerializeField, Range(0, 100f)] float _fillWaveFrequency;
    [SerializeField, Range(0, 1f)] float _fillWaveSpeed;

    [Header("Background")]
    [SerializeField] Color _backgroundColor;

    [Header("Border")]
    [SerializeField, Range(0, 0.15f)] float _borderWidth;
    [SerializeField] Color _borderColor;

    Material _matInstance;

    /// <summary>
    /// Health value between 0 and 1
    /// </summary>
    public float HealthNormalized
    {
        get
        {
            return _healthNormalized;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 25);
            if (value == _healthNormalized) return;

            _healthNormalized = value;
            _matInstance.SetColor("_fillColor", _lowToHighHealthTransition.Evaluate(_healthNormalized));
            SetMaterialData();
        }
    }

    void Start()
    {
        var abilityScores = FindObjectOfType<AbilityScores>();
        if (abilityScores == null) return;

        float maxHealth = abilityScores.mainStats.maxHP;
        float currentHealth = abilityScores.mainStats.currentHP;

        // Set initial health normalized
        _healthNormalized = Mathf.Clamp01(currentHealth / maxHealth);

        // Set HealthNumber text
        HealthNumber.text = $"{currentHealth}/{maxHealth}";

        SetupUniqueMaterial();
        SetMaterialData();
        Debug.Log(_healthNormalized);
    }

    public void DecreaseHealth(int damage)
    {
        StartCoroutine(ReduceHealthOverTime(damage, 1f));
    }

    IEnumerator ReduceHealthOverTime(float damage, float duration)
    {
        var abilityScores = FindObjectOfType<AbilityScores>();
        if (abilityScores == null) yield break;

        float maxHealth = abilityScores.mainStats.maxHP;
        float startHealth = abilityScores.mainStats.currentHP;
        float targetHealth = Mathf.Clamp(startHealth - damage, 0f, maxHealth);

        // Calculate normalized values
        float startHealthNormalized = Mathf.Clamp01(startHealth / maxHealth);
        float targetHealthNormalized = Mathf.Clamp01(targetHealth / maxHealth);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            HealthNormalized = Mathf.Lerp(startHealthNormalized, targetHealthNormalized, elapsed / duration);
            yield return null;
        }

        // Update current health
        HealthNumber.text = $"{targetHealth}/{maxHealth}";

        // Final update of HealthNormalized
        HealthNormalized = targetHealthNormalized;
    }


    void SetupUniqueMaterial()
    {
        if (_matInstance != null) return;

        Debug.Log("Setup Material", this.gameObject);
        _matInstance = new Material(Shader.Find("CustomShaders/HealthBar"));
        if (Application.isPlaying)
        {
            GetComponent<Renderer>().material = _matInstance;
        }
        else
        {
            GetComponent<Renderer>().sharedMaterial = _matInstance;
        }
    }

    void SetMaterialData()
    {
        if (_matInstance == null) return;

        _matInstance.SetFloat("_healthNormalized", _healthNormalized);

        SetKeyword();

        _matInstance.SetFloat("_waveAmp", _fillWaveAmplitude);
        _matInstance.SetFloat("_waveFreq", _fillWaveFrequency);
        _matInstance.SetFloat("_waveSpeed", _fillWaveSpeed);

        _matInstance.SetColor("_fillColor", _lowToHighHealthTransition.Evaluate(_healthNormalized));

        _matInstance.SetColor("_backgroundColor", _backgroundColor);
        _matInstance.SetFloat("_borderWidth", _borderWidth);
        _matInstance.SetColor("_borderColor", _borderColor);
    }

    void SetKeyword()
    {
        foreach (var kword in _matInstance.shaderKeywords)
        {
            _matInstance.DisableKeyword(kword);
        }
        if (_shape == ShapeType.Circle) _matInstance.EnableKeyword("_SHAPE_CIRCLE");
        else if (_shape == ShapeType.Box) _matInstance.EnableKeyword("_SHAPE_BOX");
        else if (_shape == ShapeType.Rhombus) _matInstance.EnableKeyword("_SHAPE_RHOMBUS");

        // Sync shader keywordEnum
        _matInstance.SetInt("_shape", (int)_shape);
    }

    void OnValidate()
    {
        SetMaterialData();
    }

    void OnDestroy()
    {
        if (_matInstance != null)
        {
            if (Application.isPlaying)
                Destroy(_matInstance);
            else
                DestroyImmediate(_matInstance);
        }
    }
}