using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace LabirinthGenerator
{
    public class Labirinth
    {
        Cell[,] cells;
        
        public static Random random = new Random();
        public int width, height;
        int t = 0;
        List<Cell> stack = new List<Cell>();
        Cell startCell;

        public Labirinth(int _width, int _height)
        {
            cells = new Cell[_width, _height];
            width = _width;
            height = _height;
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    cells[w, h] = new Cell(w, h);
            cells[0, 0].exits[0] = 1;

            startCell = cells[0, 0];
            stack.Add(startCell);
        }

        public void GenerateRec(int x, int y)
        {
            int exit = RandomExit(cells[x, y]);
            Vector2i ext;
            while (exit > 0)
            {
                ext = Cell.dirs[exit];
                cells[x, y].exits[exit] = 1;
                cells[x + ext.X, y + ext.Y].exits[(exit + 2) % 4] = 1;

                GenerateRec(x + ext.X, y + ext.Y);

                exit = RandomExit(cells[x, y]);
            }
            Console.WriteLine(++t);
        }


        public bool GenerateStep()
        {
            if (stack.Count == 0)
                return true;

            Cell cell = stack.Last();
            int exit = RandomExit(cell);

            if (RandomExit(cell) == -1)
            {
                // no neighbours
                stack.Remove(cell);
                return false;
            }
            // else
            Vector2i ext = Cell.dirs[exit];
            cell.exits[exit] = 1;
            cells[cell.position.X + ext.X, cell.position.Y + ext.Y].exits[(exit + 2) % 4] = 1;
            stack.Add(cells[cell.position.X + ext.X, cell.position.Y + ext.Y]);
            return false;
        }


        int RandomExit(Cell cell)
        {
            List<int> t = new List<int>{0, 1, 2, 3};

            while (t.Count > 0)
            {
                int r = t[random.Next(t.Count)];
                try
                {
                    Cell c = cells[(Cell.dirs[r].X + cell.position.X), (Cell.dirs[r].Y + cell.position.Y)];
                    if (c.IsEmpty())
                        return r;
                    else
                        t.Remove(r);
                }
                catch
                {
                    t.Remove(r);
                }
            }
            return -1;
        }


        public void DrawConsole()
        {
            for(int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    Console.Write(cells[w, h].ToASCII());
                }
                Console.WriteLine();
            }
        }

        public void Draw(RenderWindow rw, bool drawGrid = false)
        {
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    Vector2f position = new Vector2f(w * rw.Size.X / width, h * rw.Size.Y / height);

                    if(drawGrid)
                    {
                        RectangleShape rect = new RectangleShape();

                        rect.Position = position;
                        rect.Size = new Vector2f(rw.Size.X / width, rw.Size.Y / height);
                        rect.OutlineThickness = 1;
                        rect.FillColor = Color.Black;

                        rw.Draw(rect);
                    }

                    cells[w, h].Draw(rw, position, rw.Size.X / width, rw.Size.Y / height);
                    
                }
            }
        }


        public void SetStartCell(int x, int y)
        {
            startCell = cells[x, y];
            stack.Clear();
            stack.Add(startCell);
        }
    }
}
