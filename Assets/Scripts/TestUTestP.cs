using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TestUTestP : MonoBehaviour
{
    public Camera mainCamera;
    public Camera uiCamera;
    public Canvas rootCanvas;
    public UnityEngine.UI.Slider slider_renderScale;
    public TextMeshProUGUI renderScaleText;
    public TextMeshProUGUI resolutionText;

    public Toggle mToggle_depth;
    public Toggle mToggle_Opaque;

    public Toggle mToggle_MainLightShadow;

    public TMP_Dropdown var_Dropdown_upfilter;
    public TMP_Dropdown var_Dropdown_shadow_resolution;

    public Slider var_cascadeSlider;
    public TextMeshProUGUI cascadeText;

    public Slider var_ShadowDistanceSlider;
    public TextMeshProUGUI shadowDisValueStrText;

    public Toggle var_Toggle_HDR;

    public TMP_Dropdown var_Dropdown_ui_renderMode;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = Camera.main;

        slider_renderScale.value = (int)(UniversalRenderPipeline.asset.renderScale * 100);
        renderScaleText.text = UniversalRenderPipeline.asset.renderScale.ToString();

        mToggle_depth.isOn = UniversalRenderPipeline.asset.supportsCameraDepthTexture;
        mToggle_Opaque.isOn = UniversalRenderPipeline.asset.supportsCameraOpaqueTexture;

        mToggle_MainLightShadow.isOn = UniversalRenderPipeline.asset.supportsMainLightShadows;
        var_Dropdown_shadow_resolution.enabled = mToggle_MainLightShadow.isOn;
        var_Dropdown_upfilter.value = (int)UniversalRenderPipeline.asset.upscalingFilter - 1;
        var_Dropdown_shadow_resolution.value = UniversalRenderPipeline.asset.mainLightShadowmapResolution / 256 - 1;
        var_cascadeSlider.value = UniversalRenderPipeline.asset.shadowCascadeCount;
        cascadeText.text = UniversalRenderPipeline.asset.shadowCascadeCount.ToString();
        var_ShadowDistanceSlider.value = UniversalRenderPipeline.asset.shadowDistance;
        shadowDisValueStrText.text = UniversalRenderPipeline.asset.shadowDistance.ToString();

        onToggleMainLightShadow();

        var_Toggle_HDR.isOn = UniversalRenderPipeline.asset.supportsHDR;


        var_Dropdown_ui_renderMode.value = uiCamera.gameObject.activeSelf ? 1:0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onRenderScaleChanged()
    {
        UniversalRenderPipeline.asset.renderScale = slider_renderScale.value / 100f;
        renderScaleText.text = UniversalRenderPipeline.asset.renderScale.ToString();
        resolutionText.text = string.Format("{0}*{1}", 
            (int)(UniversalRenderPipeline.asset.renderScale * Screen.width), (int)(UniversalRenderPipeline.asset.renderScale * Screen.height));
    }

    public void onToggleDepth()
    {
        UniversalRenderPipeline.asset.supportsCameraDepthTexture = mToggle_depth.isOn;
    }

    public void onToggleOpaque()
    {
        UniversalRenderPipeline.asset.supportsCameraOpaqueTexture = mToggle_Opaque.isOn;
    }

    public void onToggleMainLightShadow()
    {
        UniversalRenderPipeline.asset.supportsMainLightShadows = mToggle_MainLightShadow.isOn;
        var_Dropdown_shadow_resolution.enabled = mToggle_MainLightShadow.isOn;
        var addiCameraData = mainCamera.GetComponent<UniversalAdditionalCameraData>();
        addiCameraData.renderShadows = mToggle_MainLightShadow.isOn;
    }

    public void onShadowResolutionChanged()
    {
        UniversalRenderPipeline.asset.mainLightShadowmapResolution = (1 << (var_Dropdown_shadow_resolution.value)) * 256;
    }
    public void onCascadeChanged()
    {
        UniversalRenderPipeline.asset.shadowCascadeCount = Mathf.Clamp((int)var_cascadeSlider.value, 1, (int)var_cascadeSlider.maxValue);
        cascadeText.text = UniversalRenderPipeline.asset.shadowCascadeCount.ToString();
    }
    public void onShadownDistanceChanged()
    {
        UniversalRenderPipeline.asset.shadowDistance = var_ShadowDistanceSlider.value;
        shadowDisValueStrText.text = UniversalRenderPipeline.asset.shadowDistance.ToString();
    }
    public void onUpFilterChanged()
    {
        UniversalRenderPipeline.asset.upscalingFilter = (UpscalingFilterSelection)(var_Dropdown_upfilter.value + 1);
    }

    public void onToggleHDR()
    {
        UniversalRenderPipeline.asset.supportsHDR = var_Toggle_HDR.isOn;
    }

    public void onToggleUIMode()
    {
        if(var_Dropdown_ui_renderMode.value == 0)
        {
            rootCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            rootCanvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
            rootCanvas.worldCamera = null;
            uiCamera.gameObject.SetActive(false);
        }
        else
        {
            rootCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            rootCanvas.worldCamera = uiCamera;
            rootCanvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
            uiCamera.gameObject.SetActive(true);
        }
    }
}
