using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public LevelObjectives objectiveManager;

    public TMP_Text objectiveText;

    void Start()
    {
        RefreshUI();
    }

    void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        if (objectiveManager == null ||
            objectiveText == null)
            return;

        string display = "OBJECTIVES\n\n";

        foreach (
            ObjectiveTarget objective
            in objectiveManager.objectives)
        {
            if (objective == null)
                continue;

            string objectName =
                objective.GetDisplayName();

            string colorName =
                GetColorName(
                    objective.GetRequiredColor()
                );

            bool completed =
                objective.IsCorrect();

            if (completed)
            {
                display +=
                    "[OK] " +
                    objectName +
                    " → " +
                    colorName +
                    "\n";
            }
            else
            {
                display +=
                    "□ " +
                    objectName +
                    " → " +
                    colorName +
                    "\n";
            }
        }

        objectiveText.text = display;
    }

    string GetColorName(Color color)
    {
        if (Approximately(color, Color.red))
            return "RED";

        if (Approximately(color, Color.blue))
            return "BLUE";

        if (Approximately(color, Color.green))
            return "GREEN";

        if (Approximately(color, Color.yellow))
            return "YELLOW";

        if (Approximately(
            color,
            new Color(0.6f, 0.2f, 1f)))
        {
            return "PURPLE";
        }

        if (Approximately(
            color,
            new Color(1f, 0.5f, 0f)))
        {
            return "ORANGE";
        }

        return "CUSTOM";
    }

    bool Approximately(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < 0.05f &&
               Mathf.Abs(a.g - b.g) < 0.05f &&
               Mathf.Abs(a.b - b.b) < 0.05f;
    }
}