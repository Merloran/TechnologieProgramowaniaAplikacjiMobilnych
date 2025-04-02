using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace PresentationModel
{
    public class ModelPlayer(ILogicPlayer player) : INotifyPropertyChanged
    {
        public float X { get; private set; } = player.X;
        public float Y { get; private set; } = player.Y;

        public event PropertyChangedEventHandler? PropertyChanged;
    
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}