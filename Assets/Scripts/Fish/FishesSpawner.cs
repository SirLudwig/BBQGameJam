using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _fishPrefab;
    [SerializeField] private FishToSpawn[] _fishes;
    [SerializeField] private int _howMany;

    private BoxCollider2D _meshCollider;

    private void Start()
    {
        _meshCollider = GetComponent<BoxCollider2D>();
        
        for (int i = 0; i <= _howMany; i++)
        {
            RandomSpawn();
        }
        
    }
    
    private void RandomSpawn()
    {
        FishToSpawn randomFish = GetRandomFish();
        //print(randomFish.Stats.Name + " probsbility: " + randomFish.Probability);
        
        float randomX = Random.Range(_meshCollider.bounds.min.x, _meshCollider.bounds.max.x);
        float randomY = Random.Range(_meshCollider.bounds.min.y, _meshCollider.bounds.max.y);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        GameObject newFish = Instantiate(_fishPrefab, randomPosition, quaternion.identity);
        newFish.GetComponent<FishController>().SetStats(randomFish.Stats);

        newFish.GetComponent<FishController>().SetField(_meshCollider);

    }

    private FishToSpawn GetRandomFish()
    {
        int allProbablies = 0;
        
        for (int i = 0; i <= _fishes.Length - 1; i++)
        {
            allProbablies += _fishes[i].Probability;
            _fishes[i].MinProb = i.Equals(0) ? 0 : _fishes[i - 1].MaxProb + 1;
            _fishes[i].MaxProb = i.Equals(0) ? _fishes[i].Probability : _fishes[i].Probability + _fishes[i - 1].Probability;

            Selection selection = new Selection(_fishes[i].MinProb, _fishes[i].MaxProb, _fishes[i].Probability);
        }

        int random = Random.Range(0, allProbablies);
        int currentProb = 0;
        
        foreach (FishToSpawn fish in _fishes)
        {
            currentProb += fish.Probability;

            if (random <= currentProb)
            {
                return fish;
            }
        }

        return new FishToSpawn();

    }

}



[System.Serializable]
public class FishToSpawn
{
    public int MinProb;
    public int MaxProb;
    
    [Range(0, 100)] public int Probability;
    public FishStatistics Stats;
}
