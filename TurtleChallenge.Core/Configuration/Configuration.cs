using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TurtleChallenge.Core.BLL;
using TurtleChallenge.Core.BusinessObjects;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Configuration
{

    public class Configuration : IConfiguration
    {
        List<FileItem> _items;
        List<FileItem> _moves;
        public Position TurtlePosition
        {
            get; set;
        }

        public Position ExitPosition
        {
            get; set;
        }

        public List<Position> MinesPosition
        {
            get; set;
        }

        public List<Position> Moves
        {
            get; set;
        }

        public Position GridDimension
        {
            get; set;
        }

        public Position StartPostion
        {
            get; set;
        }

        public Grid GameGrid
        {
            get; set;
        }
        public void LoadConfiguration()
        {
            LoadConfigurationFromFile();
            Grid grid = new Grid();
            grid.GridTiles = new Tile[GridDimension.xPos, GridDimension.yPos];

            StartPostion = TurtlePosition;
            grid.Load(StartPostion, ExitPosition, MinesPosition);

            GameGrid = grid;
        }

        public void LoadConfigurationFromFile()
        {
            LoadJSon();
            GridDimension = GetGridDimension();
            TurtlePosition = GetStartPositionTurtle();
            ExitPosition = GetExitPosition();
            MinesPosition = GetMinesPosition();
            Moves = GetMoves();
        }

        private void LoadJSon()
        {
            using (StreamReader r = new StreamReader("file.json"))
            {
                string json = r.ReadToEnd();
                _items = JsonConvert.DeserializeObject<List<FileItem>>(json);
            }
            using (StreamReader r = new StreamReader("fileMoves.json"))
            {
                string json = r.ReadToEnd();
                _moves = JsonConvert.DeserializeObject<List<FileItem>>(json);
            }
        }
        private Position GetGridDimension()
        {
            return GetPosition("GridDimensions");
        }

        private Position GetPosition(string itemType)
        {
            FileItem fileItem = _items.FirstOrDefault(item => item.ItemType.Equals(itemType));

            return new Position { xPos = fileItem.xPos, yPos = fileItem.yPos };
        }

        private List<Position> GetMoves()
        {
            List<Position> moves = new List<Position>();
            IList<FileItem> fileItems = _moves;

            foreach (FileItem FileItem in fileItems)
            {
                moves.Add(new Position { xPos = FileItem.xPos, yPos = FileItem.yPos });
            }
            return moves;
        }

        private List<Position> GetListFromTypeItem(string itemType)
        {
            List<Position> moves = new List<Position>();
            IList<FileItem> fileItems = _items.Where(item => item.ItemType.Equals(itemType)).ToList();

            foreach (FileItem FileItem in fileItems)
            {
                moves.Add(new Position { xPos = FileItem.xPos, yPos = FileItem.yPos });
            }
            return moves;
        }

        private List<Position> GetMinesPosition()
        {
            return GetListFromTypeItem("Mines");
        }

        private Position GetExitPosition()
        {
            return GetPosition("Exit");
        }

        private Position GetStartPositionTurtle()
        {

            return GetPosition("Turtle");
        }
    }
}

