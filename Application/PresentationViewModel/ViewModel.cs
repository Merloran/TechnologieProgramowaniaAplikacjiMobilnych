using PresentationViewModel.MVVMLight;
using System.Windows.Input;
using PresentationModel;

namespace PresentationViewModel
{
    public class ViewModel : ViewModelBase
    {
        private IModel model;
        private List<ViewModelPlayer> players;
        public List<ViewModelPlayer> Players
        {
            get => players;
            set
            {
                players = value;
                RaisePropertyChanged();
            }
        }

        public ICommand MoveUpClick { get; set; }
        public ICommand MoveDownClick { get; set; }
        public ICommand MoveLeftClick {  get; set; }
        public ICommand MoveRightClick {  get; set; }

        public ViewModel()
        {
            model = IModel.Create(UpdatePlayers);
            players = new List<ViewModelPlayer>();

            MoveUpClick    = new RelayCommand(model.MoveUp);
            MoveDownClick  = new RelayCommand(model.MoveDown);
            MoveLeftClick  = new RelayCommand(model.MoveLeft);
            MoveRightClick = new RelayCommand(model.MoveRight);
        }

        public void UpdatePlayers()
        {
            Players = model.GetPlayers()
                           .Select(player => new ViewModelPlayer(player.Name, player.X, player.Y, player.Speed))
                           .ToList();
        }
    }
}
