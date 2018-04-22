using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ProcGenWorld : MonoBehaviour {
    [SerializeField]
    private List<Module> sceneOBJs;

    [SerializeField]
    private int iterations;

    public bool weightedGen;
    public Module[] Modules;
    public Module StartModule;
    public GameObject Player;

    private bool d;
    private List<ModuleConnector> pendingExits;
    //Number of iterations to generate (more means bigger map)
	public void BuildWorld() {
        ProcGenIters();
	}
    public void CreateStart()
    {
        //Create starting module (at beginning location)
        var startModule = (Module)Instantiate(StartModule, transform.position, transform.rotation);
        startModule.transform.parent = transform;
        sceneOBJs.Add(startModule);
        //Get exits of these modules
        pendingExits = new List<ModuleConnector>(startModule.GetExits());
    }
    public void EmptyLists()
    {
        if (pendingExits.Any())
            pendingExits.Clear();
        if (sceneOBJs.Any())
            sceneOBJs.Clear();
    }
    public void DestroyWorld()
    {
        EmptyLists();
        var tempList = transform.Cast<Transform>().ToList();
        foreach (var c in tempList)
            DestroyImmediate(c.gameObject);
    }
    public void ClearExits()
    {
        foreach (Module m in sceneOBJs)
        {
            Component[] exits = GetComponentsInChildren<ModuleConnector>();
            foreach(ModuleConnector mc in exits)
                DestroyImmediate(mc);
        }
    }
    public void ProcGenIters()
    {
        //Iterate specific number of times
        for (int i = 0; i < iterations; i++)
        {
            var newExits = new List<ModuleConnector>();

            foreach (var pendingExit in pendingExits)
            {
                var newTag = GetRandom(pendingExit.AllowedConnections);
                var newModulePrefab = GetRandomWithTag(Modules, newTag);
                var newModule = (Module)Instantiate(newModulePrefab);
                newModule.transform.parent = transform;
                var newModuleExits = newModule.GetExits();
                var exitToMatch = newModuleExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newModuleExits);
                MatchExits(pendingExit, exitToMatch);
                //Add code for collision checking here
                Bounds nb = newModule.transform.gameObject.GetComponent<Collider>().bounds;
                foreach (Module x in sceneOBJs)
                {
                    if (nb.Intersects(x.transform.gameObject.GetComponent<Collider>().bounds))
                    {
                        Debug.Log("Destroyed attempted " + newModule.gameObject);
                        sceneOBJs.Remove(newModule);
                        DestroyImmediate(newModule.gameObject);
                        d = true;
                        break;
                    }
                }
                if (!d)
                {
                    DestroyImmediate(pendingExit.gameObject);
                    DestroyImmediate(exitToMatch.gameObject);
                    sceneOBJs.Add(newModule);
                    //Adds new exits that need to be generated from in scene
                    newExits.AddRange(newModuleExits.Where(e => e != exitToMatch));
                }
                d = false;
            }
            pendingExits = newExits;
        }
    }
    public void CreatePlayer()
    {
        Vector3 startP = new Vector3(StartModule.transform.position.x, StartModule.transform.position.y + .5f, StartModule.transform.position.z);
        var startingPlayer = Instantiate(Player);
        startingPlayer.transform.position = startP;
        startingPlayer.transform.parent = transform;
    }

    //Matches exits by rotating and transforming the next prefab to connect
    private void MatchExits(ModuleConnector oldExit, ModuleConnector newExit)
    {
        var newModule = newExit.transform.parent;
        var forwardVectorToMatch = -oldExit.transform.forward;
        var correctiveRotation = ARot(forwardVectorToMatch) - ARot(newExit.transform.forward);
        newModule.RotateAround(newExit.transform.position, Vector3.up, correctiveRotation);
        var correctiveTranslation = oldExit.transform.position - newExit.transform.position;
        newModule.transform.position += correctiveTranslation;
    }
    //RNG to prevent samey generation
    //TODO OBJECT WEIGHTING
    private static TItem GetRandom<TItem>(TItem[] arr) {
        return arr[Random.Range(0, arr.Length)];
    }
    //gets array of weighted modules to choose from
    //Note: current implementation IS EXPENSIVE (2 for loops)
    //Heavier weight on object = more spawns
    private static Module WeightedRandoms(Module[] wM)
    {
        int weightSum = 0;
        foreach (Module m in wM)
            weightSum += m.weight;
        int randS = Random.Range(0, weightSum);
        foreach (Module m in wM){
            if (randS < m.weight)
                return m;
            randS -= m.weight;
        }
        return null;
    }
    private Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
    {
        Module[] matchingModules = modules.Where(m => m.objTag.Equals(tagToMatch)).ToArray();
        if (weightedGen)
            return WeightedRandoms(matchingModules);
        else
            return GetRandom(matchingModules);
    }
    private static float ARot(Vector3 v) { return Vector3.Angle(Vector3.forward, v) * Mathf.Sign(v.x); }
    private void DebugOBJs()
    {
        foreach (Module m in sceneOBJs)
            Debug.Log(m);
    }
}
