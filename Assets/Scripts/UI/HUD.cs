using Cysharp.Threading.Tasks;
using FreedLOW.Painting.Extensions;
using FreedLOW.Painting.Infrastructure.Factories;
using FreedLOW.Painting.Infrastructure.Services.Draw;
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
        [SerializeField] private CUIColorPicker colorPicker;
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
        private string _activeObjectName;
        
        private IPrimitiveFactory _primitiveFactory;
        private IPaintService _paintService;
        private ISaveLoadDrawDataService _saveLoadService;

        [Inject]
        private void Construct(IPrimitiveFactory primitiveFactory, IPaintService paintService,
            ISaveLoadDrawDataService saveLoadService)
        {
            _primitiveFactory = primitiveFactory;
            _paintService = paintService;
            _saveLoadService = saveLoadService;
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
            
            colorPicker.SetOnValueChangeCallback(OnColorChanged);
            _paintService.SetBrushColor(colorPicker.Color);
        }

        private void OnBrushSizeChanged(float value)
        {
            brushSizeText.text = $"Brush size: {value}";
            _paintService.SetBrushSize((int)value);
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

        private async void OnCreateCube()
        {
            cubeButton.interactable = false;
            
            GameObject cube = _primitiveFactory.CreateCube(Vector3.zero);
            _activeObjectName = cube.name;
            
            await LoadOrInitializeTexture(cube);

            cubeButton.interactable = true;
        }

        private async void OnCreateSphere()
        {
            sphereButton.interactable = false;
            
            GameObject sphere = _primitiveFactory.CreateSphere(Vector3.zero);
            _activeObjectName = sphere.name;
            
            await LoadOrInitializeTexture(sphere);

            sphereButton.interactable = true;
        }

        private async void OnCreateCapsule()
        {
            capsuleButton.interactable = false;
            
            GameObject capsule = _primitiveFactory.CreateCapsule(Vector3.zero);
            _activeObjectName = capsule.name;
            
            await LoadOrInitializeTexture(capsule);

            capsuleButton.interactable = true;
        }

        private void OnClearPaintData() => 
            _paintService.ClearTexture();

        private void OnSavePaintData() => 
            _saveLoadService.SaveTexture(_activeObjectName);

        private void OnColorChanged(Color color) => 
            _paintService.SetBrushColor(color);

        private async UniTask LoadOrInitializeTexture(GameObject go)
        {
            var texture = await _saveLoadService.LoadTexture(_activeObjectName);
            if (texture is null) 
                _paintService.InitializeTexture();
            
            go.GetComponent<Renderer>().material.mainTexture = _paintService.Texture;

            _paintService.InitializePaintTarget(go.GetComponent<Collider>());
        }
    }
}