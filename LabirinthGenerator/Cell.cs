using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace LabirinthGenerator
{
    class Cell
    {
        // exits clockwise from up
        // 0: up; 1: right; 2: down; 3: left
        // 0 - wall or udefined; 1 - passage
        public int[] exits = {0, 0, 0, 0};
        int[] emptyExits = { 0, 0, 0, 0 };
        public static Vector2i[] dirs = { new Vector2i(0, -1), new Vector2i(1, 0), new Vector2i(0, 1), new Vector2i(-1, 0) };
        public Vector2i position;
        Vector2f tipPos = new Vector2f(0, 0);


        public Cell(int _x, int _y)
        {
            position = new Vector2i(_x, _y);
        }


        public bool IsEmpty()
        {
            return Enumerable.SequenceEqual(exits, emptyExits);
        }


        public void Draw(RenderWindow rw, Vector2f position, float width, float height)
        {
            uint pointCount = (uint)(exits[0] + exits[1] + exits[2] + exits[3]) * 2;
            if (pointCount == 2)
                pointCount++;
            uint t = 0;
            ConvexShape shape = new ConvexShape(pointCount);
            RectangleShape rect = new RectangleShape();
            shape.Position = position;
            shape.FillColor = new Color(250, 240, 136);
            shape.OutlineThickness = 0;
            
            rect.Position = position;
            rect.Size = new Vector2f(width, height);
            rect.OutlineThickness = 1;
            rect.FillColor = Color.Black;

            if (Enumerable.SequenceEqual(exits, emptyExits))
                return;
            
            if (exits[0] == 1)
            {
                shape.SetPoint(t++, new Vector2f(width / 4, 0));
                shape.SetPoint(t++, new Vector2f((width / 4) * 3, 0));
            }

            if (exits[1] == 1)
            {
                shape.SetPoint(t++, new Vector2f(width, height / 4));
                shape.SetPoint(t++, new Vector2f(width, (height / 4) * 3));
            }

            if (exits[2] == 1)
            {
                shape.SetPoint(t++, new Vector2f((width / 4) * 3, height));
                shape.SetPoint(t++, new Vector2f(width / 4, height));
            }

            if (exits[3] == 1)
            {
                shape.SetPoint(t++, new Vector2f(0, (height / 4) * 3));
                shape.SetPoint(t++, new Vector2f(0, height / 4));
            }

            if (pointCount == 3)
            {
                if(tipPos.X == 0 && tipPos.Y == 0)
                    tipPos = new Vector2f(width / 2 + Labirinth.random.Next(-(int)width / 3, (int)width / 3), height / 2 + Labirinth.random.Next(-(int)height / 3, (int)height / 3));
                shape.SetPoint(t, tipPos);
            }

            rw.Draw(rect);
            rw.Draw(shape);
        }
        

        public char ToASCII()  // magic, do not touch
        {
            if (Enumerable.SequenceEqual(exits, emptyExits))
                return ' ';
            if (exits[0] == 0)
            {
                if (exits[1] == 0)
                {
                    /*if (exits[2] == 0)
                        return '╸';
                    if (exits[3] == 0)
                        return '╻';*/
                    return '╗';
                }
                if (exits[2] == 0)
                {
                    /*if (exits[3] == 0)
                        return '╺';*/
                    return '═';
                }
                if (exits[3] == 0)
                    return '╔';
                return '╦';
            }
            if(exits[1] == 0)
            {
                if (exits[2] == 0)
                {
                    /*if (exits[3] == 0)
                        return '╹';*/
                    return '╝';
                }
                if (exits[3] == 0)
                    return '║';
                return '╣';
            }
            if (exits[2] == 0)
            {
                if (exits[3] == 0)
                    return '╚';
                return '╩';
            }
            if (exits[3] == 0)
                return '╠';
            return '╬';
        }
    }
}
