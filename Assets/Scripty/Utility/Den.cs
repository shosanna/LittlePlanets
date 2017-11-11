using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Den
{
    public enum Cas { Rano, Odpoledne, Vecer, Noc, Pulnoc };

    public static bool Rano()
    {
        if (GameState.Instance != null)
        {
            if (GameState.Instance.ProcentoDne() < 0.6f)
                return true;
        }
        return false;
    }

    public static bool Odpoledne()
    {
        if (GameState.Instance != null)
        {
            if (GameState.Instance.ProcentoDne() >= 0.6f && GameState.Instance.ProcentoDne() < 0.75f)
                return true;
        }
        return false;
    }

    public static bool Vecer()
    {
        if (GameState.Instance != null)
        {
            if (GameState.Instance.ProcentoDne() >= 0.75f && GameState.Instance.ProcentoDne() < 0.9f)
                return true;
        }
        return false;
    }

    public static bool Noc()
    {
        if (GameState.Instance != null)
        {
          if (GameState.Instance.ProcentoDne() >= 0.9f)
            return true;
        }
        return false;
    }

    public static bool Pulnoc()
    {
        if (GameState.Instance != null)
        {
            if (GameState.Instance.ProcentoDne() >= 1f)
                return true;
        }
        return false;
    }


    public static Cas Ted()
    {
        if (GameState.Instance.ProcentoDne() >= 1f)
        {
            return Cas.Pulnoc;
        }
        else if (GameState.Instance.ProcentoDne() >= 0.9f)
        {
            return Cas.Noc;
        }
        else if (GameState.Instance.ProcentoDne() >= 0.75f)
        {
            return Cas.Vecer;
        }
        else if (GameState.Instance.ProcentoDne() >= 0.6f)
        {
            return Cas.Odpoledne;
        }
        else
        {
            return Cas.Rano;
        }
    }
}

