using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork //분기점
{
    public string YesString { get; set; }
    public string NoString { get; set; }

    public int YesRoute { get; set; }
    public int NoRoute { get; set; }
}

public class SpeechLine //대사 한줄
{
    public Soldier Speaker { get; set; }
    public string Speech { get; set; }
}

public class Dialogue
{
    public Queue<SpeechLine> speechLines = new Queue<SpeechLine>();

    public SpeechLine Next()
    {
        return speechLines.Dequeue();
    }
}