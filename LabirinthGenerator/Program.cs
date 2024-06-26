using LabirinthGenerator;
using SFML.System;
using SFML.Window;
using SFML.Graphics;



VideoMode vm = new VideoMode(800, 800);
RenderWindow rw = new RenderWindow(vm, "Labirinth", Styles.Close, new ContextSettings(32, 32, 8));
Labirinth l = new Labirinth(30, 30);

bool done = false;
bool grid = false;

rw.KeyPressed += OnKey;
rw.Closed += OnClose;


while (rw.IsOpen)
{
    rw.DispatchEvents();
    rw.Clear();
    if(!done)
        done = l.GenerateStep();
    l.Draw(rw, grid);
    rw.Display();
}

void OnKey(object sender, KeyEventArgs e)
{
    switch (e.Code)
    {
        case Keyboard.Key.G:
            grid = !grid;
            break;
        case Keyboard.Key.R:
            l = new Labirinth(l.width, l.height);
            done = false;
            break;
    }
}

static void OnClose(object sender, EventArgs e)
{
    (sender as RenderWindow)?.Close();
}
