using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _221212_vizsgafel
{
    internal class Game
    {
        private int _year;
        private string _title;
        private string _genre;
        private string _publisher;
        private string[] _platforms;

        public Game(int year, string title, string genre, string publisher, string[] platforms)
        {
            _year = year;
            _title = title;
            _genre = genre;
            _publisher = publisher;
            _platforms = platforms;
        }

        public Game(string line)
        {
            string[] _ = line.Split(';');
            _year = int.Parse(_[0]);
            _title = _[1];
            _genre = _[2];
            _publisher = _[3];
            _platforms = _[4].Split(", ");
        }

        public int Year { get => _year; }
        public string Title { get => _title; }
        public string Genre { get => _genre; }
        public string Publisher { get => _publisher; }
        public string[] Platforms { get => _platforms; }
    }
}
