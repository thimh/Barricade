using System;
using BarricaeModelLib.GeneratedCode.Model;
using Model;
using View;

namespace BarricaeModelLib.GeneratedCode.Controller
{
    public class BarricadeController
    {
        private readonly OutputView _outputView;

        public Game Game { get; set; }

        public BarricadeController(Game game)
        {
            _outputView = new OutputView();
            Game = game;
        }


        public void MoveBarricade(Barricade barricade, Field currentField)
        {
            _outputView.ShowDirection();
            while (true)
            {
                currentField.Barricade = null;
                _outputView.ClearConsole();
                _outputView.ShowBoard(Game.Fields);
                currentField.TempIcon = false;

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        if (CanMoveBarricade(Game.Fields[currentField.LocationY - 1, currentField.LocationX]))
                        {
                            currentField = Game.Fields[currentField.LocationY - 2, currentField.LocationX];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.S:
                        if (CanMoveBarricade(Game.Fields[currentField.LocationY + 1, currentField.LocationX]))
                        {
                            currentField = Game.Fields[currentField.LocationY + 2, currentField.LocationX];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.A:
                        if (CanMoveBarricade(Game.Fields[currentField.LocationY, currentField.LocationX - 1]))
                        {
                            currentField = Game.Fields[currentField.LocationY, currentField.LocationX - 2];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.D:
                        if (CanMoveBarricade(Game.Fields[currentField.LocationY, currentField.LocationX + 1]))
                        {
                            currentField = Game.Fields[currentField.LocationY, currentField.LocationX + 2];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (CanPlaceBarricade(currentField))
                        {
                            currentField.Barricade = barricade;
                            return;
                        }
                        break;
                }
                currentField.TempIcon = true;
            }
        }

        public bool CanMoveBarricade(Field nextField)
        {
            return nextField != null && nextField.GetType() == typeof(PathField);
        }

        public bool CanPlaceBarricade(Field currentField)
        {
            if (currentField.GetType() == typeof(RestField) || currentField.GetType() == typeof(ForestField) || currentField.GetType() == typeof(FinishField)) return false;
            if (currentField.Pawn != null || currentField.Barricade != null) return false;
            return currentField.LocationX != 14;
        }
    }
}