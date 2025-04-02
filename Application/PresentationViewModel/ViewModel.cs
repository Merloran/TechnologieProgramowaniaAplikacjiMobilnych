using PresentationViewModel.MVVMLight;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using Data;
using PresentationModel;

namespace PresentationViewModel
{
    public class ViewModel : ViewModelBase
    {
        private Model _model;
        private List<ModelPlayer>? _players;
        public List<ModelPlayer>? Players
        {
            get => _players;
            set
            {
                _players = value;
                RaisePropertyChanged();
            }
        }

        public ICommand JoinGameClick { get; set; }
        public ICommand HostGameClick { get; set; }

        public ICommand MoveUpClick { get; set; }
        public ICommand MoveDownClick { get; set; }
        public ICommand MoveLeftClick {  get; set; }
        public ICommand MoveRightClick {  get; set; }

        private IVector2 _reactiveRectanglePosition;
        public IVector2 ReactiveRectanglePosition
        {
            get => _reactiveRectanglePosition;
            set
            {
                _reactiveRectanglePosition = value;
                RaisePropertyChanged();
            }
        }

        public ViewModel() 
        {
            _model = new Model(UpdateDisplayedPlayers, UpdateReactiveElements);
            JoinGameClick  = new RelayCommand(_model.AddPlayer);
            HostGameClick  = new RelayCommand(_model.AddPlayer);
            MoveUpClick    = new RelayCommand(_model.MoveUp);
            MoveDownClick  = new RelayCommand(_model.MoveDown);
            MoveLeftClick  = new RelayCommand(_model.MoveLeft);
            MoveRightClick = new RelayCommand(_model.MoveRight);
            ReactiveRectanglePosition = IVector2.Create(400.0f, 30.0f);
            UpdateDisplayedPlayers();
        }

        public void UpdateDisplayedPlayers()
        {
            Players = _model.GetPlayers();
        }

        public void UpdateReactiveElements(bool b)
        {
            ReactiveRectanglePosition += b ? IVector2.Create(10.0f, 0.0f) : IVector2.Create(-10.0f, 0.0f);
        }
    }
}
