using FreedLOW.Painting.Extensions;
using FreedLOW.Painting.Infrastructure.Factories;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FreedLOW.Painting.UI
{
    public class HUD : MonoBehaviour
    {
        [Header("Color palette")]
        [SerializeField] private CanvasGroup colorPaletteCanvas;
        [SerializeField] private Slider brushSizeSlider;
        [SerializeField] private TextMeshProUGUI brushSizeText;
        [SerializeField] private Button colorButton;
        [SerializeField] private TextMeshProUGUI colorButtonText;

        [Header("Buttons")]
        [SerializeField] private Button cubeButton;
        [SerializeField] private Button sphereButton;
        [SerializeField] private Button capsuleButton;
        [SerializeField] private Button clearPaintButton;
        [SerializeField] private Button savePaintButton;
        
        private bool _isOpen;
        
        private IPrimitiveFactory _primitiveFactory;

        [Inject]
        private void Construct(IPrimitiveFactory primitiveFactory)
        {
            _primitiveFactory = primitiveFactory;
        }

        private void Start()
        {
            colorPaletteCanvas.State(false);
            
            brushSizeSlider.onValueChanged.AddListener(OnBrushSizeChanged);
            brushSizeSlider.onValueChanged.Invoke(brushSizeSlider.value);
            
            colorButton.onClick.AddListener(OnControlColorPalette);
            
            cubeButton.onClick.AddListener(OnCreateCube);
            sphereButton.onClick.AddListener(OnCreateSphere);
            capsuleButton.onClick.AddListener(OnCreateCapsule);
            
            clearPaintButton.onClick.AddListener(OnClearPaintData);
            savePaintButton.onClick.AddListener(OnSavePaintData);
        }

        private void OnBrushSizeChanged(float value)
        {
            brushSizeText.text = $"Brush size: {value:F1}";
        }

        private void OnControlColorPalette()
        {
            if (!_isOpen)
            {
                _isOpen = true;
                colorButtonText.text = "Confirm";
            }
            else
            {
                _isOpen = false;
                colorButtonText.text = "Select color";
            }
            
            colorPaletteCanvas.State(_isOpen);
        }

        private void OnCreateCube()
        {
            _primitiveFactory.CreateCube(Vector3.zero);
        }

        private void OnCreateSphere()
        {
            _primitiveFactory.CreateSphere(Vector3.zero);
        }

        private void OnCreateCapsule()
        {
            _primitiveFactory.CreateCapsule(Vector3.zero);
        }

        private void OnClearPaintData()
        {
            
        }

        private void OnSavePaintData()
        {
            
        }
    }
}