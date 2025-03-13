using System;

public interface IGazeSelector
{
    event Action GazeIn;
    event Action GazeOut;
}