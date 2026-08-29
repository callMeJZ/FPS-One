using UnityEngine;

public class LevelObjectives : MonoBehaviour
{
    [Header("Required Objectives")]
    public ObjectiveTarget[] objectives;

    [Header("Performance")]
    public int correctObjects = 0;
    public int incorrectObjects = 0;

    private bool completed = false;

    void Update()
    {
        CheckObjectives();
    }

    void CheckObjectives()
    {
        if (completed)
            return;

        correctObjects = 0;

        foreach (ObjectiveTarget objective in objectives)
        {
            if (objective != null &&
                objective.IsCorrect())
            {
                correctObjects++;
            }
        }

        if (correctObjects == objectives.Length)
        {
            completed = true;

            ObjectivesCompleted();
        }
    }

    public void RegisterIncorrectObject()
    {
        incorrectObjects++;
    }

    void ObjectivesCompleted()
    {
        Debug.Log(
            "ALL LEVEL 1 OBJECTIVES COMPLETED!"
        );
    }

    public bool AreObjectivesComplete()
    {
        return completed;
    }
}