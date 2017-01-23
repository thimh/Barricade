using System;
using BarricaeModelLib.GeneratedCode.Model;
using BarricaeModelLib.GeneratedCode.Model.Fields;
using View;

namespace BarricaeModelLib.GeneratedCode.Controller
{
    public class BarricadeController
    {
        private readonly OutputView _outputView;
        private readonly Game _game;

        public BarricadeController(Game game)
        {
            _outputView = new OutputView();
            _game = game;
        }

        public void MoveBarricade(Barricade barricade, Field currentField)
        {
            _outputView.ShowDirection();
            while (true)
            {
                currentField.Barricade = null;
                _outputView.ClearConsole();
                _outputView.ShowBoard(_game.Fields);
                currentField.TempIcon = false;

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        if (CanMoveBarricade(_game.Fields[currentField.LocationY - 1, currentField.LocationX]))
                        {
                            currentField = _game.Fields[currentField.LocationY - 2, currentField.LocationX];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.S:
                        if (CanMoveBarricade(_game.Fields[currentField.LocationY + 1, currentField.LocationX]))
                        {
                            currentField = _game.Fields[currentField.LocationY + 2, currentField.LocationX];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.A:
                        if (CanMoveBarricade(_game.Fields[currentField.LocationY, currentField.LocationX - 1]))
                        {
                            currentField = _game.Fields[currentField.LocationY, currentField.LocationX - 2];
                            currentField.Barricade = barricade;
                        }
                        break;

                    case ConsoleKey.D:
                        if (CanMoveBarricade(_game.Fields[currentField.LocationY, currentField.LocationX + 1]))
                        {
                            currentField = _game.Fields[currentField.LocationY, currentField.LocationX + 2];
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
            return nextField.GetType() == typeof(PathField);
        }

        public bool CanPlaceBarricade(Field currentField)
        {
            if (currentField.Pawn != null || currentField.Barricade != null) return false;
            if (currentField.CanHaveBarricade == false) return false;
            return currentField.LocationX != 14;
        }
    }
}