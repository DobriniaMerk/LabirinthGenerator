using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace LabirinthGenerator
{
    class Labirinth
    {
        Cell[,] cells;
        Vector2i[] dirs = { new Vector2i(0, -1), new Vector2i(1, 0), new Vector2i(0, 1), new Vector2i(-1, 0) };
        Random random = new Random();
        int width, height;

        public Labirinth(int _width, int _height)
        {
            cells = new Cell[_width, _height];
            width = _width;
            height = _height;
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    cells[w, h] = new Cell(w, h);
        }

        public void Generate(int x, int y)
        {
            int exit = RandomExit(cells[x, y]);
            Vector2i ext;
            while (exit > 0)
            {
                ext = dirs[exit];
                cells[x, y].exits[exit] = 1;
                cells[x + ext.X, y + ext.Y].exits[(exit + 2) % 4] = 1;

                Generate(x + ext.X, y + ext.Y);

                exit = RandomExit(cells[x, y]);
            }
        }

        int RandomExit(Cell cell)
        {
            List<int> t = new List<int>{0, 1, 2, 3};

            while (t.Count > 0)
            {
                int r = t[random.Next(t.Count)];
                try
                {
                    Cell c = cells[(dirs[r].X + cell.position.X), (dirs[r].Y + cell.position.Y)];
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

        public void Draw(RenderWindow rw)
        {
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    cells[w, h].Draw(rw);
                }
            }
        }
    }
}
