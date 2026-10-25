public class NPC : Core
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupInstances();
    }

    // Update is called once per frame
    void Update()
    {
        if (!state.isComplete)
        {

        }

        state.DoBranch();
    }

    private void FixedUpdate()
    {
        state.FixedDoBranch();
    }
}
