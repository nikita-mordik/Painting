using FreedLOW.Painting.Infrastructure.Services.Draw;
using FreedLOW.Painting.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace FreedLOW.Painting.Game
{
    public class Painter : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        
        private IInputService _inputService;
        private IPaintService _paintService;

        [Inject]
        private void Construct(IInputService inputService, IPaintService paintService)
        {
            _inputService = inputService;
            _paintService = paintService;
        }

        private void Update()
        {
            if (_inputService.HasClick())
            {
                Ray ray = mainCamera.ScreenPointToRay(_inputService.GetInput());
                _paintService.Draw(ray);
            }
        }
    }
}