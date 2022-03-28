using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece
{
    public static int CurrentRow { get; set; }
    public static int CurrentColumn { get; set; }

    public static int OriginalRow { get; set; }
    public static int OriginalColumn { get; set; }

    public static GameObject GameObject { get; set; }
}
