using System;
using Data;
using PresentationModel;

namespace PresentationViewModel
{
    public class ViewModelPlayer(string name, float x, float y, float speed) : IModelPlayer
    {
        public string Name { get; set; } = name;
        public float X { get; set; } = x;
        public float Y { get; set; } = y;
        public float Speed { get; set; } = speed;
    }
}