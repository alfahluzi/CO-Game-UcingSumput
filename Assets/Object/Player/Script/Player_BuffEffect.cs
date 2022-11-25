using UnityEngine;


public class Player_BuffEffect : MonoBehaviour
{
    public float lupTimer, botolTimer, onigiriTimer;
    public float lupTime, botolTime, onigiriTime;
    public float lupNormal, lupZoomed;
    public bool lupIsActive = false, lupIsReady = false;
    [HideInInspector] public bool botolIsActive = false, botolIsReady = false;
    [HideInInspector] public bool oniIsActive = false, oniIsReady = false;

    public bool invicible;
    public FielOfView fov;
    public SpriteRenderer spriteRenderer;
    public StaminaPlayer staminaPlayer;
    public UnityEngine.Rendering.Universal.Light2D LightBack, LightMid;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        invicible = botolIsActive;
        if (lupIsActive)
        {
            //if (lupIsReady)
            LupEffect();
            if (lupTimer > 0)
                lupTimer -= Time.deltaTime;
            else
                lupIsActive = false;
        }
        else LupDeffect();

        if (botolIsActive)
        {
            if (botolIsReady)
                BotolEffect();
            if (botolTimer > 0)
                botolTimer -= Time.deltaTime;
            else
            {
                BotolDeffect();
                botolIsActive = false;
            }
        }

        if (oniIsActive)
        {
            if (oniIsReady)
                OnigiriEffect();
            if (onigiriTimer > 0)
                onigiriTimer -= Time.deltaTime;
            else
            {
                OnigiriDeffect();
                oniIsActive = false;
            }
        }
    }


    private void LupEffect()
    {
        if (LightBack.pointLightOuterRadius < lupZoomed)
            LightBack.pointLightOuterRadius += lupZoomed * Time.deltaTime;

        if (LightMid.pointLightOuterRadius < lupZoomed)
            LightMid.pointLightOuterRadius += lupZoomed * Time.deltaTime;

        Color color = spriteRenderer.material.GetColor("_EmissionColor");
        spriteRenderer.material.SetColor("_EmissionColor", new Color(color.r, color.g, color.b) * 10);
    }
    private void LupDeffect()
    {
        if (LightBack.pointLightOuterRadius > lupNormal)
            LightBack.pointLightOuterRadius -= lupNormal * Time.deltaTime;

        if (LightMid.pointLightOuterRadius > lupNormal)
            LightMid.pointLightOuterRadius -= lupNormal * Time.deltaTime;

        Color color = spriteRenderer.material.GetColor("_EmissionColor");
        spriteRenderer.material.SetColor("_EmissionColor", new Color(color.r, color.g, color.b) / 10);

        lupTimer = lupTime;
    }

    private void BotolEffect()
    {
        Color color = spriteRenderer.material.GetColor("_MainColor");
        float intensity = (color.r + color.g + color.b) / 3f;
        float factor = 1 / intensity;
        spriteRenderer.material.SetColor("_MainColor", new Color(color.r * factor, color.g * factor, color.b * factor));
    }
    private void BotolDeffect()
    {
        Color color = spriteRenderer.material.GetColor("_MainColor");
        float intensity = (color.r + color.g + color.b) / 3f;
        float factor = 1 / intensity;
        spriteRenderer.material.SetColor("_MainColor", new Color(color.r * factor, color.g * factor, color.b * factor));

        botolTimer = botolTime;
    }

    private void OnigiriEffect()
    {
        staminaPlayer.onigiriBuff = true;
    }
    private void OnigiriDeffect()
    {
        staminaPlayer.onigiriBuff = false;
        onigiriTimer = onigiriTime;
    }
}
