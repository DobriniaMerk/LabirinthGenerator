using LabirinthGenerator;
using SFML.System;
using SFML.Window;
using SFML.Graphics;


/*
VideoMode vm = new VideoMode(800, 600);
RenderWindow rw = new RenderWindow(vm, "Bezie", Styles.Resize, new ContextSettings(32, 32, 8));



rw.Closed += OnClose;


while (rw.IsOpen)
{
    rw.DispatchEvents();
    rw.Clear();
    rw.Display();
}



static void OnClose(object sender, EventArgs e)
{
    (sender as RenderWindow)?.Close();
}*/

Labirinth l = new Labirinth(79, 25);
l.Generate(0, 0);
l.DrawConsole();