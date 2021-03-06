using LabirinthGenerator;
using SFML.System;
using SFML.Window;
using SFML.Graphics;



VideoMode vm = new VideoMode(800, 800);
RenderWindow rw = new RenderWindow(vm, "Labirinth", Styles.Close, new ContextSettings(32, 32, 8));
Labirinth l = new Labirinth(30, 30);

rw.Closed += OnClose;


while (rw.IsOpen)
{
    rw.DispatchEvents();
    rw.Clear();
    l.GenerateStep();
    l.Draw(rw);
    rw.Display();
}



static void OnClose(object sender, EventArgs e)
{
    (sender as RenderWindow)?.Close();
}
