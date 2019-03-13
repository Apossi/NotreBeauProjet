using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    const int WAITING = 0;
    const int WALKING = 1;

    int state;

    Destinations destinations;
    NavMeshAgent agent;
    Animator animator;

    public float remain;

    // Use this for initialization
    void Start()
    {
        destinations = GameObject.Find("GAMEDIRECTOR").GetComponent<Destinations>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("speed", 1f);

        state = WAITING;
        StartCoroutine("WaitAndGo");
    }

    void Update()
    {
        //s'il est plus ou moins proche de la destination
        //et qu'il est encore en train de marcher
        // = s'il vient d'arriver à destination
        remain = agent.remainingDistance;
        if (agent.remainingDistance < 0.1f && state == WALKING)
        {
            //alors on lance la fonction WaitAndGo qui est une coroutine
            StartCoroutine("WaitAndGo");
        }
    }

    // une coroutine est une fonction qui est lancée en parallèle au reste,
    // et sur laquelle on peut faire "pause"
    // c'est pratique pour "attendre" x secondes avant de faire un truc
    IEnumerator WaitAndGo()
    {
        SendMessage("Say", ":)");
        SendMessage("PlaySound", "idle");

        // on change notre état, comme ça on ne relance pas un autre "WaitAndGo" pendant qu'on attend
        state = WAITING;
        // dans l'animator, on change aussi le paramètre "state", pour qu'il lance l'animation qui va avec
        animator.SetInteger("state", state);


        // c'est là qu'on dit de faire pause pendant xx secondes
        yield return new WaitForSeconds(Random.Range(2f, 6f));

        // on charge déjà le point suivant
        agent.destination = destinations.GetRandomDestination();
        Debug.Log("New destination: " + agent.destination.ToString());

        // y'a parfois besoin d'un petit délai pour recalculer la distance
        yield return new WaitForSeconds(0.5f);

        // quand c'est prêt, on change notre state, et le param de l'Animator
        state = WALKING;
        animator.SetInteger("state", state);

        SendMessage("Say", agent.destination.ToString());
        SendMessage("PlaySound", "go");
    }
}