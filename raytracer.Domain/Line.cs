﻿using System.Runtime.ConstrainedExecution;

namespace raytracer.Domain;

public class Line
{
    private Vector start;
    private Vector direction;

    public Line(Vector start, Vector direction)
    {
        this.start = start;
        this.direction = direction;
    }

    public Vector getStart() { return start; }

    public Vector getDirection() { return direction; } 
}