using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentsController : MonoBehaviour
{
    public Antiseptic antiseptic;
    public Bandage bandage;
    public DrugAndDropCrazyDoctor scalpel;

    public void AntisepticActive()
    {
        bandage.ToolDisable();
        scalpel.isDragging = false;
    }

    public void BandageActive()
    {
        antiseptic.ToolDisable();
        scalpel.isDragging = false;
    }

    public void ScalpelActive()
    {
        bandage.ToolDisable();
        antiseptic.ToolDisable();
    }
}
