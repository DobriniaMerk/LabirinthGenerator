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
        public Vector2i position;

        public Cell(int _x, int _y)
        {
            position = new Vector2i(_x, _y);
        }

        public bool IsEmpty()
        {
            return Enumerable.SequenceEqual(exits, emptyExits);
        }

        public void Draw(RenderWindow rw)
        {

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
